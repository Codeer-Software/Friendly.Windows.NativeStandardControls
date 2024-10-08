using Codeer.Friendly.Windows.Grasp;
using System.Drawing;
using System;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// ToolBar item operation class.
    /// </summary>
#else
    /// <summary>
    /// ツールバーアイテム操作クラス
    /// </summary>
#endif
    public class NativeToolBarItem : IUIObject
    {
        [Serializable]
        public class Core
        {
            public int Id { get; set; }
            public byte State { get; set; }
            public Rectangle Rect { get; set; }
        }

        WindowControl _toolBarWindow;
        Core _core;

#if ENG
        /// <summary>
        /// Returns the associated application manipulation object.
        /// </summary>
#else
        /// <summary>
        /// アプリケーション操作クラスを取得します。
        /// </summary>
#endif
        public WindowsAppFriend App => _toolBarWindow.App;

#if ENG
        /// <summary>
        /// Returns the size of IUIObject.
        /// </summary>
#else
        /// <summary>
        /// IUIObjectのサイズを取得します。
        /// </summary>
#endif
        public Size Size => _core.Rect.Size;

#if ENG
        /// <summary>
        /// ID of the item.
        /// </summary>
#else
        /// <summary>
        /// アイテムのID。
        /// </summary>
#endif
        public int Id => _core.Id;

#if ENG
        /// <summary>
        /// Returns true if the control is enabled.
        /// </summary>
#else
        /// <summary>
        /// 活性/非活性を取得します。
        /// </summary>
#endif
        public bool Enabled => (_core.State & 0x04) == 0x04;

#if ENG
        /// <summary>
        /// Make it active.
        /// </summary>
#else
        /// <summary>
        /// アクティブな状態にします。
        /// </summary>
#endif
        public void Activate() => _toolBarWindow.Activate();

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
        public Point PointToScreen(Point clientPoint)
        {
            var screenPos = _toolBarWindow.PointToScreen(_core.Rect.Location);
            return new Point(screenPos.X + clientPoint.X, screenPos.Y + clientPoint.Y);
        }

        internal NativeToolBarItem(WindowControl toolBarWindow, Core core)
        {
            _toolBarWindow = toolBarWindow;
            _core = core;
        }
    }
}
