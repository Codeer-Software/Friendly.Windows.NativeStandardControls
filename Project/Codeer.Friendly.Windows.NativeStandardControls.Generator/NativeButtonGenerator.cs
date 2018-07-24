using System;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがButtonの操作をトレースしてコード生成。
    /// </summary>
    [CaptureCodeGenerator("Codeer.Friendly.Windows.NativeStandardControls.NativeButton")]
    public class NativeButtonGenerator : NativeGeneratorBase
    {
        bool _hasCheckState;

        /// <summary>
        /// アタッチ
        /// </summary>
        protected override void Attach()
        {
            base.Attach();
            IntPtr style = NativeMethods.GetWindowLongPtr(WindowHandle, NativeCommonDefine.GWL_STYLE);

            //チェックボタン、またはラジオボタンであるか
            foreach (int checkStateStyle in new int[] { 
                NativeButton.BS_CHECKBOX, NativeButton.BS_AUTOCHECKBOX, NativeButton.BS_RADIOBUTTON, 
                NativeButton.BS_3STATE, NativeButton.BS_AUTO3STATE, NativeButton.BS_AUTORADIOBUTTON
            })
            {
                _hasCheckState = ((style.ToInt32() & checkStateStyle) == checkStateStyle);
                if (_hasCheckState)
                {
                    break;
                }
            }
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
                ControlId != (wparam.ToInt32() & 0xFFFF) ||
                NativeButton.BN_CLICKED != ((wparam.ToInt32() >> 16) & 0xFFFF))
            {
                return;
            }

            if (_hasCheckState)
            {
                BeginInvoke(delegate
                {
                    AddSentence(new TokenName(), ".EmulateCheck(CheckState." + NativeButton.GetCheckStateInTarget(WindowHandle), new TokenAsync(CommaType.Before), ");");
                });
            }
            else
            {
                AddSentence(new TokenName(), ".EmulateClick(", new TokenAsync(CommaType.Non), ");");
            }
        }
    }
}
