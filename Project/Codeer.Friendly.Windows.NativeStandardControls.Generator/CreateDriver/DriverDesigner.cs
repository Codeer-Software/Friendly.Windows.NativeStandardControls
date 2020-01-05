using Codeer.Friendly.Windows.Grasp.Inside;
using Codeer.Friendly.Windows.Grasp.Inside.InApp;
using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    public class DriverDesigner : IDriverDesigner
    {
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

            var windowInfo = WindowAnalyzer.Analyze(rootHandle, new IOtherSystemWindowAnalyzer[0]);
            var target = elementHandle;
            var parentInfo = windowInfo;
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

                if (1 < CollectionUtility.Where(dialogIds, x => x == childInfo.DialogId).Count)
                {
                    isPerfect = false;
                    if (childInfo.ZIndex.Length <= i) return new DriverIdentifyInfo[0];
                    accessPaths.Add($"IdentifyFromZIndex({ childInfo.ZIndex[i]})");
                }
                else
                {
                    accessPaths.Add($"IdentifyFromDialogId({ childInfo.DialogId})");
                }
                parentInfo = childInfo;
            }

            string name = "window";
            if (DriverCreatorAdapter.WindowClassNameAndControlDriver.TryGetValue(parentInfo.ClassName, out var ctrlDriver))
            {
                var typeName = DriverCreatorUtils.GetTypeName(ctrlDriver.ControlDriverTypeFullName);
                name = customNameGenerator.MakeDriverPropName(elementHandle, typeName, new List<string>());
            }
            var accessPath = string.Join(".", accessPaths.ToArray());
            return new[]
            {
                new DriverIdentifyInfo
                {
                    IsPerfect = isPerfect,
                    Identify = "Core." + accessPath,
                    DefaultName = name,
                    ExtensionUsingNamespaces = new string[0]
                }
            };
        }

        public void GenerateCode(object root, DriverDesignInfo info)
        {
            GetMembers(info, out var usings, out var members);

            var fileName = $"{info.ClassName}.cs";
            var windowInfo = WindowAnalyzer.Analyze((IntPtr)root, new IOtherSystemWindowAnalyzer[0]);

            var topWindowHandle = NativeMethods.GetAncestor(windowInfo.Handle, NativeMethods.Ancestor_Root);
            var isTopLevel = topWindowHandle == windowInfo.Handle;
            var rootDriver = string.Empty;
            int zIndex = 0;
            if (!isTopLevel && info.CreateAttachCode)
            {
                rootDriver = "???";
                zIndex = -1;
            }

            using (var dom = CodeDomProvider.CreateProvider("CSharp"))
            {
                var code = new NativeDriverCreator(dom).GenerateCode(isTopLevel, rootDriver, DriverCreatorAdapter.SelectedNamespace, info.ClassName, windowInfo.Text, zIndex, usings, members);
                DriverCreatorAdapter.AddCode(fileName, code, root);
            }

            //選択のための情報を設定
            foreach (var e in info.Properties)
            {
                DriverCreatorAdapter.AddCodeLineSelectInfo(fileName, e.Identify, e.Element);
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
                members.Add($"public {typeName} {e.Name} => new {typeName}({e.Identify}); {todo}");

                if (!usings.Contains(nameSpace)) usings.Add(nameSpace);
                foreach (var x in e.ExtensionUsingNamespaces)
                {
                    if (!usings.Contains(x)) usings.Add(x);
                }
            }
        }
    }
}
