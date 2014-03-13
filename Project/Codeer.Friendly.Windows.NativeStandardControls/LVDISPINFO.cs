using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's LVDISPINFO.
    /// </summary>
#else
    /// <summary>
    /// MSDNのLVDISPINFOを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct LVDISPINFO
    {
        public NMHDR hdr;
        public LVITEM_CORE item;
    }
}
