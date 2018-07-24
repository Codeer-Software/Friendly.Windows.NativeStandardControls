using System;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.Friendly.Windows.NativeStandardControls.Generator.Inside.Hook;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    //TODO
    /// <summary>
    /// ネイティブのWM_COMMANDに対応する処理を生成するクラス。
    /// </summary>
    public class NativeCommandGenerator : CaptureCodeGeneratorBase
    {
        int _threadId;

        /// <summary>
        /// アタッチ
        /// </summary>
        protected override void Attach()
        {
            //フック。
            int lpdwProcessId;
            _threadId = NativeMethods.GetWindowThreadProcessId(WindowHandle, out lpdwProcessId);
            ThreadMessageHookManager<MessageHookGetMessage>.Entry(_threadId, MyAnalyzeMessage);
            ThreadMessageHookManager<MessageHookCallWndProc>.Entry(_threadId, MyAnalyzeMessage);
        }
        /// <summary>
        /// ディタッチ。
        /// </summary>
        protected override void Detach()
        {
            ThreadMessageHookManager<MessageHookGetMessage>.Remove(_threadId, MyAnalyzeMessage);
            ThreadMessageHookManager<MessageHookCallWndProc>.Remove(_threadId, MyAnalyzeMessage);
        }

        /// <summary>
        /// メッセージ解析。
        /// </summary>
        /// <param name="handle">ハンドル。</param>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
        void MyAnalyzeMessage(IntPtr handle, int message, IntPtr wparam, IntPtr lparam)
        {
            if (handle == WindowHandle && message == NativeCommonDefine.WM_COMMAND && lparam == IntPtr.Zero)
            {
                AddSentence(new TokenName(), ".SendMessage(0x111, new IntPtr(" + wparam.ToInt32() + "), IntPtr.Zero", new TokenAsync(CommaType.Before), "); //WM_COMMAND");
            }
        }
    }
}
