using System;
using System.Drawing;

namespace Codeer.Friendly.Windows.NativeStandardControls.Inside
{
    /// <summary>
    /// ネイティブデータ変換ユーティリティー
    /// </summary>
    static class NativeDataUtility
    {
        /// <summary>
        /// boolからIntPtrへ変換。
        /// </summary>
        /// <param name="value">bool。</param>
        /// <returns>IntPtr。</returns>
        internal static IntPtr ToIntPtr(bool value)
        {
            return new IntPtr(value ? 1 : 0);
        }

        /// <summary>
        /// IntPtrからbooへ変換。
        /// </summary>
        /// <param name="value">IntPtr。</param>
        /// <returns>bool。</returns>
        internal static bool ToBool(IntPtr value)
        {
            return value.ToInt32() != 0;
        }

        /// <summary>
        /// 32bit値を作成します。
        /// </summary>
        /// <param name="lower">下位ワード。</param>
        /// <param name="upper">上位ワード。</param>
        /// <returns>32bit値。</returns>
        internal static IntPtr MAKELONG(int lower, int upper)
        {
            int val = (lower & 0xffff) + ((upper & 0xffff) << 16);
            return new IntPtr(val);
        }

        /// <summary>
        /// 32bit値を作成します。
        /// </summary>
        /// <param name="lower">下位ワード。</param>
        /// <param name="upper">上位ワード。</param>
        /// <returns>32bit値。</returns>
        internal static IntPtr MAKELPARAM(int lower, int upper)
        {
            return MAKELONG(lower, upper);
        }

        /// <summary>
        /// 16bit値を作成します。
        /// </summary>
        /// <param name="lower">下位バイト。</param>
        /// <param name="upper">上位バイト。</param>
        /// <returns>16bit値</returns>
        internal static IntPtr MAKEWORD(int lower, int upper)
        {
            int val = (lower & 0xff) + ((upper & 0xff) << 8);
            return new IntPtr(val);
        }

        /// <summary>
        /// IPアドレス作成。
        /// </summary>
        /// <param name="field0">フィールド0</param>
        /// <param name="field1">フィールド1</param>
        /// <param name="field2">フィールド2</param>
        /// <param name="field3">フィールド3</param>
        /// <returns>IPアドレス。</returns>
        internal static IntPtr MAKEIPADDRESS(byte field0, byte field1, byte field2, byte field3)
        {
            return new IntPtr((field0 << 24) + (field1 << 16) + (field2 << 8) + field3);
        }

        /// <summary>
        /// IPアドレスの範囲を作成。
        /// </summary>
        /// <param name="lower">下限。</param>
        /// <param name="upper">上限。</param>
        /// <returns>IPアドレスの範囲。</returns>
        internal static IntPtr MAKEIPRANGE(byte lower, byte upper)
        {
            return new IntPtr((upper << 8) + lower);
        }
        /// <summary>
        /// RECT→Rectangle変換。
        /// </summary>
        /// <param name="rect">RECT。</param>
        /// <returns>Rectangle。</returns>
        internal static Rectangle ToRectangle(RECT rect)
        {
            return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left + 1, rect.Bottom - rect.Top + 1);
        }

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

        /// <summary>
        /// 上位ワード取得。
        /// </summary>
        /// <param name="value">32bit値。</param>
        /// <returns>上位ワード。</returns>
        internal static short HIWORD(int value)
        {
            return (short)((value & 0xffff0000) >> 16);
        }

        /// <summary>
        /// 下位ワード取得。
        /// </summary>
        /// <param name="value">32bit値。</param>
        /// <returns>下位ワード。</returns>
        internal static short LOWORD(int value)
        {
            return (short)(value & 0xffff); 
        }

        /// <summary>
        /// IntPtr→Colorに変換。
        /// </summary>
        /// <param name="value">IntPtr。</param>
        /// <returns>Color。</returns>
        internal static Color ToColor(IntPtr value)
        {
            int red = value.ToInt32() & 0xff;
            int green = (value.ToInt32() >> 8) & 0xff;
            int blue = (value.ToInt32() >>16) & 0xff;
            return Color.FromArgb(red, green, blue);
        }

        /// <summary>
        /// Color→IntPtrに変換。
        /// </summary>
        /// <param name="color">Color。</param>
        /// <returns>IntPtr。</returns>
        internal static IntPtr ToIntPtr(Color color)
        {
            return new IntPtr(color.R | (color.G <<8 ) | (color.B << 16));
        }
    }
}
