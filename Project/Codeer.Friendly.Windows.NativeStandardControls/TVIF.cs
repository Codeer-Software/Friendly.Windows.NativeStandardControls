using System;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's TVIF_...
    /// </summary>
#else
    /// <summary>
    /// MSDNのTVIF_...を参照お願いします。
    /// </summary>
#endif
    [Flags]
    public enum TVIF : int
    {
        TEXT = 0x0001,
        IMAGE = 0x0002,
        PARAM = 0x0004,
        STATE = 0x0008,
        HANDLE = 0x0010,
        SELECTEDIMAGE = 0x0020,
        CHILDREN = 0x0040,
        INTEGRAL = 0x0080,
        STATEEX = 0x0100,
        EXPANDEDIMAGE = 0x0200,
    }
}
