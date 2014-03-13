using System.Windows.Forms;
using System.Collections.Generic;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator.Inside
{
    /// <summary>
    /// タイマーメッセージは優先順位が低い(PostMessageより低い)。
    /// ので通常のコントロールの処理がすべて終了したのちに呼び出される。
    /// </summary>
    class TimerInvoker
    {
        Timer _timer;
        List<MethodInvoker> _invokes = new List<MethodInvoker>();

        /// <summary>
        /// 遅延実行。
        /// </summary>
        /// <param name="invoke">実行オブジェクト。</param>
        internal void BeginInvoke(MethodInvoker invoke)
        {
            _invokes.Add(invoke);
            if (_timer != null)
            {
                return;
            }

            _timer = new Timer();
            _timer.Interval = 1;
            _timer.Tick += delegate
            {
                _timer.Stop();
                _timer = null;
                MethodInvoker[] tmp = _invokes.ToArray();
                _invokes.Clear();
                foreach (MethodInvoker element in tmp)
                {
                    element();
                }
            };
            _timer.Start();
        }
    }
}
