using System;
using System.Runtime.InteropServices;
using System.Text;
using Codeer.Friendly.Windows.NativeStandardControls;
using System.Diagnostics.CodeAnalysis;

namespace NativeStandardControls.TestLib
{
    /// <summary>
    /// ネイティブDLLメソッド
    /// </summary>
    static class NativeMethods
    {
        /// <summary>
        /// WM_COMMAND通知
        /// </summary>
        internal const int WM_COMMAND = 0x0111;

        /// <summary>
        /// WM_NOTIFY通知
        /// </summary>
        internal const int WM_NOTIFY = 0x004E;

        /// <summary>
        /// WM_VSCROLL通知
        /// </summary>
        internal const int WM_HSCROLL = 0x0114;

        /// <summary>
        /// WM_HSCROLL
        /// </summary>
        internal const int WM_VSCROLL = 0x0115;

        /// <summary>
        /// ウィンドウが正常であるかの確認。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>正常であるか。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindow(IntPtr hWnd);

        /// <summary>
        /// ユニコードウィンドウであるか。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>ユニコードウィンドウであるか。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowUnicode(IntPtr hWnd);

        /// <summary>
        /// クラス名称を取得。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="lpClassName">クラス名称格納バッファ。</param>
        /// <param name="nMaxCount">最大文字数。</param>
        /// <returns>文字サイズ。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowEnabled(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetFocus(IntPtr hWnd);
        /// <summary>
        /// 指定されたウィンドウに関する情報を取得。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="nIndex">操作種別。</param>
        /// <returns>結果。</returns>
        internal static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
            {
                return GetWindowLongPtr64(hWnd, nIndex);
            }
            else
            {
                return new IntPtr(GetWindowLongPtr32(hWnd, nIndex));
            }
        }

        /// <summary>
        /// 指定されたウィンドウに関する情報を取得。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="nIndex">操作種別。</param>
        /// <returns>結果</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        /// <summary>
        /// 指定されたウィンドウに関する情報を取得。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="nIndex">操作種別。</param>
        /// <returns>結果</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        static extern int GetWindowLongPtr32(IntPtr hWnd, int nIndex);

        /// <summary>
        /// 指定されたウィンドウに関する情報を設定。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="nIndex">操作種別。</param>
        /// <param name="dwNewLong">情報。</param>
        /// <returns>設定前の情報。</returns>
        internal static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
            {
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            }
            else
            {
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
            }
        }

        /// <summary>
        /// 指定されたウィンドウに関する情報を設定。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="nIndex">操作種別。</param>
        /// <param name="dwNewLong">情報。</param>
        /// <returns>設定前の情報。</returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// 指定されたウィンドウに関する情報を設定。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="nIndex">操作種別。</param>
        /// <param name="dwNewLong">情報。</param>
        /// <returns>設定前の情報。</returns>
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
    }
}
