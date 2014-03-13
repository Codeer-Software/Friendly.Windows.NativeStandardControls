using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's TVHITTESTINFO.
    /// </summary>
#else
    /// <summary>
    /// MSDNのTVHITTESTINFOを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct TVHITTESTINFO
    {
        public POINT pt;
        public int flags;
        public IntPtr hItem;
    }
}
