using Codeer.Friendly.Windows.Grasp;
using System.Drawing;
using System;
using static Codeer.Friendly.Windows.NativeStandardControls.Inside.NativeMethods;
using System.Text;
using System.Runtime.InteropServices;

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

        internal NativeMenuItem(WindowControl host, RECT rc, int id, bool enabled)
        {
            _hostWindow = host;
            _screenLeftTop = new Point(rc.Left, rc.Top);
            Size = new Size(rc.Right - rc.Left + 1, rc.Bottom - rc.Top + 1);
            Id = id;
            Enabled = enabled;
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
            => GetMenuItem(window, GetMenu(window.Handle), menuItemText);

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
            var window = WindowControl.FromZTop(app);
            return GetMenuItem(window, window.SendMessage(MN_GETHMENU, IntPtr.Zero, IntPtr.Zero), menuItemText);
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
            => GetMenuItem(window, GetMenu(window.Handle), index);

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
            var window = WindowControl.FromZTop(app);
            return GetMenuItem(window, window.SendMessage(MN_GETHMENU, IntPtr.Zero, IntPtr.Zero), index);
        }

        static NativeMenuItem GetMenuItem(WindowControl window, IntPtr menuHandle, string targetMenuText)
        {
            var count = GetMenuItemCount(menuHandle);
            for (int i = 0; i < count; i++)
            {
                var text = new StringBuilder(1024);
                GetMenuString(menuHandle, i, text, text.Capacity, MF_BYPOSITION);
                if (targetMenuText == text.ToString())
                {
                    GetMenuItemRect(window.Handle, menuHandle, i, out var rc);
                    var id = GetMenuItemID(menuHandle, i);
                    var enabled = IsMenuItemEnabled(menuHandle, i);
                    return new NativeMenuItem(window, rc, id, enabled);
                }
            }
            throw new NotSupportedException();
        }

        static NativeMenuItem GetMenuItem(WindowControl window, IntPtr menuHandle, int idnex)
        {
            if (!GetMenuItemRect(window.Handle, menuHandle, idnex, out var rc))
            {
                throw new NotSupportedException();
            }
            var id = GetMenuItemID(menuHandle, idnex);
            var enabled = IsMenuItemEnabled(menuHandle, idnex);
            return new NativeMenuItem(window, rc, id, enabled);
        }

        static bool IsMenuItemEnabled(IntPtr menuHandle, int idnex)
        {
            var mii = new MENUITEMINFO();
            mii.cbSize = (uint)Marshal.SizeOf(typeof(MENUITEMINFO));
            mii.fMask = MIIM_STATE | MIIM_ID;
            var ret = GetMenuItemInfo(menuHandle, (uint)idnex, true, ref mii);
            var enabled = ret && mii.fState == MF_ENABLED;
            return enabled;
        }
    }
}
