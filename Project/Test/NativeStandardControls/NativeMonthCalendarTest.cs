using System;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using NativeStandardControls.TestLib;

namespace NativeStandardControls
{
    /// <summary>
    /// NativeMonthCalendarテストクラス。
    /// </summary>
    [TestClass]
    public class NativeMonthCalendarTest
    {
        const int MCN_FIRST = -746;
        const int MCN_SELCHANGE = (MCN_FIRST - 3);
        const int MCN_SELECT = (MCN_FIRST);
        const int MCN_VIEWCHANGE = (MCN_FIRST - 4);
        const int NM_RELEASEDCAPTURE = -16;

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
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1032));
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
                NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1033));
                Assert.AreEqual(DateTime.Today, calendar.SelectedDay.Date);
            }
            //ハンドルから作成。
            {
                NativeMonthCalendar calendar = new NativeMonthCalendar(app, testDlg.IdentifyFromDialogId(1033).Handle);
                Assert.AreEqual(DateTime.Today, calendar.SelectedDay.Date);
            }
        }

        /// <summary>
        /// MaxSelectionCountのテスト。
        /// </summary>
        [TestMethod]
        public void TestMaxSelectionCount()
        {
            NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1031));
            Assert.AreEqual(7, calendar.MaxSelectionCount);
        }

        /// <summary>
        /// Todayのテスト。
        /// </summary>
        [TestMethod]
        public void TestToday()
        {
            NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1031));
            Assert.AreEqual(DateTime.Today, calendar.Today.Date);
        }

        /// <summary>
        /// FirstDayOfWeekのテスト。
        /// </summary>
        [TestMethod]
        public void TestFirstDayOfWeek()
        {
            NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1031));
            Assert.AreEqual(Day.Sunday, calendar.FirstDayOfWeek);
        }

        /// <summary>
        /// EmulateSelectDayとSelectedDayのテスト。
        /// </summary>
        [TestMethod]
        public void TestSingleSelectValue()
        {
            NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1033));
            DateTime setDay = DateTime.Today.AddDays(1);
            calendar.EmulateSelectDay(setDay);
            Assert.AreEqual(setDay, calendar.SelectedDay.Date);

            //非同期でも同様の効果があることを確認。
            Async a = new Async();
            setDay = DateTime.Today.AddDays(1);
            calendar.EmulateSelectDay(setDay, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.AreEqual(setDay, calendar.SelectedDay.Date);
        }

        /// <summary>
        /// EmulateSelectDayのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestSingleSelectEvent()
        {
            NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1033));
            calendar.EmulateSelectDay(DateTime.Today);
            DateTime setDay = DateTime.Today.AddDays(1);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { calendar.EmulateSelectDay(setDay); },
                new CodeInfo(1033, NativeMethods.WM_NOTIFY, MCN_SELCHANGE),
                new CodeInfo(1033, NativeMethods.WM_NOTIFY, MCN_SELECT)));

            //詳細な通知内容の確認。
            setDay.AddDays(1);
            NMSELCHANGE[] expectation = new NMSELCHANGE[2];
            expectation[0].stSelStart = expectation[1].stSelStart =
            expectation[0].stSelEnd = expectation[1].stSelEnd = NativeDataUtility.ToSYSTEMTIME(setDay);
            Assert.IsTrue(EventChecker.CheckNotifyDetail(testDlg,
                delegate { calendar.EmulateSelectDay(setDay); },
                expectation));
        }

        /// <summary>
        /// EmulateSelectDayの非同期実行イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestSingleSelectEventAsync()
        {
            NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1032));
            Async async = new Async();
            calendar.EmulateSelectDay(DateTime.Today.AddDays(1), async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateSelectDayとGetSelectionRangeの値テスト。
        /// </summary>
        [TestMethod]
        public void TestMultiSelectValue()
        {
            NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1031));
            DateTime min = DateTime.Today.AddDays(1);
            DateTime max = DateTime.Today.AddDays(2);
            calendar.EmulateSelectDay(min, max);

            DateTime minRet = new DateTime(), maxRet = new DateTime();
            calendar.GetSelectionRange(ref minRet, ref maxRet);
            Assert.AreEqual(min, minRet.Date);
            Assert.AreEqual(max, maxRet.Date);

            //非同期でも同様の効果があることを確認。
            min = min.AddDays(1);
            max = max.AddDays(1);
            Async a = new Async();
            calendar.EmulateSelectDay(min, max, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            calendar.GetSelectionRange(ref minRet, ref maxRet);
            Assert.AreEqual(min, minRet.Date);
            Assert.AreEqual(max, maxRet.Date);
        }

        /// <summary>
        /// EmulateSelectDayとGetSelectionRangeのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestMultiSelectEvent()
        {
            //イベント確認。
            NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1031));
            calendar.EmulateSelectDay(DateTime.Today);
            DateTime min = DateTime.Today.AddDays(1);
            DateTime max = DateTime.Today.AddDays(2);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { calendar.EmulateSelectDay(min, max); },
                new CodeInfo(1031, NativeMethods.WM_NOTIFY, MCN_SELCHANGE),
                new CodeInfo(1031, NativeMethods.WM_NOTIFY, MCN_SELECT)));

            //詳細な通知内容の確認。
            min = min.AddDays(1);
            max = max.AddDays(1);
            NMSELCHANGE[] expectation = new NMSELCHANGE[2];
            expectation[0].stSelStart = expectation[1].stSelStart = NativeDataUtility.ToSYSTEMTIME(min);
            expectation[0].stSelEnd = expectation[1].stSelEnd = NativeDataUtility.ToSYSTEMTIME(max);
            Assert.IsTrue(EventChecker.CheckNotifyDetail(testDlg,
                 delegate { calendar.EmulateSelectDay(min, max); },
                expectation));
        }

        /// <summary>
        /// EmulateSelectDayとGetSelectionRangeの非同期実行イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestMultiSelectEventAsync()
        {
            DateTime min = DateTime.Today.AddDays(1);
            DateTime max = DateTime.Today.AddDays(2);
            NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1034));
            Async async = new Async();
            calendar.EmulateSelectDay(min, max, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateChangeViewとMonthCalendarViewの値テスト。
        /// </summary>
        [TestMethod]
        public void TestViewValue()
        {
            NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1033));
            if (!NativeMethods.IsWindowUnicode(testDlg.Handle) || !OSUtility.Is7or8())
            {
                return;
            }
            calendar.EmulateChangeView(MonthCalendarView.Year);
            Assert.AreEqual(MonthCalendarView.Year, calendar.MonthCalendarView);

            //非同期でも同様の効果があることを確認。
            Async a = new Async();
            calendar.EmulateChangeView(MonthCalendarView.Decade, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.AreEqual(MonthCalendarView.Decade, calendar.MonthCalendarView);
        }

        /// <summary>
        /// EmulateChangeViewのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestViewEvent()
        {
            if (!NativeMethods.IsWindowUnicode(testDlg.Handle) || !OSUtility.Is7or8())
            {
                return;
            } 
            NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1033));
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { calendar.EmulateChangeView(MonthCalendarView.Year); },
                new CodeInfo(1033, NativeMethods.WM_NOTIFY, MCN_VIEWCHANGE)));
        }

        /// <summary>
        /// EmulateChangeViewの非同期実行イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestViewEventAsync()
        {
            if (!NativeMethods.IsWindowUnicode(testDlg.Handle) || !OSUtility.Is7or8())
            {
                return;
            }
            NativeMonthCalendar calendar = new NativeMonthCalendar(testDlg.IdentifyFromDialogId(1032));
            Async async = new Async();
            calendar.EmulateChangeView(MonthCalendarView.Year, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async)); ;
        }
    }
}
