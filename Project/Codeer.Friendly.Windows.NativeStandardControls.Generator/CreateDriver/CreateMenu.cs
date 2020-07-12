using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.Collections.Generic;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    internal class CreateMenu : IWindowAnalysisMenuAction
    {
        public Dictionary<string, MenuAction> GetAction(object target, WindowAnalysisTreeInfo info)
        {
            var dic = new Dictionary<string, MenuAction>();
            if (target is IntPtr handle)
            {
                dic["Pickup Children(&P)"] = () => ControlPicker.PickupChildren(handle);
            }
            return dic;
        }
    }
}
