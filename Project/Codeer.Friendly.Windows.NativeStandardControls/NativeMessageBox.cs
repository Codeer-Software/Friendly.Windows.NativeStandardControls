using Codeer.Friendly.Windows.Grasp;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Allows interacting with message boxes.
    /// </summary>
#else
    /// <summary>
    /// メッセージボックス対応クラス。
    /// </summary>
#endif
    public class NativeMessageBox
    {
        WindowControl _window;
        string _title;
        string _message;
        
#if ENG
        /// <summary>
        /// Returns a WindowControl object for the main window.
        /// </summary>
#else
        /// <summary>
        /// 本体ウィンドウ。
        /// </summary>
#endif
        public WindowControl Window { get { return _window; } }
        
#if ENG
        /// <summary>
        /// Returns the message box's title (caption). 
        /// </summary>
#else
        /// <summary>
        /// タイトルの取得。
        /// </summary>
#endif
        public string Title { get { return _title; } }
        
#if ENG
        /// <summary>
        /// Returns the message box's message.
        /// </summary>
#else
        /// <summary>
        /// メッセージの取得。
        /// </summary>
#endif
        public string Message { get { return _message; } }
        
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="window">The message box's main window.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="window">メッセージボックス本体。</param>
#endif
        public NativeMessageBox(WindowControl window)
        {
            _window = window;
            _title = _window.GetWindowText();
            _message = string.Empty;
            foreach (WindowControl staticControl in window.GetFromWindowClass("Static"))
            {
                string text = staticControl.GetWindowText();
                if (!string.IsNullOrEmpty(text))
                {
                    _message = text;
                    break;
                }
            }
        }
        
#if ENG
        /// <summary>
        /// Clicks the button with the indicated text.
        /// </summary>
        /// <param name="buttonWindowText">The text of the button. </param>
#else
        /// <summary>
        /// ボタンを押す。
        /// </summary>
        /// <param name="buttonWindowText">ボタンのウィンドウテキスト。</param>
#endif
        public void EmulateButtonClick(string buttonWindowText)
        {
            new NativeButton(_window.IdentifyFromWindowText(buttonWindowText)).EmulateClick();
        }
    }
}
