using System;
using System.Runtime.InteropServices;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがSysListView32の操作をトレースしてコード生成。
    /// </summary>
    [CaptureCodeGenerator("Codeer.Friendly.Windows.NativeStandardControls.NativeListControl")]
    public class NativeListControlGenerator : NativeGeneratorBase
    {
        /// <summary>
        /// 解析。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
        protected override void AnalyzeMessage(int message, IntPtr wparam, IntPtr lparam)
        {
            if (message != NativeCommonDefine.WM_NOTIFY)
            {
                return;
            }
            NMHDR hdr = (NMHDR)Marshal.PtrToStructure
                    (lparam, typeof(NMHDR));
            
            if (hdr.idFrom.ToInt32() != ControlId)
            {
                return;
            }
            switch (hdr.code)
            {
                case NativeListControl.LVN_ITEMCHANGED:
                    {
                        NMLISTVIEW data = (NMLISTVIEW)Marshal.PtrToStructure
                                                (lparam, typeof(NMLISTVIEW));
                        bool isSelected = (NativeListControl.GetItemStateInTarget(WindowHandle, data.iItem, LVIS.SELECTED) == LVIS.SELECTED);
                        Sentence line = new Sentence(this, new TokenName(), ".EmulateSelect(" + data.iItem + ", " + isSelected.ToString().ToLower(), new TokenAsync(CommaType.Before), ");");
                        if (!LastLineIsSame(line))
                        {
                            AddSentence(line);
                        }
                    }
                    break;
                case NativeListControl.LVN_ENDLABELEDITA:
                case NativeListControl.LVN_ENDLABELEDITW:
                    {
                        LVDISPINFO data = (LVDISPINFO)Marshal.PtrToStructure
                                (lparam, typeof(LVDISPINFO));
                        string text = NativeMethods.IsWindowUnicode(WindowHandle) ? 
                            Marshal.PtrToStringUni(data.item.pszText) :
                                Marshal.PtrToStringAnsi(data.item.pszText);
                        text = NativeEditGenerator.AdjustText(text);
                        AddSentence(new TokenName(), ".EmulateEdit(" + data.item.iItem + ", " + text, new TokenAsync(CommaType.Before), ");");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
