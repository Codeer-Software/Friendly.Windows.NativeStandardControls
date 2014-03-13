using System;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows whose WindowClass is SysDateTimePick32.
    /// </summary>
#else
    /// <summary>
    /// WindowClassがSysDateTimePick32のウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    public class NativeDateTimePicker : NativeWindow
    {
        internal const int DTM_FIRST = 0x1000;
        internal const int DTM_GETSYSTEMTIME = (DTM_FIRST + 1);
        internal const int DTM_SETSYSTEMTIME = (DTM_FIRST + 2);
        internal const int GDT_VALID = 0;
        internal const int DTN_FIRST2 = -753;
        internal const int DTN_DATETIMECHANGE = DTN_FIRST2 - 6;
        
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
        public NativeDateTimePicker(WindowControl src)
            : base(src)
        {
            Initializer.Initialize(App, GetType());
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
        public NativeDateTimePicker(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App, GetType());
        }
        
#if ENG
        /// <summary>
        /// Returns the selected DateTime.
        /// </summary>
#else
        /// <summary>
        /// 選択されている日時です。
        /// </summary>
#endif
        public DateTime SelectedDay
        {
            get
            {
                AppVar inTarget = App.Dim(new SYSTEMTIME());
                App[typeof(NativeMethods), "SendMessage"](Handle, DTM_GETSYSTEMTIME, IntPtr.Zero, inTarget);
                return NativeDataUtility.ToDateTime((SYSTEMTIME)inTarget.Core);
            }
        }
        
#if ENG
        /// <summary>
        /// Sets the selected DateTime.
        /// Notifies DTN_DATETIMECHANGE.
        /// </summary>
        /// <param name="day">DateTime to set.</param>
#else
        /// <summary>
        /// 現在時間を設定します。
        /// DTN_DATETIMECHANGEの通知が発生します。
        /// </summary>
        /// <param name="day">選択する日時。</param>
#endif
        public void EmulateSelectDay(DateTime day)
        {
            App[GetType(), "EmulateSelectDayInTarget"](Handle, day);
        }
        
#if ENG
        /// <summary>
        /// Sets the selected DateTime.
        /// Notifies DTN_DATETIMECHANGE.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="day">DateTime to set.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 現在時間を設定します。
        /// DTN_DATETIMECHANGEの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="day">選択する日時。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateSelectDay(DateTime day, Async async)
        {
            App[GetType(), "EmulateSelectDayInTarget", async](Handle, day);
        }

        /// <summary>
        /// 現在時間を設定します。
        /// DTN_DATETIMECHANGEの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="day">選択する日時。</param>
        private static void EmulateSelectDayInTarget(IntPtr handle, DateTime day)
        {
            NativeMethods.SetFocus(handle);

            //変更
            SYSTEMTIME native = NativeDataUtility.ToSYSTEMTIME(day);
            NativeMethods.SendMessage(handle, DTM_SETSYSTEMTIME, new IntPtr(GDT_VALID), ref native);

            //WM_NOTIFY送信
            NMDATETIMECHANGE nmdata = new NMDATETIMECHANGE();
            EmulateUtility.InitNotify(handle, DTN_DATETIMECHANGE, ref nmdata.nmhdr);
            nmdata.dwFlags = GDT_VALID;
            nmdata.st = native;
            NativeMethods.SendMessage(NativeMethods.GetParent(handle), NativeCommonDefine.WM_NOTIFY, nmdata.nmhdr.idFrom, ref nmdata);
        }
    }
}
