using System;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's TCIF_...
    /// </summary>
#else
    /// <summary>
    /// MSDNのTCIF_...を参照お願いします。
    /// </summary>
#endif
    [Flags]
    public enum TCIF : int
    {
        TEXT = 0x0001,
        IMAGE = 0x0002,
        RTLREADING = 0x0004,
        PARAM = 0x0008,
        STATE = 0x0010
    }
}
