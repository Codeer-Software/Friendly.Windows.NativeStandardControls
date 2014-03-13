using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがSysTabControl32の操作をトレースしてコード生成。
    /// </summary>
    public class NativeTabGenerator : NativeGeneratorBase
    {
        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="wparam">wparam</param>
        /// <param name="lparam">lparam</param>
        internal override void AnalyzeMessage(int message, IntPtr wparam, IntPtr lparam)
        {
            if (message == NativeCommonDefine.WM_NOTIFY)
            {
                NMHDR data = (NMHDR)Marshal.PtrToStructure
                        (lparam, typeof(NMHDR));

                if (data.idFrom.ToInt32() == ControlId && data.code == NativeTab.TCN_SELCHANGE)
                {
                    AddSentence(new TokenName(), ".EmulateSelectItem(" + NativeTab.GetSelectedItemIndexInTarget(WindowHandle), new TokenAsync(CommaType.Before), ");");
                }
            }
        }
    }
}
