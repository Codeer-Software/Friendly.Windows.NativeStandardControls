using Codeer.Friendly.Windows.Grasp.Inside;
using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    internal class NativeDriverCreator
    {
        private const string TODO_COMMENT = "// TODO It is not the best way to identify. Please change to a better method.";
        private const string INDENT = "    ";

        private readonly CodeDomProvider _dom;
        private readonly DriverElementNameGeneratorAdaptor _customNameGenerator;

        public NativeDriverCreator(CodeDomProvider dom)
        {
            _dom = dom;
            _customNameGenerator = new DriverElementNameGeneratorAdaptor(dom);
        }

        public void CreateDriver(WindowInfo windowInfoSrc)
        {
            CreateDriver(-1, string.Empty, windowInfoSrc, new List<string>());
        }

        public void CreateDriverFlat(WindowInfo windowInfoSrc)
        {
            var members = new List<string>();
            var usings = new List<string>();

            var driverName = MakeDriverName(windowInfoSrc.Text, new List<string>());
            CreateDriverFlat(-1, string.Empty, driverName, windowInfoSrc, members, usings, new List<string>(), false);

            var isTopLevel = NativeMethods.GetAncestor(windowInfoSrc.Handle, NativeMethods.Ancestor_Root) == windowInfoSrc.Handle;
            var code = GenerateCode(isTopLevel, driverName, DriverCreatorAdapter.SelectedNamespace, driverName, windowInfoSrc.Text, -1, usings, members);
            DriverCreatorAdapter.AddCode(driverName + ".cs", code, windowInfoSrc.Handle);
        }

        private void CreateDriver(int index, string rootDriver, WindowInfo windowInfo, List<string> driverNames)
        {
            //WindowInfoから情報を取り出す
            var windowText = windowInfo.Text;
            var windowHandle = windowInfo.Handle;

            var isTopLevel = NativeMethods.GetAncestor(windowHandle, NativeMethods.Ancestor_Root) == windowHandle;

            var driverName = MakeDriverName(windowText, driverNames);
            var dialogIds = new List<int>();
            foreach (var c in windowInfo.Children)
            {
                dialogIds.Add(c.DialogId);
            }
            var zIndexes = windowInfo.ZIndex;

            //子を調べる
            var fileName = $"{driverName}.cs";
            var members = new List<string>();
            var usings = new List<string>();
            var childrenZIndexTargetIndex = index + 1;
            var childNames = new List<string>();
            foreach (var childWindowInfo in OrderByTabOrder(windowInfo.Children))
            {
                //ドライバに合致するコントロールであるか
                string className = childWindowInfo.ClassName;
                var handle = childWindowInfo.Handle;
                var searchDescendantUserControls = true;
                if (DriverCreatorAdapter.WindowClassNameAndControlDriver.TryGetValue(className, out var ctrlDriver) && ctrlDriver.DriverMappingEnabled)
                {
                    searchDescendantUserControls = ctrlDriver.SearchDescendantUserControls;
                    var typeName = DriverCreatorUtils.GetTypeName(ctrlDriver.ControlDriverTypeFullName);
                    var ns = DriverCreatorUtils.GetTypeNamespace(ctrlDriver.ControlDriverTypeFullName);
                    if (!usings.Contains(ns)) usings.Add(ns);

                    var ctrlDriverPropName = _customNameGenerator.MakeDriverPropName(childWindowInfo.Handle, typeName, childNames);
                    var dialogId = childWindowInfo.DialogId;
                    var cZIndexes = childWindowInfo.ZIndex;
                    string key;
                    if (1 < CollectionUtility.Where(dialogIds, x => x == dialogId).Count)
                    {
                        key = $"Core.IdentifyFromZIndex({cZIndexes[childrenZIndexTargetIndex]})";
                        members.Add($"public {typeName} {ctrlDriverPropName} => new {typeName}({key}); {TODO_COMMENT}");
                    }
                    else
                    {
                        key = $"Core.IdentifyFromDialogId({dialogId})";
                        members.Add($"public {typeName} {ctrlDriverPropName} => new {typeName}({key});");
                    }
                    DriverCreatorAdapter.AddCodeLineSelectInfo(fileName, key, handle);
                }

                //さらに子を持っているならUserControlDriverを作成する
                if (searchDescendantUserControls && (0 < childWindowInfo.Children.Length))
                {
                    CreateDriver(childrenZIndexTargetIndex, driverName, childWindowInfo, driverNames);
                }
            }

            //コード生成
            var zIndex = index == -1 ? -1 : zIndexes[index];
            DriverCreatorAdapter.AddCode(fileName, GenerateCode(isTopLevel, rootDriver, DriverCreatorAdapter.SelectedNamespace, driverName, windowText, zIndex, usings, members), windowHandle);
        }

        private void CreateDriverFlat(int index, string parentKey, string driverName, WindowInfo windowInfo, List<string> members, List<string> usings, List<string> childNames, bool isUseZIndex)
        {
            //WindowInfoから情報を取り出す
            var windowText = windowInfo.Text;
            var windowHandle = windowInfo.Handle;

            var dialogIds = new List<int>();
            foreach (var c in windowInfo.Children)
            {
                dialogIds.Add(c.DialogId);
            }
            var zIndexes = windowInfo.ZIndex;

            //子を調べる
            var fileName = $"{driverName}.cs";
            var childrenZIndexTargetIndex = index + 1;
            foreach (var childWindowInfo in OrderByTabOrder(windowInfo.Children))
            {
                //ドライバに合致するコントロールであるか
                string className = childWindowInfo.ClassName;
                var handle = childWindowInfo.Handle;
                var searchDescendantUserControls = true;
                var dialogId = childWindowInfo.DialogId;
                var cZIndexes = childWindowInfo.ZIndex;
                if (DriverCreatorAdapter.WindowClassNameAndControlDriver.TryGetValue(className, out var ctrlDriver) && ctrlDriver.DriverMappingEnabled)
                {
                    searchDescendantUserControls = ctrlDriver.SearchDescendantUserControls;
                    var typeName = DriverCreatorUtils.GetTypeName(ctrlDriver.ControlDriverTypeFullName);
                    var ns = DriverCreatorUtils.GetTypeNamespace(ctrlDriver.ControlDriverTypeFullName);
                    if (!usings.Contains(ns)) usings.Add(ns);

                    var ctrlDriverPropName = _customNameGenerator.MakeDriverPropName(childWindowInfo.Handle, typeName, childNames);

                    string key;
                    string todo = isUseZIndex ? TODO_COMMENT : string.Empty;
                    if (1 < CollectionUtility.Where(dialogIds, x => x == dialogId).Count)
                    {
                        todo = TODO_COMMENT;
                        key = $"Core{parentKey}.IdentifyFromZIndex({cZIndexes[childrenZIndexTargetIndex]})";
                        members.Add($"public {typeName} {ctrlDriverPropName} => new {typeName}({key}); {todo}");
                    }
                    else
                    {
                        key = $"Core{parentKey}.IdentifyFromDialogId({dialogId})";
                        members.Add($"public {typeName} {ctrlDriverPropName} => new {typeName}({key}); {todo}");
                    }
                    DriverCreatorAdapter.AddCodeLineSelectInfo(fileName, key, handle);
                }

                //さらに子を持っているならUserControlDriverを作成する
                if (searchDescendantUserControls && (0 < childWindowInfo.Children.Length))
                {
                    string key;
                    var isZIndex = false;
                    if (1 < CollectionUtility.Where(dialogIds, x => x == dialogId).Count)
                    {
                        isZIndex = true;
                        key = $"{parentKey}.IdentifyFromZIndex({cZIndexes[childrenZIndexTargetIndex]})";
                    }
                    else
                    {
                        key = $"{parentKey}.IdentifyFromDialogId({dialogId})";
                    }
                    CreateDriverFlat(childrenZIndexTargetIndex, key, driverName, childWindowInfo, members, usings, childNames, isUseZIndex || isZIndex);
                }
            }
        }

        private List<WindowInfo> OrderByTabOrder(WindowInfo[] enumerable)
        {
            var list = new List<WindowInfo>(enumerable);
            list.Sort((l, r) =>
            {
                var la = l.ZIndex;
                var ra = r.ZIndex;
                var lIndex = la.Length == 0 ? -1 : la[la.Length - 1];
                var rIndex = ra.Length == 0 ? -1 : ra[ra.Length - 1];
                return rIndex - lIndex;
            });
            return list;
        }

        private string MakeDriverName(string text, List<string> names)
        {
            text = text.Replace(" ", "");
            foreach (var err in "(){}<>+-=*/%!\"#$&'^~\\|@;:,.?")
            {
                text = text.Replace(err, '_');
            }

            var nameSrc = _dom.IsValidIdentifier(text) ? text : "Window";
            var name = nameSrc;
            for (int i = 0;; i++)
            {
                if (!names.Contains(name))
                {
                    names.Add(name);
                    return name + DriverCreatorUtils.Suffix;
                }
                name = nameSrc + i;
            }
        }

        private string GenerateCode(bool isTopLevel, string rootDriver, string nameSpace, string driverClassName, string windowText, int zIndex, List<string> usings, List<string> members)
        {
            var code = new List<string>
            {
                "using Codeer.Friendly.Dynamic;",
                "using Codeer.Friendly.Windows;",
                "using Codeer.Friendly.Windows.Grasp;",
                "using Codeer.TestAssistant.GeneratorToolKit;"
            };
            foreach (var e in usings)
            {
                code.Add($"using {e};");
            }

            var attr = string.IsNullOrEmpty(rootDriver) ? "WindowDriver" : "UserControlDriver";

            code.Add(string.Empty);
            code.Add($"namespace {nameSpace}");
            code.Add("{");
            code.Add($"{INDENT}[{attr}]");
            code.Add($"{INDENT}public class {driverClassName}");
            code.Add($"{INDENT}{{");
            code.Add($"{INDENT}{INDENT}public WindowControl Core {{ get; }}");
            foreach (var e in members)
            {
                code.Add($"{INDENT}{INDENT}{e}");
            }
            code.Add(string.Empty);
            code.Add($"{INDENT}{INDENT}public {driverClassName}(WindowControl core)");
            code.Add($"{INDENT}{INDENT}{{");
            code.Add($"{INDENT}{INDENT}{INDENT}Core = core;");
            code.Add($"{INDENT}{INDENT}}}");
            code.Add($"{INDENT}}}");

            if (isTopLevel)
            {
                code.Add(string.Empty);
                code.Add($"{INDENT}public static class {driverClassName}_Extensions");
                code.Add($"{INDENT}{{");
                code.Add($"{INDENT}{INDENT}[WindowDriverIdentify(WindowText = \"{windowText}\")]");
                code.Add($"{INDENT}{INDENT}public static {driverClassName} {GetFuncName(driverClassName)}(this WindowsAppFriend app)");
                code.Add($"{INDENT}{INDENT}{INDENT}=> new {driverClassName}(app.WaitForIdentifyFromWindowText(\"{windowText}\"));");
                code.Add($"{INDENT}}}");
            }
            else if (!string.IsNullOrEmpty(rootDriver))
            {
                code.Add(string.Empty);
                code.Add($"{INDENT}public static class {driverClassName}_Extensions");
                code.Add($"{INDENT}{{");
                code.Add($"{INDENT}{INDENT}{TODO_COMMENT}");
                code.Add($"{INDENT}{INDENT}[UserControlDriverIdentify]");
                code.Add($"{INDENT}{INDENT}public static {driverClassName} {GetFuncName(driverClassName)}(this {rootDriver} window)");
                code.Add($"{INDENT}{INDENT}{INDENT}=> new {driverClassName}(window.Core.IdentifyFromZIndex({zIndex}));");
                code.Add($"{INDENT}}}");
            }
            code.Add("}");
            return string.Join(Environment.NewLine, code.ToArray());
        }

        private string GetFuncName(string driverClassName)
        {
            return $"Attach_{driverClassName.Substring(0, driverClassName.Length - DriverCreatorUtils.Suffix.Length)}";
        }
    }
}
