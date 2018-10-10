using System;
using System.Drawing;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type Button. 
    /// </summary>
#else
    /// <summary>
    /// WindowClassがButtonのウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    public class NativeToolBar : NativeWindow
    {
        Type MyType => typeof(NativeToolBar);

        const int WM_USER = 0x0400;
        const int TB_GETBUTTON = (WM_USER + 23);
        const int TB_BUTTONCOUNT = (WM_USER + 24);
        const int TB_GETITEMRECT = (WM_USER + 29);

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
        public NativeToolBar(WindowControl src)
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
        public NativeToolBar(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App);
        }


#if ENG
        /// <summary>
        /// Get the all menu items of the ToolBar.
        /// </summary>
        /// <returns>items.</returns>
#else
        /// <summary>
        /// ツールバーのアイテムを全取得
        /// </summary>
        /// <returns>ツールバーの全アイテム</returns>
#endif
        public NativeToolBarItem[] GetItems()
        {
            var cores = (NativeToolBarItem.Core[])App[MyType, nameof(GetItemsCore)](Handle).Core;
            var items = new NativeToolBarItem[cores.Length];
            for (int i = 0; i < cores.Length; i++)
            {
                items[i] = new NativeToolBarItem(this, cores[i]);
            }
            return items;
        }

        static NativeToolBarItem.Core[] GetItemsCore(IntPtr handle)
        {
            var count = NativeMethods.SendMessage(handle, TB_BUTTONCOUNT, IntPtr.Zero, IntPtr.Zero).ToInt32();
            var items = new NativeToolBarItem.Core[count];

            for (int i = 0; i < count; i++)
            {
                var b = new TBBUTTON();
                NativeMethods.SendMessage(handle, TB_GETBUTTON, new IntPtr(i), ref b);
                var r = new RECT();
                NativeMethods.SendMessage(handle, TB_GETITEMRECT, new IntPtr(i), ref r);
                items[i] = new NativeToolBarItem.Core
                {
                    Id = b.idCommand,
                    State = b.fsState,
                    Rect = new Rectangle(r.Left, r.Top, r.Right - r.Left + 1, r.Bottom - r.Top + 1)
                };
            }
            return items;
        }
    }
}
