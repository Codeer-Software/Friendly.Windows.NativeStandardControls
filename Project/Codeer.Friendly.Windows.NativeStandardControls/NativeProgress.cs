using System;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type msctls_progress32.
    /// </summary>
#else
    /// <summary>
    /// WindowClassがmsctls_progress32のウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(WindowClassName = "msctls_progress32")]
    public class NativeProgress : NativeWindow
    {
        const int PBM_GETRANGE = (NativeCommonDefine.WM_USER + 7);
        const int PBM_GETPOS = (NativeCommonDefine.WM_USER + 8);
        
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
        public NativeProgress(WindowControl src)
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
        public NativeProgress(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App);
        }
        
#if ENG
        /// <summary>
        /// Returns the minimum value.
        /// </summary>
#else
        /// <summary>
        /// 最小値です。
        /// </summary>
#endif
        public int Min
        {
            get
            {
                return (int)SendMessage(PBM_GETRANGE, new IntPtr(1), IntPtr.Zero);
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the maximum value.
        /// </summary>
#else
        /// <summary>
        /// 最大値です。
        /// </summary>
#endif
        public int Max
        {
            get
            {
                return (int)SendMessage(PBM_GETRANGE, IntPtr.Zero, IntPtr.Zero);
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the current value (position).
        /// </summary>
#else
        /// <summary>
        /// 現在位置です。
        /// </summary>
#endif
        public int Pos
        {
            get
            {
                return (int)SendMessage(PBM_GETPOS, IntPtr.Zero, IntPtr.Zero);
            }
        }
    }
}
