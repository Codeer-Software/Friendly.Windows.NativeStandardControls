using System;

namespace NativeStandardControls.TestLib
{
    /// <summary>
    /// OS情報取得ユーティリティー。
    /// </summary>
    static class OSUtility
    {
        /// <summary>
        /// Windows7であるか。
        /// </summary>
        /// <returns>Windows7であるか。</returns>
        internal static bool Is7or8()
        {
            OperatingSystem os = Environment.OSVersion;
            return (os.Platform == System.PlatformID.Win32NT && os.Version.Major == 6 && 1 <= os.Version.Minor);
        }
    }
}
