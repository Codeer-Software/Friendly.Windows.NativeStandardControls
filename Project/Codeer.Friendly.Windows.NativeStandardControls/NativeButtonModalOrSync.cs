using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.Grasp.ScreenTransition;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type Button that has action that show modal dialog or synchronize with the before action.
    /// </summary>
    /// <typeparam name="T">Modal dialog's window driver type.</typeparam>
#else
    /// <summary>
    /// 押下時にモーダルダイアログが表示される可能性のあり、表示されない場合は前の非同期処理を同期させるボタンに対応した操作を提供します。
    /// </summary>
    /// <typeparam name="T">表示されるモーダルダイアログのウィンドウドライバ。</typeparam>
#endif
    public class NativeButtonModalOrSync<T> : ClickModalOrSync<T>
    {
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">WindowControl with the target window. </param>
        /// <param name="create">Create window driver function.</param>
        /// <param name="sync">Synchronize with the before action.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="src">元となるウィンドウコントロールです。</param>
        /// <param name="create">ウィンドウドライバ生成処理。</param>
        /// <param name="sync">前の非同期にした処理を同期させる処理。</param>
#endif
        public NativeButtonModalOrSync(WindowControl src, InvokeModal<T>.CreateWindowDriver create, InvokeSync.Sync sync)
            : base(src.App, a => new NativeButton(src).EmulateClick(a), create, sync) { }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">WindowControl with the target window. </param>
        /// <param name="create">Create window driver function.</param>
        /// <param name="async">Before action's. asynchronous execution object.</param>
#else
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">元となるウィンドウコントロールです。</param>
        /// <param name="create">ウィンドウドライバ生成処理。</param>
        /// <param name="async">前の非同期にした処理の非同期実行オブジェクト。</param>
#endif
        public NativeButtonModalOrSync(WindowControl src, InvokeModal<T>.CreateWindowDriver create, Async async)
            : base(src.App, a => new NativeButton(src).EmulateClick(a), create, () => async.WaitForCompletion()) { }
    }
}
