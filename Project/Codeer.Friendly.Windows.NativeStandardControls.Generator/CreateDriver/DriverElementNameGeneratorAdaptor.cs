using Codeer.TestAssistant.GeneratorToolKit;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    internal class DriverElementNameGeneratorAdaptor
    {
        private readonly List<IDriverElementNameGenerator> _nameGenerators = new List<IDriverElementNameGenerator>();
        private readonly CodeDomProvider _dom;

        public DriverElementNameGeneratorAdaptor(CodeDomProvider dom)
        {
            _dom = dom;

            Initialize();
        }

        private void Initialize()
        {
            //名前カスタムクラスを集める
            var nameGeneratorType = typeof(IDriverElementNameGenerator);

            foreach (var type in EnumAllTypes())
            {
                try
                {
                    if (nameGeneratorType.IsAssignableFrom(type) && type.IsClass)
                    {
                        var generator = (IDriverElementNameGenerator)Activator.CreateInstance(type);
                        _nameGenerators.Add(generator);
                    }
                }
                catch { }
            }

            //プライオリティが高い順にソート
            _nameGenerators.Sort((l, r) => r.Priority - l.Priority);
        }

        private IEnumerable<Type> EnumAllTypes()
        {
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in GetTypesFromAssembly(asm))
                {
                    yield return type;
                }
            }
        }

        private IEnumerable<Type> GetTypesFromAssembly(Assembly asm)
        {
            try
            {
                return asm.GetTypes();
            }
            catch
            {
                return new Type[0];
            }
        }

        public string MakeDriverPropName(object control, string secondCandidate, List<string> names)
        {
            var customName = MakeName(control);
            if (!string.IsNullOrEmpty(customName)) return ToUniqueName(customName, names);
            var name = (!string.IsNullOrEmpty(secondCandidate) && _dom.IsValidIdentifier(secondCandidate)) ?
                        secondCandidate :
                        control.GetType().Name;
            return ToUniqueName(name, names);
        }

        private static string ToUniqueName(string nameSrc, List<string> names)
        {
            var name = nameSrc;
            for (int i = 0;; i++)
            {
                if (!names.Contains(name))
                {
                    names.Add(name);
                    return name;
                }
                name = nameSrc + i;
            }
        }

        private string MakeName(object obj)
        {
            foreach (var e in _nameGenerators)
            {
                var name = e.GenerateName(obj);
                if (!string.IsNullOrEmpty(name)) return name;
            }
            return string.Empty;
        }
    }
}
