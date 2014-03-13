using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's POINT.
    /// </summary>
#else
    /// <summary>
    /// MSDNのPOINTを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }
}
