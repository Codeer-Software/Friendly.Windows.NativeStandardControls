using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    internal static class NativeMethods
    {
        public const int Ancestor_Parent = 1;
        public const int Ancestor_Root = 2;

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern IntPtr GetAncestor(IntPtr hwnd, int flags);
    }
}
