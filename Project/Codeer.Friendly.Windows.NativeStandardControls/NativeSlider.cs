using System;
using System.Drawing;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type msctls_trackbar32.
    /// </summary>
#else
    /// <summary>
    /// WindowClassがmsctls_trackbar32のウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(WindowClassName = "msctls_trackbar32")]
    public class NativeSlider : NativeWindow
    {
        const int TBM_GETPOS = (NativeCommonDefine.WM_USER);
        const int TBM_GETRANGEMIN = (NativeCommonDefine.WM_USER + 1);
        const int TBM_GETRANGEMAX = (NativeCommonDefine.WM_USER + 2);
        const int TBM_SETPOS = (NativeCommonDefine.WM_USER + 5);
        const int TBS_VERT = 2;
        const int TB_THUMBTRACK = 5;
        const int TBS_NOTIFYBEFOREMOVE = 0x0800;
        const int TRBN_FIRST = -1501;
        const int TRBN_THUMBPOSCHANGING = TRBN_FIRST - 1;
        
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
        public NativeSlider(WindowControl src)
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
        public NativeSlider(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App);
        }
        
#if ENG
        /// <summary>
        /// Returns true if this is a vertical slider.
        /// </summary>
#else
        /// <summary>
        /// 縦スクロールバーであるか。
        /// </summary>
#endif
        public bool IsVertical
        {
            get
            {
                return ((long)(IntPtr)App[typeof(NativeMethods), "GetWindowLongPtr"](Handle, NativeCommonDefine.GWL_STYLE).Core & TBS_VERT) != 0;
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the minimum value.
        /// </summary>
#else
        /// <summary>
        /// 最小値です。
        /// </summary>
#endif
        public int Min
        {
            get
            {
                return (int)SendMessage(TBM_GETRANGEMIN, IntPtr.Zero, IntPtr.Zero);
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the maximum value.
        /// </summary>
#else
        /// <summary>
        /// 最大値を取得です。
        /// </summary>
#endif
        public int Max
        {
            get
            {
                return (int)SendMessage(TBM_GETRANGEMAX, IntPtr.Zero, IntPtr.Zero);
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the current position.
        /// </summary>
#else
        /// <summary>
        /// 現在位置です。
        /// </summary>
#endif
        public int Pos
        {
            get
            {
                return (int)App[GetType(), "GetPosInTarget"](Handle).Core;
            }
        }
        
#if ENG
        /// <summary>
        /// Sets the currently selected position.
        /// Produces a WM_HSCROLL or WM_VSCROLL notification.
        /// Uses SB_THUMBTRACK type scrolling.
        /// </summary>
        /// <param name="pos">The position to set.</param>
#else
        /// <summary>
        /// スクロールバーの現在位置を設定のための通知を発生させます。
        /// WM_HSCROLL、もしくはWM_VSCROLLの通知が発生します。
        /// スクロールのタイプはSB_THUMBTRACKです。
        /// </summary>
        /// <param name="pos">現在位置。</param>
#endif
        public void EmulateChangePos(int pos)
        {
            App[GetType(), "EmulateChangePosInTarget"](Handle, IsVertical, pos);
        }
        
#if ENG
        /// <summary>
        /// Sets the currently selected position.
        /// Produces a WM_HSCROLL or WM_VSCROLL notification.
        /// Uses SB_THUMBTRACK type scrolling.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="pos">The position to set.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 現在位置を設定します。
        /// WM_HSCROLL、もしくはWM_VSCROLLの通知が発生します。
        /// スクロールのタイプはSB_THUMBTRACKです。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="pos">現在位置。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangePos(int pos, Async async)
        {
            App[GetType(), "EmulateChangePosInTarget", async](Handle, IsVertical, pos);
        }

        /// <summary>
        /// 現在位置を設定します。
        /// WM_HSCROLL、もしくはWM_VSCROLLの通知が発生します。
        /// スクロールのタイプはSB_THUMBTRACKです。
        /// Notify設定がある場合はTRBN_THUMBPOSCHANGINGの通知も発生します。
        /// nReasonはTB_THUMBTRACKです。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="isVertical">縦方向か。</param>
        /// <param name="pos">現在位置。</param>
        private static void EmulateChangePosInTarget(IntPtr handle, bool isVertical, int pos)
        {
            NativeMethods.SetFocus(handle);
            NativeMethods.SendMessage(handle, TBM_SETPOS, new IntPtr(1), new IntPtr(pos));

            //Notify設定がある場合はTRBN_THUMBPOSCHANGINGの通知も発生します
            if ((NativeMethods.GetWindowLongPtr(handle, NativeCommonDefine.GWL_STYLE).ToInt64() & TBS_NOTIFYBEFOREMOVE) == TBS_NOTIFYBEFOREMOVE)
            {
                NMTRBTHUMBPOSCHANGING notify = new NMTRBTHUMBPOSCHANGING();
                EmulateUtility.InitNotify(handle, TRBN_THUMBPOSCHANGING, ref notify.hdr);
                notify.dwPos = pos;//変更予定の値が入る。
                notify.nReason = TB_THUMBTRACK;
                NativeMethods.SendMessage(NativeMethods.GetParent(handle), NativeCommonDefine.WM_NOTIFY, notify.hdr.idFrom, ref notify);
            }

            //設定変更
            NativeMethods.SendMessage(NativeMethods.GetParent(handle),
                (isVertical ? NativeScrollBar.WM_VSCROLL : NativeScrollBar.WM_HSCROLL), NativeDataUtility.MAKELONG(NativeScrollBar.SB_THUMBTRACK, pos), handle);
        }

        /// <summary>
        /// 現在位置を取得。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <returns>現在位置。</returns>
        internal static int GetPosInTarget(IntPtr handle)
        {
            return (int)NativeMethods.SendMessage(handle, TBM_GETPOS, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
