using System;
using System.Collections.Generic;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがComboBox、ComboBoxEx32の操作をトレースしてコード生成。
    /// </summary>
    [CaptureCodeGenerator("Codeer.Friendly.Windows.NativeStandardControls.NativeComboBox")]
    public class NativeComboBoxGenerator : NativeGeneratorBase
    {
        bool _isDropDownList;

        /// <summary>
        /// アタッチ
        /// </summary>
        protected override void Attach()
        {
            base.Attach();
            IntPtr dwStyle = NativeMethods.GetWindowLongPtr(WindowHandle, NativeCommonDefine.GWL_STYLE);
            _isDropDownList = ((dwStyle.ToInt32() & NativeComboBox.CBS_DROPDOWNLIST) == NativeComboBox.CBS_DROPDOWNLIST);
        }

        /// <summary>
        /// 解析。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
        internal override void AnalyzeMessage(int message, IntPtr wparam, IntPtr lparam)
        {
            if (!HasFocus() ||
                message != NativeCommonDefine.WM_COMMAND ||
                ControlId != (wparam.ToInt32() & 0xFFFF))
            {
                return;
            }
            switch ((int)(wparam.ToInt32() >> 16) & 0xFFFF)
            {
                case NativeComboBox.CBN_SELCHANGE:
                    AddSentence(new TokenName(), ".EmulateSelectItem(" + NativeComboBox.GetCurSelInTarget(WindowHandle), new TokenAsync(CommaType.Before), ");");
                    break;
                case NativeComboBox.CBN_EDITCHANGE:
                    if (!_isDropDownList)
                    {
                        string comboText = GetWindowText();
                        int curSel = NativeComboBox.GetCurSelInTarget(WindowHandle);
                        if (curSel != -1)
                        {
                            string itemText = NativeComboBox.GetLBTextInTarget(WindowHandle, curSel);
                            if (comboText == itemText)
                            {
                                return;
                            }
                        }
                        AddSentence(new TokenName(), ".EmulateChangeEditText(" + NativeEditGenerator.AdjustText(comboText), new TokenAsync(CommaType.Before), ");");
                    }
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
            RemoveDuplicationFunction(code, "EmulateChangeEditText");
        }
    }
}
