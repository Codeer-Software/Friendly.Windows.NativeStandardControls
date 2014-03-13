using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's NMTRBTHUMBPOSCHANGING.
    /// </summary>
#else
    /// <summary>
    /// MSDNのNMTRBTHUMBPOSCHANGINGを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct NMTRBTHUMBPOSCHANGING
    {
        public NMHDR hdr;
        public int dwPos;
        public int nReason;
    }
}
