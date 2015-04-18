using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.Grasp.ScreenTransition;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type Button that has action that synchronize with the before action.
    /// </summary>
#else
    /// <summary>
    /// 押下時に前の非同期処理を同期させるボタンに対応した操作を提供します。
    /// </summary>
#endif
    public class NativeButtonSync : ClickSync
    {
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">WindowControl with the target window. </param>
        /// <param name="sync">Synchronize.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="src">元となるウィンドウコントロールです。</param>
        /// <param name="sync">前の非同期にした処理を同期させる処理。</param>
#endif
        public NativeButtonSync(WindowControl src, Sync sync)
            : base(() => new NativeButton(src).EmulateClick(), sync) { }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">WindowControl with the target window. </param>
        /// <param name="async">Before action's. asynchronous execution object.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="src">元となるウィンドウコントロールです。</param>
        /// <param name="async">前の非同期にした処理の非同期実行オブジェクト。</param>
#endif
        public NativeButtonSync(WindowControl src, Async async)
            : base(() => new NativeButton(src).EmulateClick(), () => async.WaitForCompletion()) { }
    }
}
