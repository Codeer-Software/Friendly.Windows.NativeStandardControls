using System;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's LVIF_...
    /// </summary>
#else
    /// <summary>
    /// MSDNのLVIF_...を参照お願いします。
    /// </summary>
#endif
    [Flags]
    public enum LVIF : int
    {
        TEXT = 0x00000001,
        IMAGE = 0x00000002,
        PARAM = 0x00000004,
        STATE = 0x00000008,
    }
}
