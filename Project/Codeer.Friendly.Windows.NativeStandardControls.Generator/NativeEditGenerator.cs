using System;
using System.Text;
using System.Collections.Generic;
using Codeer.TestAssistant.GeneratorToolKit;
using System.CodeDom.Compiler;
using System.IO;
using System.CodeDom;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがEdit、RichEdit20A、RichEdit20Wの操作をトレースしてコード生成。
    /// </summary>
    [CaptureCodeGenerator("Codeer.Friendly.Windows.NativeStandardControls.NativeEdit")]
    public class NativeEditGenerator : NativeGeneratorBase
    {
        /// <summary>
        /// 解析。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
        protected override void AnalyzeMessage(int message, IntPtr wparam, IntPtr lparam)
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
            using (var writer = new StringWriter())
            using (var provider = CodeDomProvider.CreateProvider("CSharp"))
            {
                var expression = new CodePrimitiveExpression(text);
                provider.GenerateCodeFromExpression(expression, writer, options: null);
                return writer.ToString();
            }
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
