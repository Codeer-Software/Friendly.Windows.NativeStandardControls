using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.Grasp.Inside;
using Codeer.Friendly.Windows.Grasp.Inside.InApp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    public class DriverDesigner : IDriverDesigner
    {
        const string Indent = "    ";
        const string TodoComment = "// TODO It is not the best way to identify. Please change to a better method.";
        const string WindowsAppFriendTypeFullName = "Codeer.Friendly.Windows.WindowsAppFriend";
        const string AttachByClassName = "Class Name";
        const string AttachByWindowText = "Window Text";
        const string AttachVariableWindowText = "Variable Window Text";
        const string AttachCustom = "Custom";

        public int Priority { get; }

        public bool CanDesign(object obj) => obj is IntPtr;

        public string CreateDriverClassName(object root)
        {
            using (var dom = CodeDomProvider.CreateProvider("CSharp"))
            {
                var buf = new StringBuilder(1024);
                NativeMethods.GetWindowText((IntPtr)root, buf, buf.Capacity);
                return new NativeDriverCreator(dom).MakeDriverName(buf.ToString(), new List<string>());
            }
        }

        public DriverIdentifyInfo[] GetIdentifyingCandidates(object root, object element)
        {
            using (var dom = CodeDomProvider.CreateProvider("CSharp"))
            {
                var customNameGenerator = new DriverElementNameGeneratorAdaptor(dom);
                return GetIdentifyingCandidatesCore(customNameGenerator, root, element);
            }
        }

        public string[] GetAttachExtensionClassCandidates(object obj)
        {
            if (!(obj is IntPtr elementHandle)) return new string[0];

            var candidates = new List<string>();

            if (NativeMethods.GetAncestor(elementHandle, NativeMethods.Ancestor_Root) != elementHandle)
            {
                var parent = NativeMethods.GetParent(elementHandle);
                while (parent != IntPtr.Zero)
                {
                    var driver = DriverCreatorUtils.GetDriverTypeName(parent);
                    if (!string.IsNullOrEmpty(driver))
                    {
                        candidates.Add(driver);
                    }
                    parent = NativeMethods.GetParent(parent);
                }
                if (DriverCreatorAdapter.MultiTypeFullNameAndUserControlDriver.TryGetValue(string.Empty, out var nativeUserControls))
                {
                    foreach (var x in nativeUserControls)
                    {
                        candidates.Add(x.DriverTypeFullName);
                    }
                }
                foreach (var x in DriverCreatorAdapter.MultiWindowClassNameAndWindowDriver)
                {
                    foreach (var y in x.Value)
                    {
                        if (!candidates.Contains(y.DriverTypeFullName))
                        {
                            candidates.Add(y.DriverTypeFullName);
                        }
                    }
                }
                foreach (var x in DriverCreatorAdapter.MultiWindowTextAndWindowDriver)
                {
                    foreach (var y in x.Value)
                    {
                        if (!candidates.Contains(y.DriverTypeFullName))
                        {
                            candidates.Add(y.DriverTypeFullName);
                        }
                    }
                }
            }
            candidates.Add(WindowsAppFriendTypeFullName);
            return candidates.ToArray();
        }

        public string[] GetAttachMethodCandidates(object obj)
        {
            var candidates = new List<string>();
            candidates.Add(AttachByWindowText);
            candidates.Add(AttachByClassName);
            candidates.Add(AttachVariableWindowText);
            candidates.Add(AttachCustom);
            return candidates.ToArray();
        }

        internal static void CreateControlDriver(IntPtr handle)
        {
            var info = WindowAnalyzer.Analyze(handle, new IOtherSystemWindowAnalyzer[0]);

            var driverName = info.ClassName + "Driver";
            using (var dom = CodeDomProvider.CreateProvider("CSharp"))
            {
                driverName = new NativeDriverCreator(dom).MakeDriverName(driverName, new List<string>(), "Control");
            }

            var generatorName = driverName + "Generator";

            var driverCode = @"using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.TestAssistant.GeneratorToolKit;
using Codeer.Friendly.Windows.NativeStandardControls;

namespace [*namespace]
{
    [ControlDriver(WindowClassName = ""{className}"", Priority = 2)]
    public class {driverName} : NativeWindow
    {
        public {driverName}(WindowControl window)
            : base(window) { }
    }
}
";
            DriverCreatorAdapter.AddCode($"{driverName}.cs", driverCode.Replace("{className}", info.ClassName).Replace("{driverName}", driverName), handle);

            var generatorCode = @"using System;
using Codeer.TestAssistant.GeneratorToolKit;
using Codeer.Friendly.Windows.NativeStandardControls.Generator;

namespace [*namespace]
{
    [CaptureCodeGenerator(""[*namespace.{driverName}]"")]
    public class {generatorName} : NativeGeneratorBase
    {
        protected override void AnalyzeMessage(int message, IntPtr wparam, IntPtr lparam)
        {
        }

        protected override void Detach()
        {
        }
    }
}
";
            DriverCreatorAdapter.AddCode($"{generatorName}.cs", generatorCode.Replace("{generatorName}", generatorName).Replace("{driverName}", driverName), handle);
        }

        DriverIdentifyInfo[] GetIdentifyingCandidatesCore(DriverElementNameGeneratorAdaptor customNameGenerator, object root, object element)
        {
            if (!(root is IntPtr rootHandle)) return new DriverIdentifyInfo[0];
            if (!(element is IntPtr elementHandle)) return new DriverIdentifyInfo[0];

            var handles = new List<IntPtr>();
            handles.Add(elementHandle);
            var current = NativeMethods.GetParent(elementHandle);
            while (current != IntPtr.Zero)
            {
                handles.Insert(0, current);
                if (current == rootHandle) break;
                current = NativeMethods.GetParent(current);
            }
            if (current != rootHandle) return new DriverIdentifyInfo[0];

            var parentInfoSrc = WindowAnalyzer.Analyze(rootHandle, new IOtherSystemWindowAnalyzer[0]);
            var parentInfo = parentInfoSrc;
            var target = elementHandle;
            var isPerfect = true;
            var accessPaths = new List<string>();
            for (int i = 0; i < handles.Count - 1; i++)
            {
                var parentHandle = handles[i];
                var childHandle = handles[i + 1];
                var dialogIds = new List<int>();
                WindowInfo childInfo = null;
                foreach (var c in parentInfo.Children)
                {
                    if (c.Handle == childHandle) childInfo = c;
                    dialogIds.Add(c.DialogId);
                }
                if (childInfo == null) return new DriverIdentifyInfo[0];

                if (1 == CollectionUtility.Where(dialogIds, x => x == childInfo.DialogId).Count)
                {
                    accessPaths.Add($"IdentifyFromDialogId({ childInfo.DialogId})");
                }
                else if (1 == CollectionUtility.Where(parentInfo.Children, x => x.Text == childInfo.Text).Count)
                {
                    //先頭から特定できる場合
                    if (GetSameWindowTextCount(childInfo.Text, parentInfoSrc) == 1)
                    {
                        accessPaths.Clear();
                        accessPaths.Add($"IdentifyFromWindowText({NativeEditGenerator.AdjustText(childInfo.Text)})");
                    }
                    else
                    {
                        accessPaths.Add($"GetChildren().Where(e => e.GetWindowText() == { NativeEditGenerator.AdjustText(childInfo.Text)}).Single()");
                    }
                }
                else
                {
                    isPerfect = false;
                    if (childInfo.ZIndex.Length <= i) return new DriverIdentifyInfo[0];
                    accessPaths.Add($"IdentifyFromZIndex({ childInfo.ZIndex[i]})");
                }
                parentInfo = childInfo;
            }

            string typeName = "window";
            if (DriverCreatorAdapter.WindowClassNameAndControlDriver.TryGetValue(parentInfo.ClassName, out var ctrlDriver))
            {
                typeName = DriverCreatorUtils.GetTypeName(ctrlDriver.ControlDriverTypeFullName);
            }
            var name = customNameGenerator.MakeDriverPropName(elementHandle, typeName, new List<string>());
            var accessPath = string.Join(".", accessPaths.ToArray());
            return new[]
            {
                new DriverIdentifyInfo
                {
                    IsPerfect = isPerfect,
                    Identify = "Core." + accessPath + ".Convert()",
                    DefaultName = name,
                    ExtensionUsingNamespaces = new string[0],
                    DriverTypeCandidates = DriverCreatorUtils.GetControlDriverTypeFullNames(WindowAnalyzer.Analyze(elementHandle, new IOtherSystemWindowAnalyzer[0]))
                }
            };
        }

        static int GetSameWindowTextCount(string text, WindowInfo parentInfoSrc)
        {
            int count = 0;
            if (parentInfoSrc.Text == text)
            {
                count++;
            }
            foreach (var e in parentInfoSrc.Children)
            {
                count += GetSameWindowTextCount(text, e);
            }
            return count;
        }

        public void GenerateCode(object targetControl, DriverDesignInfo info)
        {
            var code = GenerateCodeCore((IntPtr)targetControl, info);
            var fileName = $"{info.ClassName}.cs";
            DriverCreatorAdapter.AddCode(fileName, code, targetControl);

            //行選択でのツリーとの連動用
            foreach (var e in info.Properties)
            {
                DriverCreatorAdapter.AddCodeLineSelectInfo(fileName, e.Identify, e.Element);
            }
        }

        string GenerateCodeCore(IntPtr targetControl, DriverDesignInfo info)
        {
            //クラス定義部分
            var classDefine = GenerateClassDefine(targetControl, info, out var memberUsings);

            //拡張メソッド部分
            var extentionsDefine = GenerateExtensions(targetControl, info, out var extensionUsings);

            //using
            var usings = new List<string>();
            DistinctAddRange(new[]
                    {
                        "Codeer.TestAssistant.GeneratorToolKit",
                        "Codeer.Friendly.Windows.NativeStandardControls",
                        "Codeer.Friendly.Windows.Grasp",
                        "Codeer.Friendly.Windows",
                        "Codeer.Friendly.Dynamic",
                        "Codeer.Friendly",
                        "System.Linq"
                    }, usings);
            DistinctAddRange(memberUsings, usings);
            DistinctAddRange(extensionUsings, usings);
            usings.Remove(DriverCreatorAdapter.SelectedNamespace);
            usings.Sort();

            //コード作成
            var code = new List<string>();
            foreach (var e in usings)
            {
                code.Add($"using {e};");
            }
            code.Add(string.Empty);
            code.Add($"namespace {DriverCreatorAdapter.SelectedNamespace}");
            code.Add("{");
            code.AddRange(classDefine);
            code.AddRange(extentionsDefine);
            code.Add("}");
            return string.Join(Environment.NewLine, code.ToArray());
        }

        static List<string> GenerateClassDefine(IntPtr targetControl, DriverDesignInfo info, out List<string> usings)
        {
            GetMembers(info, out usings, out var members);

            var code = new List<string>();

            var windowInfo = WindowAnalyzer.Analyze(targetControl, new IOtherSystemWindowAnalyzer[0]);
            var isTopLevel = NativeMethods.GetAncestor(windowInfo.Handle, NativeMethods.Ancestor_Root) == windowInfo.Handle;

            var attr = isTopLevel ? "WindowDriver" : "UserControlDriver";
            code.Add($"{Indent}[{attr}]");
            code.Add($"{Indent}public class {info.ClassName}");
            code.Add($"{Indent}{{");
            code.Add($"{Indent}{Indent}public WindowControl Core {{ get; }}");
            foreach (var e in members)
            {
                code.Add($"{Indent}{Indent}{e}");
            }
            code.Add(string.Empty);
            code.Add($"{Indent}{Indent}public {info.ClassName}(WindowControl core)");
            code.Add($"{Indent}{Indent}{{");
            code.Add($"{Indent}{Indent}{Indent}Core = core;");
            code.Add($"{Indent}{Indent}}}");
            code.Add($"{Indent}}}");

            return code;
        }

        static List<string> GenerateExtensions(IntPtr targetControl, DriverDesignInfo info, out List<string> usings)
        {
            var code = new List<string>();
            usings = new List<string>();

            if (!info.CreateAttachCode) return code;

            var windowInfo = WindowAnalyzer.Analyze(targetControl, new IOtherSystemWindowAnalyzer[0]);
            var isTopLevel = NativeMethods.GetAncestor(windowInfo.Handle, NativeMethods.Ancestor_Root) == windowInfo.Handle;

            code.Add(string.Empty);
            code.Add($"{Indent}public static class {info.ClassName}Extensions");
            code.Add($"{Indent}{{");

            var funcName = GetAttachFuncName(info.ClassName);

            //WindowsAppFriendにアタッチする場合
            if (info.AttachExtensionClass == WindowsAppFriendTypeFullName)
            {
                if (isTopLevel)
                {
                    if (info.AttachMethod == AttachCustom)
                    {
                        code.Add($"{Indent}{Indent}[WindowDriverIdentify(CustomMethod = \"TryGet\")]");
                        code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app, T identifier)");
                        code.Add($"{Indent}{Indent}{{");
                        code.Add($"{Indent}{Indent}{Indent}//TODO");
                        code.Add($"{Indent}{Indent}}}");
                        code.Add(string.Empty);
                        code.Add($"{Indent}{Indent}public static bool TryGet(WindowControl window, out T identifier)");
                        code.Add($"{Indent}{Indent}{{");
                        code.Add($"{Indent}{Indent}{Indent}//TODO");
                        code.Add($"{Indent}{Indent}}}");
                    }
                    else if (info.AttachMethod == AttachVariableWindowText)
                    {
                        code.Add($"{Indent}{Indent}[WindowDriverIdentify(CustomMethod = \"TryGet\")]");
                        code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app, string text)");
                        code.Add($"{Indent}{Indent}{Indent}=> app.WaitForIdentifyFromWindowText(\"{windowInfo.Text}\").Convert();");
                        code.Add(string.Empty);
                        code.Add($"{Indent}{Indent}public static bool TryGet(WindowControl window, out string text)");
                        code.Add($"{Indent}{Indent}{{");
                        code.Add($"{Indent}{Indent}{Indent}text = window.GetWindowText();");
                        code.Add($"{Indent}{Indent}{Indent}return window.WindowClassName == \"{windowInfo.ClassName}\";");
                        code.Add($"{Indent}{Indent}}}");
                    }
                    else
                    {
                        if (info.AttachMethod == AttachByClassName)
                        {
                            code.Add($"{Indent}{Indent}[WindowDriverIdentify(WindowClass = \"{windowInfo.ClassName}\")]");
                            code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app)");
                            code.Add($"{Indent}{Indent}{Indent}=> app.WaitForIdentifyFromWindowClass(\"{windowInfo.ClassName}\").Convert();");
                        }
                        else
                        {
                            code.Add($"{Indent}{Indent}[WindowDriverIdentify(WindowText = \"{windowInfo.Text}\")]");
                            code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app)");
                            code.Add($"{Indent}{Indent}{Indent}=> app.WaitForIdentifyFromWindowText(\"{windowInfo.Text}\").Convert();");
                        }
                    }
                }
                //UserControl
                else
                {
                    if (info.AttachMethod == AttachCustom)
                    {
                        code.Add($"{Indent}{Indent}[UserControlDriverIdentify(CustomMethod = \"TryGet\")]");
                        code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app, T identifier)");
                        code.Add($"{Indent}{Indent}{{");
                        code.Add($"{Indent}{Indent}{Indent}//TODO");
                        code.Add($"{Indent}{Indent}}}");
                        code.Add(string.Empty);
                        code.Add($"{Indent}{Indent}public static T[] TryGet(this WindowsAppFriend app)");
                        code.Add($"{Indent}{Indent}{{");
                        code.Add($"{Indent}{Indent}{Indent}//TODO");
                        code.Add($"{Indent}{Indent}}}");
                    }
                    else if (info.AttachMethod == AttachVariableWindowText)
                    {
                        code.Add($"{Indent}{Indent}[UserControlDriverIdentify(CustomMethod = \"TryGet\")]");
                        code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app, string text)");
                        code.Add($"{Indent}{Indent}{Indent}=> app.GetTopLevelWindows().SelectMany(e => e.GetFromWindowText(text)).FirstOrDefault()?.Convert();");
                        code.Add(string.Empty);
                        code.Add($"{Indent}{Indent}public static string[] TryGet(this WindowsAppFriend app)");
                        code.Add($"{Indent}{Indent}{Indent}=> app.GetTopLevelWindows().SelectMany(e => e.GetFromWindowClass(\"{windowInfo.ClassName}\")).Select(e => e.GetWindowText()).ToArray();");
                    }
                    else
                    {
                        if (info.AttachMethod == AttachByClassName)
                        {
                            code.Add($"{Indent}{Indent}[UserControlDriverIdentify]");
                            code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app)");
                            code.Add($"{Indent}{Indent}{Indent}=> app.GetTopLevelWindows().SelectMany(e => e.GetFromWindowClass(\"{windowInfo.ClassName}\")).FirstOrDefault()?.Convert();");
                        }
                        else
                        {
                            code.Add($"{Indent}{Indent}[UserControlDriverIdentify]");
                            code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this WindowsAppFriend app)");
                            code.Add($"{Indent}{Indent}{Indent}=> app.GetTopLevelWindows().SelectMany(e => e.GetFromWindowText(\"{windowInfo.Text}\")).FirstOrDefault()?.Convert();");
                        }
                    }
                }
            }
            //ドライバへのアタッチ
            else
            {
                SeparateNameSpaceAndTypeName(info.AttachExtensionClass, out var ns, out var parentDriver);
                if (!string.IsNullOrEmpty(ns))
                {
                    usings.Add(ns);
                }

                if (info.AttachMethod == AttachCustom)
                {
                    code.Add($"{Indent}{Indent}[UserControlDriverIdentify(CustomMethod = \"TryGet\")]");
                    code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this {parentDriver} parent, T identifier)");
                    code.Add($"{Indent}{Indent}{{");
                    code.Add($"{Indent}{Indent}{Indent}//TODO");
                    code.Add($"{Indent}{Indent}}}");
                    code.Add(string.Empty);
                    code.Add($"{Indent}{Indent}public static T[] TryGet(this {parentDriver} parent)");
                    code.Add($"{Indent}{Indent}{{");
                    code.Add($"{Indent}{Indent}{Indent}//TODO");
                    code.Add($"{Indent}{Indent}}}");
                }
                else if (info.AttachMethod == AttachVariableWindowText)
                {
                    code.Add($"{Indent}{Indent}[UserControlDriverIdentify(CustomMethod = \"TryGet\")]");
                    code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this {parentDriver} parent, string text)");
                    code.Add($"{Indent}{Indent}{Indent}=> parent.Core.IdentifyFromWindowText(\"{windowInfo.Text}\").Convert();");
                    code.Add(string.Empty);
                    code.Add($"{Indent}{Indent}public static string[] TryGet(this {parentDriver} parent)");
                    code.Add($"{Indent}{Indent}{Indent}=> parent.Core.GetFromWindowClass(\"{windowInfo.ClassName}\").Select(e => e.GetWindowText()).ToArray();");
                }
                else
                {
                    if (info.AttachMethod == AttachByClassName)
                    {
                        code.Add($"{Indent}{Indent}[UserControlDriverIdentify]");
                        code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this {parentDriver} parent)");
                        code.Add($"{Indent}{Indent}{Indent}=> parent.Core.GetFromWindowClass(\"{windowInfo.ClassName}\").FirstOrDefault()?.Convert();");
                    }
                    else
                    {
                        code.Add($"{Indent}{Indent}[UserControlDriverIdentify]");
                        code.Add($"{Indent}{Indent}public static {info.ClassName} {funcName}(this {parentDriver} parent)");
                        code.Add($"{Indent}{Indent}{Indent}=> parent.Core.GetFromWindowText(\"{windowInfo.Text}\").FirstOrDefault()?.Convert();");
                    }
                }
            }
            code.Add($"{Indent}}}");

            return code;
        }

        static void SeparateNameSpaceAndTypeName(string attachExtensionClass, out string ns, out string parentDriver)
        {
            ns = string.Empty;
            parentDriver = attachExtensionClass;

            var sp = attachExtensionClass.Split('.');
            if (sp.Length < 2) return;

            parentDriver = sp[sp.Length - 1];
            var nsArray = new string[sp.Length - 1];
            Array.Copy(sp, nsArray, nsArray.Length);
            ns = string.Join(".", nsArray);
        }

        static string GetAttachFuncName(string driverClassName)
        {
            var index = driverClassName.IndexOf(DriverCreatorUtils.Suffix);
            if (0 < index && index == driverClassName.Length - DriverCreatorUtils.Suffix.Length)
            {
                return $"Attach{driverClassName.Substring(0, driverClassName.Length - DriverCreatorUtils.Suffix.Length)}";
            }
            return "Attach" + driverClassName;
        }

        static void DistinctAddRange(IEnumerable<string> src, List<string> dst)
        {
            foreach (var e in src)
            {
                if (!dst.Contains(e)) dst.Add(e);
            }
        }

        static void GetMembers(DriverDesignInfo info, out List<string> usings, out List<string> members)
        {
            usings = new List<string>();
            members = new List<string>();
            var fileName = $"{info.ClassName}.cs";
            foreach (var e in info.Properties)
            {
                var typeName = DriverCreatorUtils.GetTypeName(e.TypeFullName);
                var nameSpace = DriverCreatorUtils.GetTypeNamespace(e.TypeFullName);
                var todo = (e.IsPerfect.HasValue && !e.IsPerfect.Value) ? NativeDriverCreator.TODO_COMMENT : string.Empty;
                members.Add($"public {typeName} {e.Name} => {e.Identify}; {todo}");
                if (!usings.Contains(nameSpace)) usings.Add(nameSpace);
                foreach (var x in e.ExtensionUsingNamespaces)
                {
                    if (!usings.Contains(x)) usings.Add(x);
                }
            }
        }
    }
}
