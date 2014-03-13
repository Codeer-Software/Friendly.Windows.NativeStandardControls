using System;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.Friendly.Windows.NativeStandardControls.Generator.Inside;
using Codeer.Friendly.Windows.NativeStandardControls.Generator.Inside.Hook;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// Nativeコントロールのコード生成基本クラス。
    /// </summary>
    public abstract class NativeGeneratorBase : GeneratorBase
    {
        int _controlId;
        IntPtr _parentHandle;
        int _threadId;
        TimerInvoker _invoker = new TimerInvoker();

        /// <summary>
        /// コントロールID。
        /// </summary>
        internal int ControlId { get { return _controlId; } }

        /// <summary>
        /// アタッチ。
        /// </summary>
        protected override void Attach()
        {
            //親ハンドルを取得。
            _parentHandle = NativeMethods.GetParent(WindowHandle);

            //ダイアログID。
            _controlId = NativeMethods.GetDlgCtrlID(WindowHandle);

            //フック。
            int lpdwProcessId;
            _threadId = NativeMethods.GetWindowThreadProcessId(_parentHandle, out lpdwProcessId);
            ThreadMessageHookManager<MessageHookCallWndProc>.Entry(_threadId, MyAnalyzeMessage);
        }

        /// <summary>
        /// ディタッチ。
        /// </summary>
        protected override void Detach()
        {
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
            if (handle == _parentHandle)
            {
                try
                {
                    AnalyzeMessage(message, wparam, lparam);
                }
                catch (Exception e)
                {
                    AddSentence("/*" + e.Message + "*/");
                }
            }
        }

        /// <summary>
        /// フォーカスを持っているか。
        /// </summary>
        /// <returns>フォーカスを持っているか。</returns>
        internal bool HasFocus()
        {
            IntPtr focus = NativeMethods.GetFocus();
            while (focus != IntPtr.Zero)
            {
                if (focus == WindowHandle)
                {
                    return true;
                }
                focus = NativeMethods.GetParent(focus);
            }
            return false;
        }

        /// <summary>
        /// 解析。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
        internal abstract void AnalyzeMessage(int message, IntPtr wparam, IntPtr lparam);

        /// <summary>
        /// 重複した関数の削除。
        /// </summary>
        /// <param name="list">リスト。</param>
        /// <param name="function">関数。</param>
        internal void RemoveDuplicationFunction(List<Sentence> list, string function)
        {
            bool findChangeText = false;
            for (int i = list.Count - 1; 0 <= i; i--)
            {
                if (IsDuplicationFunction(list[i], function))
                {
                    if (findChangeText)
                    {
                        list.RemoveAt(i);
                    }
                    findChangeText = true;
                }
                else
                {
                    findChangeText = false;
                }
            }
        }

        /// <summary>
        /// 重複した関数であるか。
        /// </summary>
        /// <param name="sentence">センテンス。</param>
        /// <param name="function">関数。</param>
        /// <returns>重複した関数であるか。</returns>
        private bool IsDuplicationFunction(Sentence sentence, string function)
        {
            if (!ReferenceEquals(this, sentence.Owner))
            {
                return false;
            }
            if (sentence.Tokens.Length <= 2)
            {
                return false;
            }
            if (!(sentence.Tokens[0] is TokenName) ||
                (sentence.Tokens[1] == null))
            {
                return false;
            }
            return sentence.Tokens[1].ToString().IndexOf("." + function) == 0;
        }

        /// <summary>
        /// 最終行が重複していたら削除。
        /// </summary>
        /// <param name="function">関数名称。</param>
        internal void RemoveLastLineDuplicationFunction(string function)
        {
            if (0 < CurrentCode.Count && IsDuplicationFunction(CurrentCode[CurrentCode.Count - 1], function))
            {
                CurrentCode.RemoveAt(CurrentCode.Count - 1);
            }
        }

        /// <summary>
        /// 最終行が指定の行と一致するか。
        /// </summary>
        /// <param name="line">行情報。</param>
        /// <returns>最終行が指定の行と一致するか。</returns>
        internal bool LastLineIsSame(Sentence line)
        {
            return (0 < CurrentCode.Count) && (line.Equals(CurrentCode[CurrentCode.Count - 1]));
        }

        /// <summary>
        /// 遅延実行。
        /// </summary>
        /// <param name="invoke">実行オブジェクト。</param>
        internal void BeginInvoke(MethodInvoker invoke)
        {
            _invoker.BeginInvoke(invoke);
        }

        /// <summary>
        /// ウィンドウテキストの取得。
        /// </summary>
        /// <returns>ウィンドウテキスト。</returns>
        internal string GetWindowText()
        {
            int len = NativeMethods.GetWindowTextLength(WindowHandle);
            StringBuilder text = new StringBuilder((len + 1) * 8);
            NativeMethods.GetWindowText(WindowHandle, text, len * 8);
            return text.ToString();
        }
    }
}
