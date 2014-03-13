using System;
using System.Windows.Forms;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type Button. 
    /// </summary>
#else
    /// <summary>
    /// WindowClassがButtonのウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    public class NativeButton : NativeWindow
    {
        internal const int BM_GETCHECK = 0x00F0;
        internal const int BM_SETCHECK = 0x00F1;
        internal const int BM_GETSTATE = 0x00F2;
        internal const int BM_SETSTATE = 0x00F3;
        internal const int BM_SETSTYLE = 0x00F4;
        internal const int BM_CLICK = 0x00F5;
        internal const int BM_GETIMAGE = 0x00F6;
        internal const int BM_SETIMAGE = 0x00F7;
        internal const int BM_SETDONTCLICK = 0x00F8;
        internal const int BN_CLICKED = 0;
        internal const int BS_CHECKBOX = 0x00000002;
        internal const int BS_AUTOCHECKBOX = 0x00000003;
        internal const int BS_RADIOBUTTON = 0x00000004;
        internal const int BS_3STATE = 0x00000005;
        internal const int BS_AUTO3STATE = 0x00000006;
        internal const int BS_AUTORADIOBUTTON = 0x00000009;
        
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
        public NativeButton(WindowControl src)
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
        public NativeButton(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App, GetType());
        }
        
#if ENG
        /// <summary>
        /// Returns the object's check state.
        /// </summary>
#else
        /// <summary>
        /// チェック状態。
        /// </summary>
#endif
        public CheckState CheckState
        {
            get
            {
                return (CheckState)App[GetType(), "GetCheckStateInTarget"](Handle).Core;
            }
        }
        
#if ENG
        /// <summary>
        /// Performs a click and notifies BN_CLICKED.
        /// </summary>
#else
        /// <summary>
        /// クリック。
        /// BN_CLICKEDの通知が発生します。
        /// </summary>
#endif
        public void EmulateClick()
        {
            App[GetType(), "EmulateClickInTarget"](Handle);
        }
        
#if ENG
        /// <summary>
        /// Performs a click and notifies BN_CLICKED.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// クリック。
        /// BN_CLICKEDの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateClick(Async async)
        {
            App[GetType(), "EmulateClickInTarget", async](Handle);
        }
        
#if ENG
        /// <summary>
        /// Sets the object's check state. 
        /// If the state changes, BN_CLICKED is notified.
        /// </summary>
        /// <param name="state">Check state.</param>
#else
        /// <summary>
        /// チェック状態設定。
        /// 状態が変更された場合、BN_CLICKEDの通知が発生します。
        /// </summary>
        /// <param name="state">チェック状態。</param>
#endif
        public void EmulateCheck(CheckState state)
        {
            App[GetType(), "EmulateCheckInTarget"](Handle, state);
        }
        
#if ENG
        /// <summary>
        /// Sets the object's check state. 
        /// If the state changes, BN_CLICKED is notified.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="state">Check state.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// チェック状態設定。
        /// 状態が変更された場合、BN_CLICKEDの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="state">チェック状態。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateCheck(CheckState state, Async async)
        {
            App[GetType(), "EmulateCheckInTarget", async](Handle, state);
        }

        /// <summary>
        /// クリック。
        /// BN_CLICKEDの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        private static void EmulateClickInTarget(IntPtr handle)
        {
            NativeMethods.SetFocus(handle);
            NativeMethods.SendMessage(handle, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// チェック状態設定。
        /// 状態が変更された場合、BN_CLICKEDの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="state">チェック状態。</param>
        private static void EmulateCheckInTarget(IntPtr handle, CheckState state)
        {
            NativeMethods.SetFocus(handle);

            int tryCount = 0;
            int max = Enum.GetValues(typeof(CheckState)).Length;
            while (state != (CheckState)NativeMethods.SendMessage(handle, BM_GETCHECK, IntPtr.Zero, IntPtr.Zero).ToInt32())
            {
                NativeMethods.SendMessage(handle, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
                tryCount++;
                if (max < tryCount)
                {
                    throw new NotSupportedException(ResourcesLocal.Instance.CheckStateIsNotSupported);
                }
            }
        }

        /// <summary>
        /// チェック状態取得。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <returns>チェック状態</returns>
        internal static CheckState GetCheckStateInTarget(IntPtr handle)
        {
            return (CheckState)NativeMethods.SendMessage(handle, NativeButton.BM_GETCHECK, IntPtr.Zero, IntPtr.Zero).ToInt32();
        }
    }
}
