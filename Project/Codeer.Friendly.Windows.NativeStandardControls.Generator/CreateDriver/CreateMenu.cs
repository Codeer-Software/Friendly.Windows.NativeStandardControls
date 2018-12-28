using Codeer.TestAssistant.GeneratorToolKit;
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
            if (target is Control ctrl)
            {
                dic["Create Driver(&C)"] = () =>
                {
                    using (var dom = CodeDomProvider.CreateProvider("CSharp"))
                    {
                        object windowInfo = ReflectionAccessor.InvokeStaticMethod("Codeer.Friendly.Windows.Grasp.Inside.InApp.WindowAnalyzer", "Analyze", target, DriverCreatorUtils.OtherArray);
                        new NativeDriverCreator(dom).CreateDriver(windowInfo);
                    }
                };
            }
            return dic;
        }
    }
}
