using System;
using System.Collections.Generic;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがScrollBarの操作をトレースしてコード生成。
    /// </summary>
    [CaptureCodeGenerator("Codeer.Friendly.Windows.NativeStandardControls.NativeScrollBar")]
    public class NativeScrollBarGenerator : NativeGeneratorBase
    {
        /// <summary>
        /// 解析。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
        protected override void AnalyzeMessage(int message, IntPtr wparam, IntPtr lparam)
        {
            if (lparam != WindowHandle)
            {
                return;
            }
            switch (message)
            {
                case NativeScrollBar.WM_HSCROLL:
                case NativeScrollBar.WM_VSCROLL:
                    RemoveLastLineDuplicationFunction("EmulateScroll");
                    AddSentence(new TokenName(), ".EmulateScroll(" + NativeMethods.GetScrollPos(WindowHandle, NativeScrollBar.SB_CTL), new TokenAsync(CommaType.Before), ");");
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 最適化。
        /// </summary>
        /// <param name="code">コード。</param>
        public override void Optimize(List<Sentence> code)
        {
            RemoveDuplicationFunction(code, "EmulateScroll");
        }
    }
}
