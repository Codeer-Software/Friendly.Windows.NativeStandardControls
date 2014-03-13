using System;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's TCIS_...
    /// </summary>
#else
    /// <summary>
    /// MSDNのTCIS_...を参照お願いします。
    /// </summary>
#endif
    [Flags]
    public enum TCIS : int
    {
        BUTTONPRESSED = 0x0001,
        HIGHLIGHTED = 0x0002,
    }
}
