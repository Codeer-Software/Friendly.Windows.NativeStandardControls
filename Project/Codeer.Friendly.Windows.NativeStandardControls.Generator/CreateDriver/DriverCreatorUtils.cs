﻿using Codeer.Friendly.Windows.Grasp.Inside.InApp;
using Codeer.TestAssistant.GeneratorToolKit;
using System;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    static class DriverCreatorUtils
    {
        internal static string Suffix { get; } = "_Driver";

        internal static string GetTypeName(string driver)
        {
            var sp = driver.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            return sp[sp.Length - 1];
        }

        internal static string GetTypeNamespace(string driver)
        {
            var index = driver.LastIndexOf(".");
            if (index == -1) return driver;
            return driver.Substring(0, index);
        }

        internal static string GetDriverTypeName(IntPtr handle)
        {
            var windowInfo = WindowAnalyzer.Analyze(handle, new IOtherSystemWindowAnalyzer[0]);
            if (DriverCreatorAdapter.WindowClassNameAndWindowDriver.TryGetValue(windowInfo.ClassName, out var driverByClass)) return driverByClass.DriverTypeFullName;
            if (DriverCreatorAdapter.WindowTextAndWindowDriver.TryGetValue(windowInfo.ClassName, out var driverWindowText)) return driverWindowText.DriverTypeFullName;
            return string.Empty;
        }
    }
}
