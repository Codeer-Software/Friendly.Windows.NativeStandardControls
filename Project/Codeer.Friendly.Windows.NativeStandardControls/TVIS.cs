using System;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's TVIS_...
    /// </summary>
#else
    /// <summary>
    /// MSDNのTVIS_...を参照お願いします。
    /// </summary>
#endif
    [Flags]
    public enum TVIS
    {
        SELECTED = 0x0002,
        CUT = 0x0004,
        DROPHILITED = 0x0008,
        BOLD = 0x0010,
        EXPANDED = 0x0020,
        EXPANDEDONCE = 0x0040,
        EXPANDPARTIAL = 0x0080,
    }
}
