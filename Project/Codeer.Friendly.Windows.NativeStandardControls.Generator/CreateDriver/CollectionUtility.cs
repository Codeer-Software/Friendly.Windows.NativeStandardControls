using System;
using System.Collections.Generic;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.CreateDriver
{
    internal static class CollectionUtility
    {
        public static List<T> Where<T>(IEnumerable<T> src, Predicate<T> isMatch)
        {
            var list = new List<T>();
            foreach (var e in src)
            {
                if (isMatch(e)) list.Add(e);
            }
            return list;
        }
    }
}
