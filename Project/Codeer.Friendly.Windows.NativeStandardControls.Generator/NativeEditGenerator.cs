using System;
using System.Text;
using System.Collections.Generic;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがEdit、RichEdit20A、RichEdit20Wの操作をトレースしてコード生成。
    /// </summary>
    [Generator("Codeer.Friendly.Windows.NativeStandardControls.NativeEdit")]
    public class NativeEditGenerator : NativeGeneratorBase
    {
        /// <summary>
        /// 解析。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
        internal override void AnalyzeMessage(int message, IntPtr wparam, IntPtr lparam)
        {
            if (HasFocus() &&
                message == NativeCommonDefine.WM_COMMAND &&
                ControlId == (wparam.ToInt32() & 0xFFFF) &&
                ((int)(wparam.ToInt32() >> 16) & 0xFFFF) == NativeEdit.EN_UPDATE)
            {
                AddSentence(new TokenName(), ".EmulateChangeText(" + AdjustText(GetWindowText()), new TokenAsync(CommaType.Before), ");");
            }
        }

        /// <summary>
        /// テキストを調整する
        /// </summary>
        /// <param name="text">テキスト。</param>
        /// <returns>調整済み行。</returns>
        static internal string AdjustText(string text)
        {
            text = text.Replace("\"", "\"\"");
            string[] lines = text.Replace("\r\n", "\n").Replace("\r", "\n").Split(new char[] { '\n' });
            StringBuilder builder = new StringBuilder();
            foreach (string line in lines)
            {
                if (0 < builder.Length)
                {
                    builder.Append(" + Environment.NewLine + ");
                }
                builder.Append("@\"" + line + "\"");
            }
            return builder.ToString();
        }

        /// <summary>
        /// 最適化。
        /// </summary>
        /// <param name="code">コード。</param>
        public override void Optimize(List<Sentence> code)
        {
            RemoveDuplicationFunction(code, "EmulateChangeText");
        }
    }
}
