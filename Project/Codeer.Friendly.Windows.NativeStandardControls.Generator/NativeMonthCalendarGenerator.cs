using System;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using System.Runtime.InteropServices;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがSysMonthCal32の操作をトレースしてコード生成。
    /// </summary>
    [Generator("Codeer.Friendly.Windows.NativeStandardControls.NativeMonthCalendar")]
    public class NativeMonthCalendarGenerator : NativeGeneratorBase
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
            NMSELCHANGE data = (NMSELCHANGE)Marshal.PtrToStructure
                    (lparam, typeof(NMSELCHANGE));

            if (data.nmhdr.idFrom.ToInt32() != ControlId ||
                data.nmhdr.code != NativeMonthCalendar.MCN_SELCHANGE)
            {
                return;
            }

            //選択日時取得。
            DateTime start = NativeDataUtility.ToDateTime(data.stSelStart);
            DateTime end = start;
            try
            {
                end = NativeDataUtility.ToDateTime(data.stSelEnd);
            }
            catch { }

            //コード生成。
            Sentence line;
            if (start == end)
            {
                line = new Sentence(this, new TokenName(), ".EmulateSelectDay(new DateTime(" + start.Year + ", " + start.Month + ", " + start.Day + ")", new TokenAsync(CommaType.Before), ");");
            }
            else
            {
                line = new Sentence(this, new TokenName(), ".EmulateSelectDay(" +
                    "new DateTime(" + start.Year + ", " + start.Month + ", " + start.Day + "), " +
                    "new DateTime(" + end.Year + ", " + end.Month + ", " + end.Day + ")", new TokenAsync(CommaType.Before), ");");
            }
            AddSentence(line);
        }
    }
}
