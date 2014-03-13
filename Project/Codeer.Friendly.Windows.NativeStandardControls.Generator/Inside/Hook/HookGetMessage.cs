using System;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.Inside.Hook
{
    /// <summary>
    /// メッセージフックオブジェクト。
    /// </summary>
    class MessageHookGetMessage : HookBase
    {
        /// <summary>
        /// フックタイプ
        /// </summary>
        protected override int HookType { get { return NativeMethods.WH_GETMESSAGE; } }

        /// <summary>
        /// ウィンドウプロックフック。
        /// </summary>
        /// <param name="hookCode">フックコード。</param>
        /// <param name="wParam">WPARAM。</param>
        /// <param name="lParam">LPARAM。</param>
        /// <returns>戻り値。</returns>
        protected override IntPtr WindowProcHook(int hookCode, IntPtr wParam, IntPtr lParam)
        {
            if (hookCode < 0)
            {
                return CallNextHookEx(hookCode, wParam, lParam);
            }
            if (wParam.ToInt32() == 1)
            {
                NativeMethods.MSG messageInfo = (NativeMethods.MSG)Marshal.PtrToStructure
                        (lParam, typeof(NativeMethods.MSG));
                AnalyzeMessage(messageInfo.hwnd, messageInfo.message, messageInfo.wparam, messageInfo.lparam);
            }
            return CallNextHookEx(hookCode, wParam, lParam);
        }
    }
}
