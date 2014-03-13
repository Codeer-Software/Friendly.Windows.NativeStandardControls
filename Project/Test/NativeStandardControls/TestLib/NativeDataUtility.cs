using System;
using Codeer.Friendly.Windows.NativeStandardControls;

namespace NativeStandardControls.TestLib
{
    /// <summary>
    /// ネイティブデータ変換ユーティリティー
    /// </summary>
    static class NativeDataUtility
    {
        /// <summary>
        /// DateTime→SYSTEMTIME変換。
        /// </summary>
        /// <param name="time">DateTime。</param>
        /// <returns>SYSTEMTIME。</returns>
        internal static SYSTEMTIME ToSYSTEMTIME(DateTime time)
        {
            SYSTEMTIME system = new SYSTEMTIME();
            time = time.ToLocalTime();
            system.Year = Convert.ToInt16(time.Year);
            system.Month = Convert.ToInt16(time.Month);
            system.DayOfWeek = Convert.ToInt16(time.DayOfWeek);
            system.Day = Convert.ToInt16(time.Day);
            system.Hour = Convert.ToInt16(time.Hour);
            system.Minute = Convert.ToInt16(time.Minute);
            system.Second = Convert.ToInt16(time.Second);
            system.Milliseconds = Convert.ToInt16(time.Millisecond);
            return system;
        }

        /// <summary>
        /// SYSTEMTIME→DateTime変換。
        /// </summary>
        /// <param name="time">SYSTEMTIME。</param>
        /// <returns>DateTime。</returns>
        internal static DateTime ToDateTime(SYSTEMTIME time)
        {
            return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second, time.Milliseconds, DateTimeKind.Local);
        }
    }
}
