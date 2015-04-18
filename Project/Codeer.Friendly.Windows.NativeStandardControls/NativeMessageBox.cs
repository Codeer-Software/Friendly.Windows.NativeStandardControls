using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.Grasp.ScreenTransition;
using Codeer.Friendly.Windows.NativeStandardControls.Properties;

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
        InvokeSync.Sync _sync;

#if ENG
        /// <summary>
        /// OK button's text.
        /// </summary>
#else
        /// <summary>
        /// 「OK」ボタン文字列。
        /// </summary>
#endif
        public static string ButtonTextOK { get; set; }

#if ENG
        /// <summary>
        /// Cancel button's text.
        /// </summary>
#else
        /// <summary>
        /// 「キャンセル」ボタン文字列。
        /// </summary>
#endif
        public static string ButtonTextCancel { get; set; }

#if ENG
        /// <summary>
        /// Yes button's text.
        /// </summary>
#else
        /// <summary>
        /// 「はい」ボタン文字列。
        /// </summary>
#endif
        public static string ButtonTextYes { get; set; }

#if ENG
        /// <summary>
        /// No button's text.
        /// </summary>
#else
        /// <summary>
        /// 「いいえ」ボタン文字列。
        /// </summary>
#endif
        public static string ButtonTextNo { get; set; }

#if ENG
        /// <summary>
        /// Abort button's text.
        /// </summary>
#else
        /// <summary>
        /// 「中止」ボタン文字列。
        /// </summary>
#endif
        public static string ButtonTextAbort { get; set; }

#if ENG
        /// <summary>
        /// Retry button's text.
        /// </summary>
#else
        /// <summary>
        /// 「再試行」ボタン文字列。
        /// </summary>
#endif
        public static string ButtonTextRetry { get; set; }

#if ENG
        /// <summary>
        /// Ignore button's text.
        /// </summary>
#else
        /// <summary>
        /// 「無視」ボタン文字列。
        /// </summary>
#endif
        public static string ButtonTextIgnore { get; set; }

        static NativeMessageBox()
        {
            ButtonTextOK = Resources.ButtonOK;
            ButtonTextCancel = Resources.ButtonCancel;
            ButtonTextYes = Resources.ButtonYes;
            ButtonTextNo = Resources.ButtonNo;
            ButtonTextAbort = Resources.ButtonAbort;
            ButtonTextRetry = Resources.ButtonRetry;
            ButtonTextIgnore = Resources.ButtonIgnore;
        }

        /// <summary>
        /// Button.
        /// </summary>
        public class Button
        {
            NativeMessageBox _msg;
            string _text;
            internal Button(NativeMessageBox msg, string text)
            {
                _msg = msg;
                _text = text;
            }
            
#if ENG
            /// <summary>
            /// Perform Click.
            /// And Synchronize with the before action.
            /// </summary>
#else
            /// <summary>
            /// クリックします。
            /// </summary>
#endif
            public void EmulateClick()
            {
                _msg.EmulateButtonClick(_text);
            }
        }

#if ENG
        /// <summary>
        /// OK button.
        /// </summary>
#else
        /// <summary>
        /// 「OK」ボタン。
        /// </summary>
#endif
        public Button ButtonOK { get; private set; }

#if ENG
        /// <summary>
        /// Cancel button.
        /// </summary>
#else
        /// <summary>
        /// 「キャンセル」ボタン。
        /// </summary>
#endif
        public Button ButtonCancel { get; private set; }

#if ENG
        /// <summary>
        /// Yes button.
        /// </summary>
#else
        /// <summary>
        /// 「はい」ボタン。
        /// </summary>
#endif
        public Button ButtonYes { get; private set; }

#if ENG
        /// <summary>
        /// No button.
        /// </summary>
#else
        /// <summary>
        /// 「いいえ」ボタン。
        /// </summary>
#endif
        public Button ButtonNo { get; private set; }

#if ENG
        /// <summary>
        /// Abort button.
        /// </summary>
#else
        /// <summary>
        /// 「中止」ボタン。
        /// </summary>
#endif
        public Button ButtonAbort { get; private set; }

#if ENG
        /// <summary>
        /// Retry button.
        /// </summary>
#else
        /// <summary>
        /// 「再試行」ボタン。
        /// </summary>
#endif
        public Button ButtonRetry { get; private set; }

#if ENG
        /// <summary>
        /// Ignore button.
        /// </summary>
#else
        /// <summary>
        /// 「無視」ボタン。
        /// </summary>
#endif
        public Button ButtonIgnore { get; private set; }

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
        public NativeMessageBox(WindowControl window) : this(window, () => { }) { }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="window">The message box's main window.</param>
        /// <param name="async">Before action's. asynchronous execution object.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="window">メッセージボックス本体。</param>
        /// <param name="async">前の非同期にした処理の非同期実行オブジェクト。</param>
#endif
        public NativeMessageBox(WindowControl window, Async async) : this(window, () => async.WaitForCompletion()) { }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="window">The message box's main window.</param>
        /// <param name="sync">Synchronize.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="window">メッセージボックス本体。</param>
        /// <param name="sync">前の非同期にした処理を同期させる処理。</param>
#endif
        public NativeMessageBox(WindowControl window, ClickSync.Sync sync)
        {
            _window = window;
            _sync = sync;
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
            ButtonOK = new Button(this, ButtonTextOK);
            ButtonCancel = new Button(this, ButtonTextCancel);
            ButtonYes = new Button(this, ButtonTextYes);
            ButtonNo = new Button(this, ButtonTextNo);
            ButtonAbort = new Button(this, ButtonTextAbort);
            ButtonRetry = new Button(this, ButtonTextRetry);
            ButtonIgnore = new Button(this, ButtonTextIgnore);
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
            _sync();
        }
    }
}
