using Codeer.Friendly.Windows.Grasp.Inside.InApp;
using Codeer.TestAssistant.GeneratorToolKit;
using System;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    static class ControlPicker
    {
        internal static void PickupChildren(IntPtr handle)
        {
            var windowInfo = WindowAnalyzer.Analyze(handle, new IOtherSystemWindowAnalyzer[0]);
            foreach (var e in windowInfo.Children)
            {
                if (DriverCreatorAdapter.WindowClassNameAndControlDriver.ContainsKey(e.ClassName))
                {
                    DriverCreatorAdapter.AddDriverElements(e.Handle);
                }
            }
        }
    }
}
