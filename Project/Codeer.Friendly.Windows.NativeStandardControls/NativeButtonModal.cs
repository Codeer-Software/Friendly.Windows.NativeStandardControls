using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.Grasp.ScreenTransition;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type Button that has action that show modal dialog.
    /// </summary>
    /// <typeparam name="T">Modal dialog's window driver type.</typeparam>
#else
    /// <summary>
    /// 押下時にモーダルダイアログが表示される可能性のあるボタンに対応した操作を提供します。
    /// </summary>
    /// <typeparam name="T">表示されるモーダルダイアログのウィンドウドライバ。</typeparam>
#endif
    public class NativeButtonModal<T> : ClickModal<T>
    {
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">WindowControl with the target window. </param>
        /// <param name="create">Create window driver function.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="src">元となるウィンドウコントロールです。</param>
        /// <param name="create">ウィンドウドライバ生成処理。</param>
#endif
        public NativeButtonModal(WindowControl src, CreateWindowDriver create)
            : base(src.App, a => new NativeButton(src).EmulateClick(a), create) { }
    }
}
