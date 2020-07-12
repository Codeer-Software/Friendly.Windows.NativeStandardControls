using Codeer.Friendly.Windows.Grasp;
using System;
using System.Dynamic;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// WindowControl extension methods.
    /// </summary>
#else
    /// <summary>
    /// WindowControlの拡張メソッド
    /// </summary>
#endif
    public static class WindowControlExtensions
    {
        class DynamicConverter : DynamicObject
        {
            WindowControl _src;
            internal DynamicConverter(WindowControl src) => _src = src;

            public override bool TryConvert(ConvertBinder binder, out object result)
            {
                result = null;
                if (binder.Type.GetConstructor(new[] { typeof(WindowControl) }) != null)
                {
                    result = Activator.CreateInstance(binder.Type, new object[] { _src });
                    return true;
                }
                return false;
            }
        }

#if ENG
        /// <summary>
        /// Can be converted to a class that has a constructor that takes only WindowControl as an argument.
        /// </summary>
        /// <param name="window">The underlying WindowControl.</param>
        /// <returns>Converted result.</returns>
#else
        /// <summary>
        /// WindowControlを一つだけ引数に取るクラスに変換するオブジェクトを返します。
        /// </summary>
        /// <param name="window">元となるWindowControl</param>
        /// <returns>変換オブジェクト</returns>
#endif
        public static dynamic Convert(this WindowControl window)
            => window == null ? null : new DynamicConverter(window);
    }
}
