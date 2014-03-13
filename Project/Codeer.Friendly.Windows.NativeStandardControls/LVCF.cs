using System;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's LVCF_...
    /// </summary>
#else
    /// <summary>
    /// MSDNのLVCF_...を参照お願いします。
    /// </summary>
#endif
    [Flags]
    public enum LVCF : int
    {
        FMT = 0x0001,
        WIDTH = 0x0002,
        TEXT = 0x0004,
        SUBITEM = 0x0008,
        IMAGE = 0x0010,
        ORDER = 0x0020,
        MINWIDTH = 0x0040,
        DEFAULTWIDTH = 0x0080,
        IDEALWIDTH = 0x0100,
    }
}
