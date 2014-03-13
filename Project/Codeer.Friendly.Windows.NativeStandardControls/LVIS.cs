using System;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's LVIS_...
    /// </summary>
#else
    /// <summary>
    /// MSDNのLVIS_...を参照お願いします。
    /// </summary>
#endif
    [Flags]
    public enum LVIS : int
    {
        FOCUSED = 0x0001,
        SELECTED = 0x0002,
        CUT = 0x0004,
        DROPHILITED = 0x0008,
        GLOW = 0x0010,
        ACTIVATING = 0x0020,
    }
}
