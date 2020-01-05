using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    internal static class NativeMethods
    {
        public const int Ancestor_Parent = 1;
        public const int Ancestor_Root = 2;

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr GetAncestor(IntPtr hwnd, int flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetParent(IntPtr hWnd);
    }
}
