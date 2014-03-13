using System;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.Inside.Hook
{
    /// <summary>
    /// ウィンドウプロックフックオブジェクト。
    /// </summary>
    class MessageHookCallWndProc : HookBase
    {
        /// <summary>
        /// フックタイプ
        /// </summary>
        protected override int HookType { get { return NativeMethods.WH_CALLWNDPROC; } }
        
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
            NativeMethods.CWPSTRUCT messageInfo = (NativeMethods.CWPSTRUCT)Marshal.PtrToStructure
                    (lParam, typeof(NativeMethods.CWPSTRUCT));
            AnalyzeMessage(messageInfo.hwnd, messageInfo.message, messageInfo.wparam, messageInfo.lparam);
            return CallNextHookEx(hookCode, wParam, lParam);
        }
    }
}
