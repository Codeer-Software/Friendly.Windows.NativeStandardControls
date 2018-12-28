using System;
using System.Collections.Generic;
using System.Reflection;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    internal class ReflectionAccessor
    {
        private static readonly Dictionary<string, Type> FullNameAndType = new Dictionary<string, Type>();

        public object Object { get; }

        public ReflectionAccessor(object obj)
        {
            Object = obj;
        }

        public T GetProperty<T>(string name)
        {
            return (T)Object.GetType().GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetValue(Object, new object[0]);
        }

        public static Type GetType(string typeFullName)
        {
            lock (FullNameAndType)
            {
                //キャッシュを見る
                if (FullNameAndType.TryGetValue(typeFullName, out Type type))
                {
                    return type;
                }

                //各アセンブリに問い合わせる			
                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                foreach (var assembly in assemblies)
                {
                    type = assembly.GetType(typeFullName);
                    if (type != null)
                    {
                        FullNameAndType.Add(typeFullName, type);
                        break;
                    }
                }
                return type;
            }
        }

        public static object InvokeStaticMethod(string typeFullName, string method, params object[] args)
        {
            return GetType(typeFullName).GetMethod(method, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Invoke(null, args);
        }
    }
}
