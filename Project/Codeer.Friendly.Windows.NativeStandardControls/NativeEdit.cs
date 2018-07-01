using System;
using System.Text;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type Edit, RichEdit20A, and RichEdit20W.
    /// </summary>    
#else
    /// <summary>
    /// WindowClassがEdit、RichEdit20A、RichEdit20Wのウィンドウに対応した操作を提供します。
    /// </summary>    
#endif
    [ControlDriver(WindowClassName = "Edit|RichEdit20A|RichEdit20W")]
    public class NativeEdit : NativeWindow
    {
        internal const int EM_GETSEL = 0x00B0;
        internal const int EM_SETSEL = 0x00B1;
        internal const int EM_SCROLLCARET = 0x00B7;
        internal const int EM_GETLINECOUNT = 0x00BA;
        internal const int EM_GETFIRSTVISIBLELINE = 0x00CE;
        internal const int EN_CHANGE = 0x0300;
        internal const int EN_UPDATE = 0x0400;
        
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
        public NativeEdit(WindowControl src)
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
        public NativeEdit(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App);
        }
        
#if ENG
        /// <summary>
        /// Returns the contained text.
        /// </summary>
#else
        /// <summary>
        /// テキストです。
        /// </summary>
#endif
        public string Text { get { return GetWindowText(); } }
        
#if ENG
        /// <summary>
        /// Returns the number of lines of text.
        /// </summary>
#else
        /// <summary>
        /// 行数です。
        /// </summary>
#endif
        public int LineCount
        {
            get
            {
                return (int)SendMessage(EM_GETLINECOUNT, IntPtr.Zero, IntPtr.Zero).ToInt32();
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the number of the first visible line.
        /// </summary>
#else
        /// <summary>
        /// 一番上の可視行です。
        /// </summary>
#endif
        public int FirstVisibleLine
        {
            get
            {
                return (int)SendMessage(EM_GETFIRSTVISIBLELINE, IntPtr.Zero, IntPtr.Zero);
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the current selection range.
        /// </summary>
        /// <param name="startChar">Starting character index of the current selection.</param>
        /// <param name="endChar">Ending character index of the current selection.</param>
#else
        /// <summary>
        /// 選択範囲を取得します。
        /// </summary>
        /// <param name="startChar">開始文字インデックス。</param>
        /// <param name="endChar">終了文字インデックス。</param>
#endif
        public void GetSelection(ref int startChar, ref int endChar)
        {
            AppVar start = App.Dim((int)0);
            AppVar end = App.Dim((int)0);
            App[typeof(NativeMethods), "SendMessage"](Handle, EM_GETSEL, start, end);
            startChar = (int)start.Core;
            endChar = (int)end.Core;
        }
        
#if ENG
        /// <summary>
        /// Changes the control's text.
        /// Notifies EN_CHANGE and EN_UPDATE.
        /// For RichEdit20A and RichEdit20W, notice only occurs when the event mask is set..
        /// </summary>
        /// <param name="newText">New text</param>
#else
        /// <summary>
        /// テキストを変更します。
        /// EN_CHANGE、EN_UPDATEの通知が発生します。
        /// RichEdit20A、RichEdit20Wの場合は、イベントマスクが設定されている場合のみ通知が発生します。
        /// </summary>
        /// <param name="newText">新たなテキスト。</param>
#endif
        public void EmulateChangeText(string newText)
        {
            App[GetType(), "EmulateChangeTextInTarget"](Handle, newText);
        }
        
#if ENG
        /// <summary>
        /// Changes the control's text.
        /// Notifies EN_CHANGE and EN_UPDATE.
        /// For RichEdit20A and RichEdit20W, notice only occurs when the event mask is set..
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="newText">New text</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// テキストを変更します。
        /// EN_CHANGE、EN_UPDATEの通知が発生します。
        /// RichEdit20A、RichEdit20Wの場合は、イベントマスクが設定されている場合のみ通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="newText">新たなテキスト。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeText(string newText, Async async)
        {
            App[GetType(), "EmulateChangeTextInTarget", async](Handle, newText);
        }
        
#if ENG
        /// <summary>
        /// Changes the text selection.
        /// Selects all text when startChar is 0 and endChar is -1. Cancels the current selection when startChar is -1.
        /// For RichEdit20A and RichEdit20W, notifies EN_SELCHANGE if the event mask is set.
        /// </summary>
        /// <param name="startChar">Character index of the beginning of the selection.</param>
        /// <param name="endChar">Character index of the end of the selection.</param>
#else
        /// <summary>
        /// 選択を設定します。
        /// 開始位置が0で終了位置が-1のときは、すべてのテキストが選択されます。開始位置が-1のときは、現在の選択が解除されます。
        /// RichEdit20A、RichEdit20Wの場合は、イベントマスクが設定されていれば、EN_SELCHANGEの通知が発生します。
        /// </summary>
        /// <param name="startChar">開始文字列</param>
        /// <param name="endChar">終了文字列</param>
#endif
        public void EmulateChangeSelection(int startChar, int endChar)
        {
            App[GetType(), "EmulateChangeSelectionInTarget"](Handle, startChar, endChar);
        }
        
#if ENG
        /// <summary>
        /// Changes the text selection.
        /// Selects all text when startChar is 0 and endChar is -1. Cancels the current selection when startChar is -1.
        /// For RichEdit20A and RichEdit20W, notifies EN_SELCHANGE if the event mask is set.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="startChar">Character index of the beginning of the selection.</param>
        /// <param name="endChar">Character index of the end of the selection.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 選択を設定します。
        /// 開始位置が0で終了位置が-1のときは、すべてのテキストが選択されます。開始位置が-1のときは、現在の選択が解除されます。
        /// RichEdit20A、RichEdit20Wの場合は、イベントマスクが設定されていれば、EN_SELCHANGEの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="startChar">開始文字列</param>
        /// <param name="endChar">終了文字列</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeSelection(int startChar, int endChar, Async async)
        {
            App[GetType(), "EmulateChangeSelectionInTarget", async](Handle, startChar, endChar);
        }

        /// <summary>
        /// テキストを変更します。
        /// EN_UPDATE、EN_CHANGEの通知が発生します。
        /// RichEdit20A、RichEdit20Wの場合は、イベントマスクが設定されている場合のみ通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="newText">新たなテキスト。</param>
        private static void EmulateChangeTextInTarget(IntPtr handle, string newText)
        {
            NativeMethods.SetFocus(handle);
            NativeMethods.SetWindowText(handle, newText);

            //Editの場合はSetWindowTextでイベントが発生しないので明示的に投げてやる
            StringBuilder className = new StringBuilder(1024);
            NativeMethods.GetClassName(handle, className, 1024);
            if (string.Compare(className.ToString(), "Edit", true) == 0)
            {
                EmulateUtility.SendCommand(handle, EN_UPDATE);
                EmulateUtility.SendCommand(handle, EN_CHANGE);
            }
        }

        /// <summary>
        /// 選択を設定します。
        /// 開始位置が0で終了位置が-1のときは、すべてのテキストが選択されます。開始位置が-1のときは、現在の選択が解除されます。
        /// RichEdit20A、RichEdit20Wの場合は、イベントマスクが設定されていれば、EN_SELCHANGEの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="startChar">開始文字列。</param>
        /// <param name="endChar">終了文字列。</param>
        private static void EmulateChangeSelectionInTarget(IntPtr handle, int startChar, int endChar)
        {
            NativeMethods.SetFocus(handle);
            NativeMethods.SendMessage(handle, EM_SETSEL, new IntPtr(startChar), new IntPtr(endChar));
            NativeMethods.SendMessage(handle, EM_SCROLLCARET, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
