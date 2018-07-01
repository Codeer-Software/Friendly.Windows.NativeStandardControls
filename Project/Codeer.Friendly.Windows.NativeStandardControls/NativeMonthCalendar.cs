using System;
using System.Windows.Forms;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type SysMonthCal32.
    /// </summary>    
#else
    /// <summary>
    /// WindowClassがSysMonthCal32のウィンドウに対応した操作を提供します。
    /// </summary>    
#endif
    [ControlDriver(WindowClassName = "SysMonthCal32")]
    public class NativeMonthCalendar : NativeWindow
    {
        internal const int MCM_FIRST = 0x1000;
        internal const int MCM_GETCURSEL = (MCM_FIRST + 1);
        internal const int MCM_SETCURSEL = (MCM_FIRST + 2);
        internal const int MCM_GETMAXSELCOUNT = (MCM_FIRST + 3);
        internal const int MCM_GETSELRANGE = (MCM_FIRST + 5);
        internal const int MCM_SETSELRANGE = (MCM_FIRST + 6);
        internal const int MCM_GETTODAY = (MCM_FIRST + 13);
        internal const int MCM_GETFIRSTDAYOFWEEK = (MCM_FIRST + 16);
        internal const int MCM_GETCURRENTVIEW = (MCM_FIRST + 22);
        internal const int MCM_SETCURRENTVIEW = (MCM_FIRST + 32);
        internal const int MCN_FIRST = -746;
        internal const int MCN_SELCHANGE = (MCN_FIRST - 3);
        internal const int MCN_SELECT = (MCN_FIRST);
        internal const int MCN_VIEWCHANGE = (MCN_FIRST - 4);

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">WindowControl with the target window. </param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="src">元となるウィンドウコントロールです。</param>
#endif
        public NativeMonthCalendar(WindowControl src)
            : base(src)
        {
            Initializer.Initialize(App);
        }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
        /// <param name="windowHandle">Window handle.</param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="windowHandle">ウィンドウハンドル。</param>
#endif
        public NativeMonthCalendar(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App);
        }

 #if ENG
        /// <summary>
        /// Returns the maximum number of days that can be selected.
        /// </summary>
#else
        /// <summary>
        /// 選択できる最大日数です。
        /// </summary>
#endif
        public int MaxSelectionCount
        {
            get
            {
                return (int)SendMessage(MCM_GETMAXSELCOUNT, IntPtr.Zero, IntPtr.Zero);
            }
        }

#if ENG
        /// <summary>
        /// Returns today's date.
        /// </summary>
#else
        /// <summary>
        /// 今日の日付です。
        /// </summary>
#endif
        public DateTime Today
        {
            get
            {
                AppVar inTarget = App.Dim(new SYSTEMTIME());
                App[typeof(NativeMethods), "SendMessage"](Handle, MCM_GETTODAY, IntPtr.Zero, inTarget);
                return NativeDataUtility.ToDateTime((SYSTEMTIME)inTarget.Core);
            }
        }

#if ENG
        /// <summary>
        /// Returns the currently selected date.
        /// </summary>
#else
        /// <summary>
        /// 現在の選択日時です。
        /// </summary>
#endif
        public DateTime SelectedDay
        {
            get
            {
                AppVar inTarget = App.Dim(new SYSTEMTIME());
                App[typeof(NativeMethods), "SendMessage"](Handle, MCM_GETCURSEL, IntPtr.Zero, inTarget);
                return NativeDataUtility.ToDateTime((SYSTEMTIME)inTarget.Core);
            }
        }

#if ENG
        /// <summary>
        /// Returns the current display mode.
        /// </summary>
#else
        /// <summary>
        /// 現在の表示モードを取得します。
        /// </summary>
#endif
        public MonthCalendarView MonthCalendarView
        {
            get
            {
                return (MonthCalendarView)SendMessage(MCM_GETCURRENTVIEW, IntPtr.Zero, IntPtr.Zero);
            }
        }

#if ENG
        /// <summary>
        /// Returns the day of the week displayed in the leftmost column.
        /// </summary>
#else
        /// <summary>
        /// カレンダーの左端の列に表示される曜日を取得します。
        /// </summary>
#endif
        public Day FirstDayOfWeek
        {
            get
            {
                int ret = (int)SendMessage(MCM_GETFIRSTDAYOFWEEK, IntPtr.Zero, IntPtr.Zero);
                return (Day)NativeDataUtility.LOWORD(ret);
            }
        }

#if ENG
        /// <summary>
        /// Obtains the currently selected date range.
        /// Use this when multiple dates are selected.
        /// </summary>
        /// <param name="min">The selection start date.</param>
        /// <param name="max">The selection end date.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// ユーザーによって現在選択されている日付範囲を取得します。
        /// 複数選択の場合に使用します。
        /// </summary>
        /// <param name="min">最小日付。</param>
        /// <param name="max">最大日付。</param>
        /// <returns>成否。</returns>
#endif
        public bool GetSelectionRange(ref DateTime min, ref DateTime max)
        {
            AppVar inTarget = App.Dim(new SYSTEMTIME[2]);
            if (!(bool)App[GetType(), "GetSelectionRangeInTarget"](Handle, inTarget).Core)
            {
                return false;
            }
            SYSTEMTIME[] sysTimes = (SYSTEMTIME[])inTarget.Core;
            min = NativeDataUtility.ToDateTime(sysTimes[0]);
            max = NativeDataUtility.ToDateTime(sysTimes[1]);
            return true;
        }

#if ENG
        /// <summary>
        /// Sets the selected date.
        /// Produces MCN_SELCHANGE and MCN_SELECT notifications.
        /// </summary>
        /// <param name="day">The date to select.</param>
#else
        /// <summary>
        /// 現在の選択日付を設定します。
        /// MCN_SELCHANGE、MCN_SELECTの通知が発生します。
        /// </summary>
        /// <param name="day">選択日付。</param>
#endif
        public void EmulateSelectDay(DateTime day)
        {
            App[GetType(), "EmulateSelectDayInTarget"](Handle, day);
        }

#if ENG
        /// <summary>
        /// Sets the selected date.
        /// Produces MCN_SELCHANGE and MCN_SELECT notifications.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="day">The date to select.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 現在の選択日付を設定します。
        /// MCN_SELCHANGE、MCN_SELECTの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="day">選択する日時。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateSelectDay(DateTime day, Async async)
        {
            App[GetType(), "EmulateSelectDayInTarget", async](Handle, day);
        }

#if ENG
        /// <summary>
        /// Sets the selected date range.
        /// Use this when selecting multiple dates.
        /// Produces MCN_SELCHANGE and MCN_SELECT notifications.
        /// </summary>
        /// <param name="min">The start date.</param>
        /// <param name="max">The end date.</param>
#else
        /// <summary>
        /// 現在の選択範囲を設定します。
        /// 複数選択の場合に使用します。
        /// MCN_SELCHANGE、MCN_SELECTの通知が発生します。
        /// </summary>
        /// <param name="min">最小日付。</param>
        /// <param name="max">最大日付。</param>
#endif
        public void EmulateSelectDay(DateTime min, DateTime max)
        {
            App[GetType(), "EmulateSelectDayInTarget"](Handle, min, max);
        }

#if ENG
        /// <summary>
        /// Sets the selected date range.
        /// Use this when selecting multiple dates.
        /// Produces MCN_SELCHANGE and MCN_SELECT notifications.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="min">The start date.</param>
        /// <param name="max">The end date.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 現在の選択範囲を設定します。
        /// 複数選択の場合に使用します。
        /// MCN_SELCHANGE、MCN_SELECTの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="min">最小日付。</param>
        /// <param name="max">最大日付。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateSelectDay(DateTime min, DateTime max, Async async)
        {
            App[GetType(), "EmulateSelectDayInTarget", async](Handle, min, max);
        }

#if ENG
        /// <summary>
        /// Set's the calendar's display mode.
        /// Produces a MCN_VIEWCHANGE notification.
        /// </summary>
        /// <param name="view">The display mode.</param>
#else
        /// <summary>
        /// 表示モードを設定します。
        /// MCN_VIEWCHANGEの通知が発生します。
        /// </summary>
        /// <param name="view">表示モード。</param>
#endif
        public void EmulateChangeView(MonthCalendarView view)
        {
            App[GetType(), "EmulateChangeViewInTarget"](Handle, view);
        }

#if ENG
        /// <summary>
        /// Set's the calendar's display mode.
        /// Produces a MCN_VIEWCHANGE notification.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="view">The display mode.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 表示モードを設定します。
        /// MCN_VIEWCHANGEの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="view">表示モード。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeView(MonthCalendarView view, Async async)
        {
            App[GetType(), "EmulateChangeViewInTarget", async](Handle, view);
        }

        /// <summary>
        /// ユーザーによって現在選択されている日付範囲を取得します。
        /// 複数選択の場合に使用します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="minMax">最小日付、最大日付。</param>
        /// <returns>成否。</returns>
        private static bool GetSelectionRangeInTarget(IntPtr handle, SYSTEMTIME[] minMax)
        {
            return NativeDataUtility.ToBool(NativeMethods.SendMessage(handle, MCM_GETSELRANGE, IntPtr.Zero, minMax));
        }

        /// <summary>
        /// 現在の選択日付を設定します。
        /// MCN_SELCHANGE、MCN_SELECTの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="day">選択する日時。</param>
        private static void EmulateSelectDayInTarget(IntPtr handle, DateTime day)
        {
            NativeMethods.SetFocus(handle);

            //変更
            SYSTEMTIME native = NativeDataUtility.ToSYSTEMTIME(day);
            NativeMethods.SendMessage(handle, MCM_SETCURSEL, IntPtr.Zero, ref native);

            //通知
            foreach (int message in new int[] { MCN_SELCHANGE, MCN_SELECT })
            {
                NMSELCHANGE notify = new NMSELCHANGE();
                EmulateUtility.InitNotify(handle, message, ref notify.nmhdr);
                notify.stSelStart = notify.stSelEnd = native;
                NativeMethods.SendMessage(NativeMethods.GetParent(handle), NativeCommonDefine.WM_NOTIFY, notify.nmhdr.idFrom, ref notify);
            }
        }

        /// <summary>
        /// 現在の選択日付を設定します。
        /// MCN_SELCHANGE、MCN_SELECTの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="min">最小日付。</param>
        /// <param name="max">最大日付。</param>
        private static void EmulateSelectDayInTarget(IntPtr handle, DateTime min, DateTime max)
        {
            NativeMethods.SetFocus(handle);

            SYSTEMTIME[] sysTimes = new SYSTEMTIME[] { NativeDataUtility.ToSYSTEMTIME(min), NativeDataUtility.ToSYSTEMTIME(max) };
            NativeMethods.SendMessage(handle, MCM_SETSELRANGE, IntPtr.Zero, sysTimes);

            //通知
            foreach (int message in new int[] { MCN_SELCHANGE, MCN_SELECT })
            {
                NMSELCHANGE notify = new NMSELCHANGE();
                EmulateUtility.InitNotify(handle, message, ref notify.nmhdr);
                notify.stSelStart = sysTimes[0];
                notify.stSelEnd = sysTimes[1];
                NativeMethods.SendMessage(NativeMethods.GetParent(handle), NativeCommonDefine.WM_NOTIFY, notify.nmhdr.idFrom, ref notify);
            }
        }

        /// <summary>
        /// 表示モードを設定します。
        /// MCN_VIEWCHANGEの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="view">表示モード。</param>
        private static void EmulateChangeViewInTarget(IntPtr handle, MonthCalendarView view)
        {
            NativeMethods.SetFocus(handle);
            NativeMethods.SendMessage(handle, MCM_SETCURRENTVIEW, IntPtr.Zero, new IntPtr((int)view));
        }
    }
}
