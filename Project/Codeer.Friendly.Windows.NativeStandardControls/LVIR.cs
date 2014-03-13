using System;
using System.Collections.Generic;
using System.Text;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's LVIR_...
    /// </summary>
#else
    /// <summary>
    /// MSDNのLVIR_...を参照お願いします。
    /// </summary>
#endif
    public enum LVIR
    {
        BOUNDS = 0,
        ICON = 1,
        LABEL = 2,
    }
}
