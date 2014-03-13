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
    /// NativeSpinButtonテストクラス。
    /// </summary>
    [TestClass]
    public class NativeSpinButtonTest
    {
        const int UDN_FIRST = -721;
        const int UDN_DELTAPOS = UDN_FIRST - 1;
        const int SB_THUMBPOSITION = 4;
        const int SB_ENDSCROLL = 8;
        const int EN_CHANGE = 0x0300;
        const int EN_UPDATE = 0x0400;

        WindowsAppFriend app;
        WindowControl testDlg;

        /// <summary>
        /// 初期化。
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            app = new WindowsAppFriend(Process.Start(TargetPath.NativeControls));
            EventChecker.Initialize(app);
            WindowControl main = WindowControl.FromZTop(app);
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1025));
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
            //WindowControlから作成。
            {
                NativeSpinButton spin = new NativeSpinButton(testDlg.IdentifyFromDialogId(1021));
                int min = 0, max = 0;
                spin.GetRange(ref min, ref max);
                Assert.AreEqual(1000, max);
            }
            //ハンドルから作成。
            {
                NativeSpinButton spin = new NativeSpinButton(app, testDlg.IdentifyFromDialogId(1021).Handle);
                int min = 0, max = 0;
                spin.GetRange(ref min, ref max);
                Assert.AreEqual(1000, max);
            }
        }

        /// <summary>
        /// GetRangeのテスト。
        /// </summary>
        [TestMethod]
        public void TestRange()
        {
            NativeSpinButton spin = new NativeSpinButton(testDlg.IdentifyFromDialogId(1021));
            int min = 0, max = 0;
            spin.GetRange(ref min, ref max);
            Assert.AreEqual(1, min);
            Assert.AreEqual(1000, max);
        }

        /// <summary>
        /// EmulateChangePosとPosの値テスト。
        /// </summary>
        [TestMethod]
        public void TestPosValue()
        {
            NativeSpinButton spin = new NativeSpinButton(testDlg.IdentifyFromDialogId(1021));
            spin.EmulateChangePos(200);
            Assert.AreEqual(200, spin.Pos);

            //非同期でも同様の効果があることを確認。
            Async a = new Async();
            spin.EmulateChangePos(300, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.AreEqual(300, spin.Pos);
        }

        /// <summary>
        /// EmulateChangePosのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestPosEvent()
        {
            //同期実行。
            NativeSpinButton spin = new NativeSpinButton(testDlg.IdentifyFromDialogId(1021));
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { spin.EmulateChangePos(100); },
                new CodeInfo(1020, NativeMethods.WM_COMMAND, EN_UPDATE),
                new CodeInfo(1020, NativeMethods.WM_COMMAND, EN_CHANGE),
                new CodeInfo(1021, NativeMethods.WM_NOTIFY, UDN_DELTAPOS),
                new CodeInfo(1021, NativeMethods.WM_VSCROLL, SB_THUMBPOSITION, 100),
                new CodeInfo(1021, NativeMethods.WM_VSCROLL, SB_ENDSCROLL, 100)));

            //詳細な通知内容の確認。
            NMUPDOWN[] expectation = new NMUPDOWN[1];
            expectation[0].iPos = 150;
            expectation[0].iDelta = 50;
            Assert.IsTrue(EventChecker.CheckNotifyDetail(testDlg,
                delegate { spin.EmulateChangePos(150); },
                expectation));
        }

        /// <summary>
        /// EmulateChangePosの非同期実行イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestPosEventAsync()
        {
            NativeSpinButton spin = new NativeSpinButton(testDlg.IdentifyFromDialogId(1023));
            Async async = new Async();
            spin.EmulateChangePos(100, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }
    }
}
