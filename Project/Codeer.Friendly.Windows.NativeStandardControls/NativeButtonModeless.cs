using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.Grasp.ScreenTransition;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type Button that has action that show modal dialog.
    /// </summary>
    /// <typeparam name="T">Modeless dialog's window driver type.</typeparam>
#else
    /// <summary>
    /// 押下時にモーダレスダイアログが表示される可能性のあるボタンに対応した操作を提供します。
    /// </summary>
    /// <typeparam name="T">表示されるモーダレスダイアログのウィンドウドライバ。</typeparam>
#endif
    public class NativeButtonModeless<T> : ClickModeless<T>
    {
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">WindowControl with the target window. </param>
        /// <param name="create">Create window driver.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="src">元となるウィンドウコントロールです。</param>
        /// <param name="create">ウィンドウドライバ生成処理。</param>
#endif
        public NativeButtonModeless(WindowControl src, CreateWindowDriver create) 
            : base(() => new NativeButton(src).EmulateClick(), create) { }
    }
}
