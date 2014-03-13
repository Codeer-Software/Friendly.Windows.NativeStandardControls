using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's NMLISTVIEW.
    /// </summary>
#else
    /// <summary>
    /// MSDNのNMLISTVIEWを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct NMLISTVIEW
    {
        public NMHDR nmhdr;
        public int iItem;
        public int iSubItem;
        public int uNewState;
        public int uOldState;
        public int uChanged;
        public POINT ptAction;
        public IntPtr lParam;
    }
}
