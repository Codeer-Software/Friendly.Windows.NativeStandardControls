using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's TVITEM.
    /// </summary>
#else
    /// <summary>
    /// MSDNのTVITEMを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct TVITEM
    {
        public int mask;
        public IntPtr hItem;
        public int state;
        public int stateMask;
        public IntPtr pszText;
        public int cchTextMax;
        public int iImage;
        public int iSelectedImage;
        public int cChildren;
        public IntPtr lParam;
    }
}
