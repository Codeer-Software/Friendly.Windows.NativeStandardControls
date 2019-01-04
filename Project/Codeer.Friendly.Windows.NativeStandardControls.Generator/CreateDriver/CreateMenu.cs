using Codeer.Friendly.Windows.Grasp.Inside.InApp;
using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    internal class CreateMenu : IWindowAnalysisMenuAction
    {
        public Dictionary<string, MethodInvoker> GetAction(object target, WindowAnalysisTreeInfo info)
        {
            var dic = new Dictionary<string, MethodInvoker>();
            if (target is IntPtr handle)
            {
                dic["Create Driver(&C)"] = () =>
                {
                    using (var dom = CodeDomProvider.CreateProvider("CSharp"))
                    {
                        new NativeDriverCreator(dom).CreateDriver(WindowAnalyzer.Analyze(handle, new IOtherSystemWindowAnalyzer[0]));
                    }
                };
            }
            return dic;
        }
    }
}
