using System;
using System.Text;
using System.Globalization;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type msctls_updown32.
    /// </summary>
#else
    /// <summary>
    /// WindowClassがmsctls_updown32のウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    public class NativeSpinButton : NativeWindow
    {
        const int UDM_GETRANGE = (NativeCommonDefine.WM_USER + 102);
        const int UDM_GETBUDDY = (NativeCommonDefine.WM_USER + 106);
        const int UDM_SETPOS32 = (NativeCommonDefine.WM_USER + 113);
        const int UDM_GETPOS32 = (NativeCommonDefine.WM_USER + 114);
        const int UDN_FIRST = -721;
        const int UDN_DELTAPOS = UDN_FIRST - 1;
        const int SB_THUMBPOSITION = 4;
        const int SB_ENDSCROLL = 8;
        
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
        public NativeSpinButton(WindowControl src)
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
        public NativeSpinButton(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App, GetType());
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
        /// Obtains the selected range.
        /// </summary>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
#else
        /// <summary>
        /// 範囲を取得します。
        /// </summary>
        /// <param name="min">下限。</param>
        /// <param name="max">上限。</param>
#endif
        public void GetRange(ref int min, ref int max)
        {
            int ret = (int)SendMessage(UDM_GETRANGE, IntPtr.Zero, IntPtr.Zero);
            min = NativeDataUtility.HIWORD(ret);
            max = NativeDataUtility.LOWORD(ret);
        }
        
#if ENG
        /// <summary>
        /// Sets the current position.
        /// Produces a UDN_DELTAPOS notification.
        /// Also produces EN_CHANGE and EN_UPDATE notifications as a pair.
        /// Also produces a WM_VSCROLL notification.
        /// </summary>
        /// <param name="pos">Position to set.</param>
#else
        /// <summary>
        /// 現在位置を設定します。
        /// UDN_DELTAPOSの通知が発生します。
        /// また、ペアとなるEditにEN_CHANGE、EN_UPDATEの通知が発生します。
        /// また、WM_VSCROLLの通知も発生します。
        /// </summary>
        /// <param name="pos">現在位置。</param>
#endif
        public void EmulateChangePos(int pos)
        {
            App[GetType(), "EmulateChangePosInTarget"](Handle, pos);
        }
        
#if ENG
        /// <summary>
        /// Sets the current position.
        /// Produces a UDN_DELTAPOS notification.
        /// Also produces EN_CHANGE and EN_UPDATE notifications as a pair.
        /// Also produces a WM_VSCROLL notification.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="pos">Position to set.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 現在位置を設定します。
        /// UDN_DELTAPOSの通知が発生します。
        /// また、ペアとなるEditにEN_CHANGE、EN_UPDATEの通知が発生します。
        /// また、WM_VSCROLLの通知も発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="pos">現在位置。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangePos(int pos, Async async)
        {
            App[GetType(), "EmulateChangePosInTarget", async](Handle, pos);
        }

        /// <summary>
        /// 現在位置を設定します。
        /// 現在の設定と異なる場合、UDN_DELTAPOSの通知が発生します。
        /// また、ペアとなるEditにEN_CHANGE、EN_UPDATEの通知が発生します。
        /// また、WM_VSCROLLの通知も発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="pos">現在位置。</param>
        private static void EmulateChangePosInTarget(IntPtr handle, int pos)
        {
            NativeMethods.SetFocus(handle);
            int currentPos = GetPosInTarget(handle);
            if (currentPos == pos)
            {
                return;
            }

            //位置変更
            NativeMethods.SendMessage(handle, UDM_SETPOS32, IntPtr.Zero, NativeDataUtility.MAKELPARAM(pos, 0));

            //通知
            NMUPDOWN info = new NMUPDOWN();
            EmulateUtility.InitNotify(handle, UDN_DELTAPOS, ref info.hdr);
            info.iPos = pos;
            info.iDelta= pos - currentPos;
            NativeMethods.SendMessage(NativeMethods.GetParent(handle), NativeCommonDefine.WM_NOTIFY, info.hdr.idFrom, ref info);

            //WM_VSCROLL
            NativeMethods.SendMessage(NativeMethods.GetParent(handle), NativeCommonDefine.WM_VSCROLL, NativeDataUtility.MAKELONG(SB_THUMBPOSITION, pos), handle);
            NativeMethods.SendMessage(NativeMethods.GetParent(handle), NativeCommonDefine.WM_VSCROLL, NativeDataUtility.MAKELONG(SB_ENDSCROLL, pos), handle);
        }

        /// <summary>
        /// 現在位置を取得。
        /// </summary>
        /// <param name="handle">ハンドル。</param>
        /// <returns>現在位置。</returns>
        internal static int GetPosInTarget(IntPtr handle)
        {
            int isErr = 0;
            int pos = (int)NativeMethods.SendMessage(handle, UDM_GETPOS32, IntPtr.Zero, ref isErr);
            if (isErr == 0)
            {
                return pos;
            }

            IntPtr buddy = NativeMethods.SendMessage(handle, UDM_GETBUDDY, IntPtr.Zero, IntPtr.Zero);
            if (buddy != IntPtr.Zero)
            {
                int len = NativeMethods.GetWindowTextLength(buddy);
                StringBuilder builder = new StringBuilder((len + 1) * 8);
                NativeMethods.GetWindowText(buddy, builder, len * 8);
                int val = 0;
                return int.TryParse(builder.ToString(), NumberStyles.Number, CultureInfo.CurrentCulture, out val) ? val : 0;
            }
            throw new NotSupportedException(ResourcesLocal.Instance.SpinButtonHasNoBuddy);
        }
    }
}
