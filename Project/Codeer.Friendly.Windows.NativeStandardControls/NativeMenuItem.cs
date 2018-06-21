using Codeer.Friendly.Windows.Grasp;
using System.Drawing;
using System;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Menu item operation class.
    /// </summary>
#else
    /// <summary>
    /// メニューアイテム操作クラス
    /// </summary>
#endif
    public class NativeMenuItem : IUIObject
    {
        WindowControl _hostWindow;
        Point _screenLeftTop;

#if ENG
        /// <summary>
        /// Returns the associated application manipulation object.
        /// </summary>
#else
        /// <summary>
        /// アプリケーション操作クラスを取得します。
        /// </summary>
#endif
        public WindowsAppFriend App => _hostWindow.App;

#if ENG
        /// <summary>
        /// Returns the size of IUIObject.
        /// </summary>
#else
        /// <summary>
        /// IUIObjectのサイズを取得します。
        /// </summary>
#endif
        public Size Size { get; }

#if ENG
        /// <summary>
        /// ID of the menu.
        /// </summary>
#else
        /// <summary>
        /// メニューのID。
        /// </summary>
#endif
        public int Id { get; }

#if ENG
        /// <summary>
        /// Returns menu text.
        /// </summary>
#else
        /// <summary>
        /// メニューテキストを取得します。
        /// </summary>
#endif
        public string Text { get; }

#if ENG
        /// <summary>
        /// Returns true if the control is enabled.
        /// </summary>
#else
        /// <summary>
        /// 活性/非活性を取得します。
        /// </summary>
#endif
        public bool Enabled { get; }

#if ENG
        /// <summary>
        /// Make it active.
        /// </summary>
#else
        /// <summary>
        /// アクティブな状態にします。
        /// </summary>
#endif
        public void Activate() => _hostWindow.Activate();

#if ENG
        /// <summary>
        /// Convert IUIObject's client coordinates to screen coordinates.
        /// </summary>
        /// <param name="clientPoint">client coordinates.</param>
        /// <returns>screen coordinates.</returns>
#else
        /// <summary>
        /// IUIObjectのクライアント座標からスクリーン座標に変換します。
        /// </summary>
        /// <param name="clientPoint">クライアント座標</param>
        /// <returns>スクリーン座標</returns>
#endif
        public Point PointToScreen(Point clientPoint) => new Point(_screenLeftTop.X + clientPoint.X, _screenLeftTop.Y + clientPoint.Y);

        NativeMenuItem(WindowControl host, Core core)
        {
            _hostWindow = host;
            _screenLeftTop = new Point(core.Rect.Left, core.Rect.Top);
            Size = new Size(core.Rect.Right - core.Rect.Left + 1, core.Rect.Bottom - core.Rect.Top + 1);
            Id = core.Id;
            Text = core.Text;
            Enabled = core.Enabled;
        }

#if ENG
        /// <summary>
        /// Get the menu item of the specified window.
        /// </summary>
        /// <param name="window">Window.</param>
        /// <param name="menuItemText">Text of menu item.</param>
        /// <returns>NaiveMenuItem.</returns>
#else
        /// <summary>
        /// 指定のウィンドウの持つメニューアイテムを取得
        /// </summary>
        /// <param name="window">ウィンドウ</param>
        /// <param name="menuItemText">メニューアイテムの文字列</param>
        /// <returns>メニューアイテム</returns>
#endif
        public static NativeMenuItem GetMenuItem(WindowControl window, string menuItemText)
        {
            Initializer.Initialize(window.App);
            return GetMenuItem(window, (IntPtr)window.App[typeof(NativeMethods), "GetMenu"](window.Handle).Core, menuItemText);
        }

#if ENG
        /// <summary>
        /// Get the menu items of the displayed popup menu.
        /// </summary>
        /// <param name="app">application manipulation object.</param>
        /// <param name="menuItemText">Text of menu item.</param>
        /// <returns>NaiveMenuItem.</returns>
#else
        /// <summary>
        /// 表示されているポップアップメニューの持つメニューアイテムを取得
        /// </summary>
        /// <param name="app">アプリケーション操作クラス</param>
        /// <param name="menuItemText">メニューアイテムの文字列</param>
        /// <returns>メニューアイテム</returns>
#endif
        public static NativeMenuItem GetPopupMenuItem(WindowsAppFriend app, string menuItemText)
        {
            Initializer.Initialize(app);
            var window = WindowControl.FromZTop(app);
            return GetMenuItem(window, window.SendMessage(NativeMethods.MN_GETHMENU, IntPtr.Zero, IntPtr.Zero), menuItemText);
        }

#if ENG
        /// <summary>
        /// Get the menu item of the specified window.
        /// </summary>
        /// <param name="window">Window.</param>
        /// <param name="index">Index of menu item.</param>
        /// <returns>NaiveMenuItem.</returns>
#else
        /// <summary>
        /// 指定のウィンドウの持つメニューアイテムを取得
        /// </summary>
        /// <param name="window">ウィンドウ</param>
        /// <param name="index">メニューアイテムのインデックス</param>
        /// <returns>メニューアイテム</returns>
#endif
        public static NativeMenuItem GetMenuItem(WindowControl window, int index)
        {
            Initializer.Initialize(window.App);
            return GetMenuItem(window, (IntPtr)window.App[typeof(NativeMethods), "GetMenu"](window.Handle).Core, index);
        }

#if ENG
        /// <summary>
        /// Get the menu items of the displayed popup menu.
        /// </summary>
        /// <param name="app">application manipulation object.</param>
        /// <param name="index">Index of menu item.</param>
        /// <returns>NaiveMenuItem.</returns>
#else
        /// <summary>
        /// 表示されているポップアップメニューの持つメニューアイテムを取得
        /// </summary>
        /// <param name="app">アプリケーション操作クラス</param>
        /// <param name="index">メニューアイテムのインデックス</param>
        /// <returns>メニューアイテム</returns>
#endif
        public static NativeMenuItem GetPopupMenuItem(WindowsAppFriend app, int index)
        {
            Initializer.Initialize(app);
            var window = WindowControl.FromZTop(app);
            return GetMenuItem(window, window.SendMessage(NativeMethods.MN_GETHMENU, IntPtr.Zero, IntPtr.Zero), index);
        }


#if ENG
        /// <summary>
        /// Get the all menu items of the specified window.
        /// </summary>
        /// <param name="window">Window.</param>
        /// <returns>NaiveMenuItem[].</returns>
#else
        /// <summary>
        /// 指定のウィンドウの持つメニューアイテムを全取得
        /// </summary>
        /// <param name="window">ウィンドウ</param>
        /// <returns>メニューアイテム</returns>
#endif
        public static NativeMenuItem[] GetMenuItems(WindowControl window)
        {
            Initializer.Initialize(window.App);
            return GetMenuItems(window, (IntPtr)window.App[typeof(NativeMethods), "GetMenu"](window.Handle).Core);
        }

#if ENG
        /// <summary>
        /// Get the all menu items of the displayed popup menu.
        /// </summary>
        /// <param name="app">application manipulation object.</param>
        /// <returns>NaiveMenuItem[].</returns>
#else
        /// <summary>
        /// 表示されているポップアップメニューの持つメニューアイテムを取得
        /// </summary>
        /// <param name="app">アプリケーション操作クラス</param>
        /// <returns>メニューアイテム</returns>
#endif
        public static NativeMenuItem[] GetPopupMenuItems(WindowsAppFriend app)
        {
            Initializer.Initialize(app);
            var window = WindowControl.FromZTop(app);
            return GetMenuItems(window, window.SendMessage(NativeMethods.MN_GETHMENU, IntPtr.Zero, IntPtr.Zero));
        }

        static NativeMenuItem GetMenuItem(WindowControl window, IntPtr menuHandle, string targetMenuText)
        {
            Core core = (Core)window.App[typeof(NativeMenuItem), nameof(GetMenuItem)](window.Handle, menuHandle, targetMenuText).Core;
            return new NativeMenuItem(window, core);
        }

        static NativeMenuItem GetMenuItem(WindowControl window, IntPtr menuHandle, int index)
        {
            Core core = (Core)window.App[typeof(NativeMenuItem), nameof(GetMenuItem)](window.Handle, menuHandle, index).Core;
            return new NativeMenuItem(window, core);
        }

        static NativeMenuItem[] GetMenuItems(WindowControl window, IntPtr menuHandle)
        {
            Core[] cores = (Core[])window.App[typeof(NativeMenuItem), nameof(GetMenuItems)](window.Handle, menuHandle).Core;
            var items = new NativeMenuItem[cores.Length];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new NativeMenuItem(window, cores[i]);
            }
            return items;
        }
        
        static Core[] GetMenuItems(IntPtr hwnd, IntPtr menuHandle)
        {
            var count = NativeMethods.GetMenuItemCount(menuHandle);
            if (count < 0) count = 0;

            var items = new Core[count];
            for (int i = 0; i < count; i++)
            {
                NativeMethods.GetMenuItemRect(hwnd, menuHandle, i, out var rc);
                var id = NativeMethods.GetMenuItemID(menuHandle, i);
                if (!GetMenuItemState(menuHandle, i, out var text, out var enabled)) return null;
                items[i] = new Core(rc, id, text, enabled);
            }
            return items;
        }

        static Core GetMenuItem(IntPtr hwnd, IntPtr menuHandle, string targetMenuText)
        {
            var count = NativeMethods.GetMenuItemCount(menuHandle);
            for (int i = 0; i < count; i++)
            {
                if (!GetMenuItemState(menuHandle, i, out var text, out var enabled)) return null;
                if (targetMenuText == text)
                {
                    NativeMethods.GetMenuItemRect(hwnd, menuHandle, i, out var rc);
                    var id = NativeMethods.GetMenuItemID(menuHandle, i);
                    return new Core(rc, id, text.ToString(), enabled);
                }
            }
            return null;
        }

        static Core GetMenuItem(IntPtr hwnd, IntPtr menuHandle, int index)
        {
            if (!NativeMethods.GetMenuItemRect(hwnd, menuHandle, index, out var rc))
            {
                return null;
            }
            var id = NativeMethods.GetMenuItemID(menuHandle, index);
            if (!GetMenuItemState(menuHandle, index, out var text, out var enabled)) return null;
            return new Core(rc, id, text, enabled);
        }

        static bool GetMenuItemState(IntPtr menuHandle, int index, out string text, out bool enabled)
        {
            var mii = new NativeMethods.MENUITEMINFO(NativeMethods.MIIM.STATE | NativeMethods.MIIM.ID | NativeMethods.MIIM.STRING);     
            var ret = NativeMethods.GetMenuItemInfo(menuHandle, (uint)index, true, mii);
            if (ret)
            {
                ++mii.cch;
                mii.dwTypeData = new String(' ', (int)mii.cch);
                ret = NativeMethods.GetMenuItemInfo(menuHandle, (uint)index, true, mii);
            }
            enabled = ret && !IsOn(mii.fState, NativeMethods.MF_DISABLED);
            text = mii.dwTypeData;
            return ret;
        }

        static bool IsOn(uint fState, int flg) => (fState & flg) == flg;

        [Serializable]
        class Core
        {
            public RECT Rect { get; set; }
            public int Id { get; set; }
            public string Text { get; set; }
            public bool Enabled { get; set; }

            public Core(RECT rc, int id, string text, bool enabled)
            {
                Rect = rc;
                Id = id;
                Text = text;
                Enabled = enabled;
            }
        }
    }
}
