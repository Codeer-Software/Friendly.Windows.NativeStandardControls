using System;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using System.Windows.Forms;
using Codeer.Friendly.Windows.Grasp;
using System.Collections.Generic;

namespace Codeer.Friendly.Windows.NativeStandardControls.Inside
{
    /// <summary>
    /// エミュレートのためのユーティリティーです。
    /// </summary>
    static class EmulateUtility
    {
        /// <summary>
        /// WM_COMMANDを通知します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="handle">ハンドル。</param>
        /// <param name="code">通知コード。</param>
        internal static void SendCommand(WindowsAppFriend app, IntPtr handle, int code)
        {
            app["SendCommand"](handle, code);
        }

        /// <summary>
        /// WM_COMMANDを通知します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="handle">ハンドル。</param>
        /// <param name="code">通知コード。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        internal static void SendCommand(WindowsAppFriend app, IntPtr handle, int code, Async async)
        {
            app["SendCommand", async](handle, code);
        }

        /// <summary>
        /// WM_COMMANDを通知します。
        /// </summary>
        /// <param name="handle">ハンドル。</param>
        /// <param name="code">通知コード。</param>
        internal static void SendCommand(IntPtr handle, int code)
        {
            int dialogId = NativeMethods.GetDlgCtrlID(handle);
            IntPtr parent = NativeMethods.GetParent(handle);
            IntPtr wParam = new IntPtr((dialogId & 0xFFFF) | ((code & 0xFFFF) << 16));
            NativeMethods.SendMessage(NativeMethods.GetParent(handle), NativeCommonDefine.WM_COMMAND, wParam, handle);
        }

        /// <summary>
        /// WM_NOTIFYを通知します。
        /// 対象アプリケーション内部で実行する必要があります。
        /// </summary>
        /// <param name="handle">ハンドル。</param>
        /// <param name="code">通知コード。</param>
        /// <param name="header">ヘッダ。</param>
        internal static void InitNotify(IntPtr handle, int code, ref NMHDR header)
        {
            header.code = code;
            header.hwndFrom = handle;
            header.idFrom = new IntPtr(NativeMethods.GetDlgCtrlID(handle));
        }
    }
}
