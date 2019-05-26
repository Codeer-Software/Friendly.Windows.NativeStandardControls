using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがSysIPAddress32のウィンドウに対応した操作を提供します。
    /// </summary>
    [CaptureCodeGenerator("Codeer.Friendly.Windows.NativeStandardControls.NativeIPAddress")]
    public class NativeIPAddressGenerator : NativeGeneratorBase
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
            NMIPADDRESS data = (NMIPADDRESS)Marshal.PtrToStructure
                    (lparam, typeof(NMIPADDRESS));
            if (data.hdr.idFrom.ToInt32() != ControlId || data.hdr.code != NativeIPAddress.IPN_FIELDCHANGED)
            {
                return;
            }
            byte field0 = 0, field1 = 0, field2 = 0, field3 = 0;
            NativeIPAddress.GetAddressInTarget(WindowHandle, ref field0, ref field1, ref field2, ref field3);
            AddSentence(new TokenName(), ".EmulateChangeAddress(" + field0 + ", " + field1 + ", " + field2 + ", " + field3, new TokenAsync(CommaType.Before), ");");
        }

        /// <summary>
        /// 最適化。
        /// </summary>
        /// <param name="code">コード。</param>
        public override void Optimize(List<Sentence> code)
        {
            RemoveDuplicationFunction(code, "EmulateChangeAddress");
        }
    }
}
