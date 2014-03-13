using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's NMDATETIMECHANGE.
    /// </summary>
#else
    /// <summary>
    /// MSDNのNMDATETIMECHANGEを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct NMDATETIMECHANGE
    {
        public NMHDR nmhdr;
        public int dwFlags;
        public SYSTEMTIME st;
    }
}
