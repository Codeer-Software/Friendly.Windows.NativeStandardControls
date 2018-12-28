using System;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    internal static class DriverCreatorUtils
    {
        public static string Suffix { get; } = "_Driver";

        public static object OtherArray { get; set; }

        public static string GetTypeName(string driver)
        {
            var sp = driver.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            return sp[sp.Length - 1];
        }

        public static string GetTypeNamespace(string driver)
        {
            var index = driver.LastIndexOf(".");
            if (index == -1) return driver;
            return driver.Substring(0, index);
        }
    }
}
