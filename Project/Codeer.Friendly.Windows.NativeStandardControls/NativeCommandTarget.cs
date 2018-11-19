using Codeer.TestAssistant.GeneratorToolKit;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using System.Collections.Generic;
using System.Reflection;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
    [ControlDriver]
    public abstract class NativeCommandTarget : WindowControl
    {
        [CaptureSetting]
        public Dictionary<int, string> MenuDefines { get; set; } = new Dictionary<int, string>();

        public NativeCommandTarget(WindowControl src) : base(src)
        {
            GetAllMenu(true, this, new string[0], MenuDefines);
        }

        static void GetAllMenu(bool isRoot, object obj, string[] names, Dictionary<int, string> dic)
        {
            if (!isRoot)
            {
                if (obj == null) return;
                if (obj.GetType().IsValueType) return;
                if (obj is WindowControl) return;
                if (obj is WindowsAppFriend) return;
            }

            foreach (var e in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (e.GetIndexParameters().Length != 0) continue;
                var prop = e.GetValue(obj, new object[0]);
                if (prop == null) continue;

                var nextNames = new List<string>(names);
                nextNames.Add(e.Name);

                var command = prop as NativeCommand;
                if (command != null) dic[command.Id] = string.Join(".", nextNames.ToArray());
                else GetAllMenu(false, prop, nextNames.ToArray(), dic);
            }
        }
    }
}
