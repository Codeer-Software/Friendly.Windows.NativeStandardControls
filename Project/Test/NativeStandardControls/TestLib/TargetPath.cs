using System.IO;
using System;

namespace NativeStandardControls.TestLib
{
    /// <summary>
    /// テスト対象のパス管理クラス。
    /// </summary>
    static class TargetPath
    {
        /// <summary>
        /// NativeControls.exeへのパス。
        /// 以下の4タイプのアプリケーションを切り分ける。
        /// ・32bitマルチバイト
        /// ・32bitユニコード
        /// ・64bitマルチバイト
        /// ・64bitユニコード
        /// </summary>
        internal static string NativeControls
        {
            get
            {
                string name = string.Empty;
                if (IntPtr.Size == 8)
                {
                    name += "64";
                }
#if UNI
                name += "-Uni";
#endif
                return Path.GetFullPath("..\\..\\debug" + name + "\\NativeControls.exe"); 
            }
        }
    }
}
