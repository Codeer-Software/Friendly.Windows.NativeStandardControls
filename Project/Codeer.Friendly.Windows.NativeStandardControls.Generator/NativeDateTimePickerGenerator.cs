using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがSysDateTimePick32の操作をトレースしてコード生成。
    /// </summary>
    public class NativeDateTimePickerGenerator : NativeGeneratorBase
    {
        /// <summary>
        /// 解析。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
        internal override void AnalyzeMessage(int message, IntPtr wparam, IntPtr lparam)
        {
            if (message != NativeCommonDefine.WM_NOTIFY)
            {
                return;
            }
            NMDATETIMECHANGE data = (NMDATETIMECHANGE)Marshal.PtrToStructure
                    (lparam, typeof(NMDATETIMECHANGE));
            if (data.nmhdr.idFrom.ToInt32() != ControlId ||
                data.nmhdr.code != NativeDateTimePicker.DTN_DATETIMECHANGE ||
                data.dwFlags != NativeDateTimePicker.GDT_VALID)
            {
                return;
            }

            DateTime time = NativeDataUtility.ToDateTime(data.st);
            Sentence line = new Sentence(this, new TokenName(), ".EmulateSelectDay(new DateTime(" + time.Year + ", " + time.Month + ", " + time.Day + ")", new TokenAsync(CommaType.Before), ");");
            if (!LastLineIsSame(line))
            {
                AddSentence(line);
            }
        }
    }
}
