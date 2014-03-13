using System;
using System.Collections.Generic;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがmsctls_trackbar32の操作をトレースしてコード生成。
    /// </summary>
    public class NativeSliderGenerator : NativeGeneratorBase
    {
        /// <summary>
        /// 解析。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
        internal override void AnalyzeMessage(int message, IntPtr wparam, IntPtr lparam)
        {
            if (lparam != WindowHandle)
            {
                return;
            }

            if (!HasFocus())
            {
                return;
            }

            switch (message)
            {
                case NativeScrollBar.WM_HSCROLL:
                case NativeScrollBar.WM_VSCROLL:
                    RemoveLastLineDuplicationFunction("EmulateChangePos");
                    AddSentence(new TokenName(), ".EmulateChangePos(" + NativeSlider.GetPosInTarget(WindowHandle), new TokenAsync(CommaType.Before), ");");
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
            RemoveDuplicationFunction(code, "EmulateChangePos");
        }
    }
}
