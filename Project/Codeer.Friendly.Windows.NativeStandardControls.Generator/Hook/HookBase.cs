using System;
using System.Collections.Generic;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.Hook
{
    /// <summary>
    /// フックオブジェクト。
    /// 一つのスレッドにあまりに多くのフックを実行すると失敗することがあるのでフック自体は一回にまとめる。
    /// </summary>
    public abstract class HookBase
    {
        NativeMethods.HookProc _traceProc;
        IntPtr _idHook;
        Dictionary<AnalyzeMessage, bool> _proc = new Dictionary<AnalyzeMessage, bool>();

        /// <summary>
        /// 空であるか。
        /// </summary>
        public bool Empty { get { return _proc.Count == 0; } }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="threadId">スレッドID。</param>
        public void Init(int threadId)
        {
            _traceProc = WindowProcHook;
            _idHook = NativeMethods.SetWindowsHookEx(HookType, _traceProc, IntPtr.Zero, threadId);
        }

        /// <summary>
        /// 登録。
        /// </summary>
        /// <param name="proc">メッセージ解析メソッド。</param>
        public void Entry(AnalyzeMessage proc)
        {
            _proc.Add(proc, true);
        }

        /// <summary>
        /// 削除。
        /// </summary>
        /// <param name="proc">メッセージ解析メソッド。</param>
        public void Remove(AnalyzeMessage proc)
        {
            _proc.Remove(proc);
            if (0 < _proc.Count)
            {
                return;
            }
            NativeMethods.UnhookWindowsHookEx(_idHook);
            GC.KeepAlive(_traceProc);
            _traceProc = null;
        }

        /// <summary>
        /// メッセージ解析。
        /// </summary>
        /// <param name="handle">ハンドル。</param>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
        protected void AnalyzeMessage(IntPtr handle, int message, IntPtr wparam, IntPtr lparam)
        {
            foreach (KeyValuePair<AnalyzeMessage, bool> element in _proc)
            {
                element.Key(handle, message, wparam, lparam);
            }
        }

        /// <summary>
        /// 次のフック呼び出し
        /// </summary>
        /// <param name="nCode">コード</param>
        /// <param name="wParam">wParam</param>
        /// <param name="lParam">lParam</param>
        /// <returns>結果</returns>
        protected IntPtr CallNextHookEx(int nCode, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.CallNextHookEx(_idHook, nCode, wParam, lParam);
        }

        /// <summary>
        /// フックタイプ
        /// </summary>
        protected abstract int HookType { get; }

        /// <summary>
        /// ウィンドウプロックフック。
        /// </summary>
        /// <param name="hookCode">フックコード。</param>
        /// <param name="wParam">WPARAM。</param>
        /// <param name="lParam">LPARAM。</param>
        /// <returns>戻り値。</returns>
        protected abstract IntPtr WindowProcHook(int hookCode, IntPtr wParam, IntPtr lParam);
    }

}
