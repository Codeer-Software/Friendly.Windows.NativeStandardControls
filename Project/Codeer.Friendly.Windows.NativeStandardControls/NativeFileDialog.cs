using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.Grasp.ScreenTransition;
using Codeer.Friendly.Windows.NativeStandardControls.Properties;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Allows interacting with FileDialog.
    /// </summary>
#else
    /// <summary>
    /// ファイルダイアログ対応クラス。
    /// </summary>
#endif
    public class NativeFileDialog
    {
        WindowControl _window;
        InvokeSync.Sync _sync;

#if ENG
        /// <summary>
        /// Open button's text.
        /// </summary>
#else
        /// <summary>
        /// 「開く」ボタン文字列。
        /// </summary>
#endif
        public static string ButtonTextOpen { get; set; }

#if ENG
        /// <summary>
        /// Save button's text.
        /// </summary>
#else
        /// <summary>
        /// 「保存」ボタン文字列。
        /// </summary>
#endif
        public static string ButtonTextSave { get; set; }

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

        static NativeFileDialog()
        {
            ButtonTextOpen = Resources.ButtonOpen;
            ButtonTextSave = Resources.ButtonSave;
            ButtonTextCancel = Resources.ButtonCancel;
        }

#if ENG
        /// <summary>
        /// Open button.
        /// </summary>
#else
        /// <summary>
        /// 「開く」ボタン。
        /// </summary>
#endif
        public NativeButtonModalOrSync<NativeMessageBox> ButtonOpen { get { return CreateButton(ButtonTextOpen); } }

#if ENG
        /// <summary>
        /// Save button.
        /// </summary>
#else
        /// <summary>
        /// 「保存」ボタン。
        /// </summary>
#endif
        public NativeButtonModalOrSync<NativeMessageBox> ButtonSave { get { return CreateButton(ButtonTextSave); } }

#if ENG
        /// <summary>
        /// キャンセル button.
        /// </summary>
#else
        /// <summary>
        /// 「キャンセル」ボタン。
        /// </summary>
#endif
        public NativeButtonSync ButtonCancel { get { return new NativeButtonSync(_window.IdentifyFromWindowText(ButtonTextCancel), _sync); } }

#if ENG
        /// <summary>
        /// OK button.
        /// </summary>
#else
        /// <summary>
        /// 「OK」ボタン。
        /// </summary>
#endif
        public NativeComboBox ComboBoxFilePath { get; private set; }

#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="window">The FileDialogs's main window.</param>
        /// <param name="async">Before action's. asynchronous execution object.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="window">ファイルダイアログ本体。</param>
        /// <param name="async">前の非同期にした処理の非同期実行オブジェクト。</param>
#endif
        public NativeFileDialog(WindowControl window, Async async) : this(window, () => async.WaitForCompletion()) { }


#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="window">The FileDialogs's main window.</param>
        /// <param name="sync">Synchronize.</param>
#else
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="window">ファイルダイアログ本体。</param>
        /// <param name="sync">前の非同期にした処理を同期させる処理。</param>
#endif
        public NativeFileDialog(WindowControl window, InvokeSync.Sync sync)
        {
            _window = window;
            _sync = sync;

            List<WindowControl> combos = new List<WindowControl>();
            combos.AddRange(_window.GetFromWindowClass("ComboBoxEx32"));
            foreach (var e in _window.GetFromWindowClass("ComboBox"))
            {
                if (e.ParentWindow.TypeFullName != "ComboBoxEx32")
                {
                    combos.Add(e);
                }
            }

            int top = 0;
            WindowControl combo = null;
            foreach(var e in combos)
            {
                if (e.GetFromWindowClass("Edit").Length != 1)
                {
                    continue;
                }
                RECT rc;
                GetWindowRect(e.Handle, out rc);
                if (top < rc.Top)
                {
                    top = rc.Top;
                    combo = e;
                }
            };
            ComboBoxFilePath = new NativeComboBox(combo);
        }

        NativeButtonModalOrSync<NativeMessageBox> CreateButton(string text)
        {
            return new NativeButtonModalOrSync<NativeMessageBox>(_window.IdentifyFromWindowText(text), (w, a) => new NativeMessageBox(w, a), _sync);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
    }
}
