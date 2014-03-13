using System;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type ScrollBar.
    /// </summary>
#else
    /// <summary>
    /// WindowClassがScrollBarのウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    public class NativeScrollBar : NativeWindow
    {
        internal const int SB_CTL = 2;
        internal const int SBS_VERT = 1;
        internal const int WM_HSCROLL = 0x0114;
        internal const int WM_VSCROLL = 0x0115;
        internal const int SB_THUMBPOSITION = 4;
        internal const int SB_THUMBTRACK = 5;
        internal const int SB_ENDSCROLL = 8;
        
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
        public NativeScrollBar(WindowControl src)
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
        public NativeScrollBar(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App, GetType());
        }
        
#if ENG
        /// <summary>
        /// Returns true if the scroll bar is vertical.
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
                return ((long)(IntPtr)App[typeof(NativeMethods), "GetWindowLongPtr"](Handle, NativeCommonDefine.GWL_STYLE).Core & SBS_VERT) != 0;
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the current position of the scroll bar.
        /// </summary>
#else
        /// <summary>
        /// スクロールバーの現在位置です。
        /// </summary>
#endif
        public int ScrollPos
        {
            get
            {
                return (int)App[typeof(NativeMethods), "GetScrollPos"](Handle, SB_CTL).Core;
            }
        }
        
#if ENG
        /// <summary>
        /// Obtains the scroll bar's scroll range.
        /// </summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
#else
        /// <summary>
        /// スクロールの範囲を取得します。
        /// </summary>
        /// <param name="min">下限。</param>
        /// <param name="max">上限。</param>
#endif
        public void GetScrollRange(ref int min, ref int max)
        {
            AppVar nativeMin = App.Dim((int)0);
            AppVar nativeMax = App.Dim((int)0);
            App[typeof(NativeMethods), "GetScrollRange"](Handle, SB_CTL, nativeMin, nativeMax);
            min = (int)nativeMin.Core;
            max = (int)nativeMax.Core;
        }
        
#if ENG
        /// <summary>
        /// Sets the current scroll position.
        /// Produces a WM_HSCROLL or WM_VSCROLL notification.
        /// Uses SB_THUMBTRACK type scrolling.
        /// </summary>
        /// <param name="pos">The position to set</param>
#else
        /// <summary>
        /// スクロールバーの現在位置を設定のための通知を発生させます。
        /// WM_HSCROLL、もしくはWM_VSCROLLの通知が発生します。
        /// スクロールのタイプはSB_THUMBTRACKです。
        /// </summary>
        /// <param name="pos">現在位置。</param>
#endif
        public void EmulateScroll(int pos)
        {
            App[GetType(), "EmulateScrollInTarget"](Handle, IsVertical, pos);
        }
        
#if ENG
        /// <summary>
        /// Sets the current scroll position.
        /// Produces a WM_HSCROLL or WM_VSCROLL notification.
        /// Uses SB_THUMBTRACK type scrolling.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="pos">The position to set</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// スクロールバーの現在位置を設定します。
        /// WM_HSCROLL、もしくはWM_VSCROLLの通知が発生します。
        /// スクロールのタイプはSB_THUMBTRACKです。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="pos">現在位置。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateScroll(int pos, Async async)
        {
            App[GetType(), "EmulateScrollInTarget", async](Handle, IsVertical, pos);
        }
                
        /// <summary>
        /// スクロールバーの現在位置を設定のための通知を発生させます。
        /// WM_HSCROLL、もしくはWM_VSCROLLの通知が発生します。
        /// スクロールのタイプはSB_THUMBTRACKです。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="isVertical">縦スクロールバーであるか。</param>
        /// <param name="pos">現在位置。</param>
        private static void EmulateScrollInTarget(IntPtr handle, bool isVertical, int pos)
        {
            NativeMethods.SetFocus(handle);
            int message = isVertical ? WM_VSCROLL : WM_HSCROLL;
            NativeMethods.SendMessage(NativeMethods.GetParent(handle), message, NativeDataUtility.MAKELONG(SB_THUMBTRACK, pos), handle);
            NativeMethods.SendMessage(NativeMethods.GetParent(handle), message, NativeDataUtility.MAKELONG(SB_THUMBPOSITION, pos), handle);
            NativeMethods.SendMessage(NativeMethods.GetParent(handle), message, NativeDataUtility.MAKELONG(SB_ENDSCROLL, 0), handle);
        }
    }
}
