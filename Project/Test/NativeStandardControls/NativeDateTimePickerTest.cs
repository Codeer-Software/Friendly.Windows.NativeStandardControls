using System;
using System.Threading;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using NativeStandardControls.TestLib;

namespace NativeStandardControls
{
    /// <summary>
    /// NativeDateTimePickerのテスト。
    /// </summary>
    [TestClass]
    public class NativeDateTimePickerTest
    {
        WindowsAppFriend app;
        WindowControl testDlg;

        const int DTN_FIRST2 = -753;
        const int DTN_DATETIMECHANGE = DTN_FIRST2 - 6;

        /// <summary>
        /// 初期化。
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            app = new WindowsAppFriend(Process.Start(TargetPath.NativeControls));
            EventChecker.Initialize(app);
            WindowControl main = WindowControl.FromZTop(app);
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1015));
            buttonTest.EmulateClick(new Async());
            testDlg = main.WaitForNextModal();
        }

        /// <summary>
        /// 終了処理。
        /// </summary>
        [TestCleanup]
        public void TearDown()
        {
            if (app != null)
            {
                new NativeButton(testDlg.IdentifyFromWindowText("OK")).EmulateClick();
                MessageBoxUtility.CloseAll(testDlg);
                app.Dispose();
                Process process = Process.GetProcessById(app.ProcessId);
                process.CloseMainWindow();
                app = null;
            }
        }

        /// <summary>
        /// コンストラクタのテスト。
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            DateTime setDay = DateTime.Today.AddDays(1);

            //WindowControlから作成。
            {
                NativeDateTimePicker picker = new NativeDateTimePicker(testDlg.IdentifyFromDialogId(1013));
                picker.EmulateSelectDay(setDay);
                Assert.AreEqual(setDay, picker.SelectedDay.Date);
            }
            //ハンドルから作成。
            {
                NativeDateTimePicker picker = new NativeDateTimePicker(app, testDlg.IdentifyFromDialogId(1013).Handle);
                picker.EmulateSelectDay(setDay);
                Assert.AreEqual(setDay, picker.SelectedDay.Date);
            }
        }

        /// <summary>
        /// EmulateSelectDayとSelectedDayの値の設定、取得のテスト。
        /// </summary>
        [TestMethod]
        public void TestSelectedDay()
        {
            DateTime setDay = DateTime.Today.AddDays(1);
            NativeDateTimePicker picker = new NativeDateTimePicker(testDlg.IdentifyFromDialogId(1013));
            picker.EmulateSelectDay(setDay);
            Assert.AreEqual(setDay, picker.SelectedDay.Date);

            //非同期でも同様の効果があることを確認。
            setDay = setDay.AddDays(1);
            Async a = new Async();
            picker.EmulateSelectDay(setDay, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.AreEqual(setDay, picker.SelectedDay.Date);
        }

        /// <summary>
        /// EmulateSelectDayのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateSelectDayEvent()
        {
            //イベント確認。
            NativeDateTimePicker picker = new NativeDateTimePicker(testDlg.IdentifyFromDialogId(1013));
            picker.EmulateSelectDay(DateTime.Today);
            DateTime setDay = DateTime.Today.AddDays(1);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { picker.EmulateSelectDay(setDay); },
                new CodeInfo(1013, NativeMethods.WM_NOTIFY, DTN_DATETIMECHANGE)));

            //詳細な通知内容をテスト。
            setDay = setDay.AddDays(1);
            NMDATETIMECHANGE[] expectation = new NMDATETIMECHANGE[1];
            expectation[0].dwFlags = 0;
            expectation[0].st = NativeDataUtility.ToSYSTEMTIME(setDay);
            Assert.IsTrue(EventChecker.CheckNotifyDetail(testDlg,
                delegate { picker.EmulateSelectDay(setDay); },
                expectation));
        }

        /// <summary>
        /// EmulateSelectDayの非同期イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateSelectDayEventAsync()
        {
            NativeDateTimePicker picker = new NativeDateTimePicker(testDlg.IdentifyFromDialogId(1014));
            Async async = new Async();
            picker.EmulateSelectDay(DateTime.Today.AddDays(1), async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }
    }
}
