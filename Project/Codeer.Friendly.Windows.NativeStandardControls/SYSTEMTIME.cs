using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's SYSTEMTIME.
    /// </summary>
#else
    /// <summary>
    /// MSDNのSYSTEMTIMEを参照お願いします。
    /// </summary>
#endif
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    [Serializable]
    public struct SYSTEMTIME
    {
        public short Year;
        public short Month;
        public short DayOfWeek;
        public short Day;
        public short Hour;
        public short Minute;
        public short Second;
        public short Milliseconds;
    }
}
