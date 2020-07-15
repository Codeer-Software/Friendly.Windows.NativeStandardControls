using Codeer.Friendly.Windows.Grasp.Inside.InApp;
using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.CodeDom.Compiler;
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

                //非推奨
                dic["Create Driver (*Obsolete)"] = () =>
                {
                    using (var dom = CodeDomProvider.CreateProvider("CSharp"))
                    {
                        new NativeDriverCreator(dom).CreateDriver(WindowAnalyzer.Analyze(handle, new IOtherSystemWindowAnalyzer[0]));
                    }
                };
                dic["Create Driver Flat (*Obsolete)"] = () =>
                {
                    using (var dom = CodeDomProvider.CreateProvider("CSharp"))
                    {
                        new NativeDriverCreator(dom).CreateDriverFlat(WindowAnalyzer.Analyze(handle, new IOtherSystemWindowAnalyzer[0]));
                    }
                };
            }
            return dic;
        }
    }
}
