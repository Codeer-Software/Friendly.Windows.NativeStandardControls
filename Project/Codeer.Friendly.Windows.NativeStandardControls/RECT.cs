using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's RECT.
    /// </summary>
#else
    /// <summary>
    /// MSDNのRECTを参照お願いします。
    /// </summary>
#endif
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
