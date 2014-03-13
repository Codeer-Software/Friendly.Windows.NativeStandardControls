using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's NMTVDISPINFO.
    /// </summary>
#else
    /// <summary>
    /// MSDNのNMTVDISPINFOを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct NMTVDISPINFO
    {
        public NMHDR hdr;
        public TVITEM item;
    }

}
