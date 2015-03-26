using System;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Allows interacting with native windows.
    /// </summary>
#else
    /// <summary>
    /// ネイティブウィンドウ基本クラスです。
    /// </summary>
#endif
    public class NativeWindow : WindowControl
    {
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">WindowControl with the target window. </param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="src">元となるウィンドウコントロールです。</param>
#endif
        public NativeWindow(WindowControl src)
            : base(src)
        {
            Initializer.Initialize(App);
        }
        
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
        /// <param name="windowHandle">Window handle.</param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="windowHandle">ウィンドウハンドル。</param>
#endif
        public NativeWindow(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App);
        }
        
#if ENG
        /// <summary>
        /// Returns a value indicating whether the control and all of its parent controls are displayed.
        /// </summary>
#else
        /// <summary>
        /// 表示状態であるか。
        /// </summary>
#endif
        public bool Visible { get { return (bool)App[typeof(NativeMethods), "IsWindowVisible"](Handle).Core; } }
        
#if ENG
        /// <summary>
        /// Returns a value indicating whether the control can respond to user interaction.
        /// </summary>
#else
        /// <summary>
        /// 有効状態であるか。
        /// </summary>
#endif
        public bool Enabled { get { return (bool)App[typeof(NativeMethods), "IsWindowEnabled"](Handle).Core; } }
    }
}
