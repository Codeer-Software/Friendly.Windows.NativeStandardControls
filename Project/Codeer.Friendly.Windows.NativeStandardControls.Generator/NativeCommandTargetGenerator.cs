using System;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.Friendly.Windows.NativeStandardControls.Generator.Hook;
using Codeer.TestAssistant.GeneratorToolKit;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// ネイティブのWM_COMMANDに対応する処理を生成するクラス。
    /// </summary>
    [CaptureCodeGenerator("Codeer.Friendly.Windows.NativeStandardControls.NativeCommandTarget")]
    public class NativeCommandTargetGenerator : CaptureCodeGeneratorBase
    {
        const int WM_COMMAND = 0x0111;

        int _threadId;
        Dictionary<int, string> _commandMap;

        /// <summary>
        /// アタッチ
        /// </summary>
        protected override void Attach()
        {
            _commandMap = CaptureSetting as Dictionary<int, string>;
            if (_commandMap == null) _commandMap = new Dictionary<int, string>();
            
            int lpdwProcessId;
            _threadId = NativeMethods.GetWindowThreadProcessId(WindowHandle, out lpdwProcessId);
            ThreadMessageHookManager<MessageHookCallWndProc>.Entry(_threadId, MyAnalyzeMessage);
            ThreadMessageHookManager<MessageHookGetMessage>.Entry(_threadId, MyAnalyzeMessage);
        }

        /// <summary>
        /// ディタッチ。
        /// </summary>
        protected override void Detach()
        {
            ThreadMessageHookManager<MessageHookCallWndProc>.Remove(_threadId, MyAnalyzeMessage);
            ThreadMessageHookManager<MessageHookGetMessage>.Remove(_threadId, MyAnalyzeMessage);
        }

        void MyAnalyzeMessage(IntPtr handle, int message, IntPtr wparam, IntPtr lparam)
        {
            if (_commandMap == null) return;
            if (message != WM_COMMAND) return;

            if (!_commandMap.TryGetValue(wparam.ToInt32(), out var path))
            {
                if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                {
                    AddSentence("//" + wparam.ToInt32());
                }
                return;
            }
            AddSentence(new TokenName(), "." + path + ".EmulateClick(", new TokenAsync(CommaType.Non), ");");
        }
    }
}
