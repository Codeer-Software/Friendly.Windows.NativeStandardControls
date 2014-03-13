using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's NMTREEVIEW.
    /// </summary>
#else
    /// <summary>
    /// MSDNのNMTREEVIEWを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct NMTREEVIEW
    {
        public NMHDR hdr;
        public int action;
        public TVITEM itemOld;
        public TVITEM itemNew;
        public POINT ptDrag;
    }
}
