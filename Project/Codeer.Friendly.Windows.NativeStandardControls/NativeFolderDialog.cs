using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.Grasp.ScreenTransition;
using Codeer.Friendly.Windows.NativeStandardControls.Properties;
using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Allows interacting with FolderDialog.
    /// </summary>
#else
    /// <summary>
    /// フォルダイアログ対応クラス。
    /// </summary>
#endif
    public class NativeFolderDialog
    {
        WindowControl _window;
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

        static NativeFolderDialog()
        {
            ButtonTextOK = Resources.ButtonOK;
            ButtonTextCancel = Resources.ButtonCancel;
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
        public NativeButtonSync ButtonOK { get { return new NativeButtonSync(_window.IdentifyFromWindowText(ButtonTextOK), _sync); } }

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
        /// Folder tree.
        /// </summary>
#else
        /// <summary>
        /// フォルダツリー。
        /// </summary>
#endif
        public Tree TreeFolder { get; private set; }

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
        public NativeFolderDialog(WindowControl window, Async async) : this(window, () => async.WaitForCompletion()) { }


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
        public NativeFolderDialog(WindowControl window, InvokeSync.Sync sync)
        {
            _window = window;
            _sync = sync;
            TreeFolder = new Tree(_window.IdentifyFromWindowClass("SysTreeView32"));
        }

        /// <summary>
        /// Folder tree.
        /// </summary>
        public class Tree
        {
            NativeTree _tree;

            internal Tree(WindowControl src)
            {
                _tree = new NativeTree(src);
            }

#if ENG
            /// <summary>
            /// Select folder.
            /// </summary>
            /// <param name="paths">paths.</param>
#else
            /// <summary>
            /// フォルダ選択。
            /// </summary>
            /// <param name="paths">パス。</param>
#endif
            public void EmulateSelectFolder(params string[] paths)
            {
                for (int i = 1; i <= paths.Length; i++)
                {
                    var itemPath = Take(paths, i);
                    IntPtr item = IntPtr.Zero;
                    while (item == IntPtr.Zero)
                    {
                        item = _tree.FindNode(itemPath);
                        Thread.Sleep(10);
                    }
                    _tree.EmulateExpand(item, true);
                    _tree.EmulateSelectItem(item);
                }
            }

            string[] Take(string[] src, int count)
            {
                string[] dst = new string[count];
                for (int i = 0; i < count; i++)
                {
                    dst[i] = src[i];
                }
                return dst;
            }
        }
    }
}
