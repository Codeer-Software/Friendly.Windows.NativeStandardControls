using System;
using System.Text;
using System.Drawing;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type ListBox.
    /// </summary>    
#else
    /// <summary>
    /// WindowClassがListBoxのウィンドウに対応した操作を提供します。
    /// </summary>    
#endif
    public class NativeListBox : NativeWindow
    {
        internal const int LB_SETSEL = 0x0185;
        internal const int LB_SETCURSEL = 0x0186;
        internal const int LB_GETSEL = 0x0187;
        internal const int LB_GETCURSEL = 0x0188;
        internal const int LB_GETTEXT = 0x0189;
        internal const int LB_GETTEXTLEN = 0x018A;
        internal const int LB_GETCOUNT = 0x018B;
        internal const int LB_GETTOPINDEX = 0x018E;
        internal const int LB_GETSELCOUNT = 0x0190;
        internal const int LB_GETSELITEMS = 0x0191;
        internal const int LB_SETTOPINDEX = 0x0197;
        internal const int LB_GETITEMRECT = 0x0198;
        internal const int LB_GETITEMDATA = 0x0199;
        internal const int LBN_SELCHANGE = 1;
        internal const int LBN_DBLCLK = 2;
        internal const int LVS_SINGLESEL = 0x4;
        
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
        public NativeListBox(WindowControl src)
            : base(src)
        {
            Initializer.Initialize(App, GetType());
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
        public NativeListBox(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App, GetType());
        }
        
#if ENG
        /// <summary>
        /// Returns the number of items in the control.
        /// </summary>
#else
        /// <summary>
        /// アイテム数です。
        /// </summary>
#endif
        public int Count
        {
            get
            {
                return (int)SendMessage(LB_GETCOUNT, IntPtr.Zero, IntPtr.Zero);
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the currently selected index.
        /// Please use SelectedIndices for multiple selection listboxes.
        /// </summary>
#else
        /// <summary>
        /// 現在選択されているアイテムのインデックスです。
        /// 複数選択リストボックスの場合はItemSelectedIndicesを使用してください。
        /// </summary>
#endif
        public int CurrentSelectedIndex
        {
            get
            {
                return (int)App[GetType(), "GetCurrentSelectedIndexInTarget"](Handle).Core;
            }
        }
        
#if ENG
        /// <summary>
        /// Returns an array of all selected indices.
        /// Please use CurrentSelectedIndex for single selection listboxes.
        /// </summary>
#else
        /// <summary>
        /// 現在選択されているアイテムのインデックスの配列です。
        /// 単一選択リストボックスの場合はCurrentSelectedIndexを使用してください。
        /// </summary>
#endif
        public int[] SelectedIndices
        {
            get
            {
                return (int[])App[GetType(), "GetSelectedIndicesInTarget"](Handle).Core;
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the index of the top item displayed in the list box.
        /// </summary>
#else
        /// <summary>
        /// リストボックスに表示される文字列の先頭インデックスです。
        /// </summary>
#endif
        public int TopIndex
        {
            get
            {
                return (int)SendMessage(LB_GETTOPINDEX, IntPtr.Zero, IntPtr.Zero);
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the data for the indicated item.
        /// </summary>
        /// <param name="index">Index of the item.</param>
        /// <returns>Item data.</returns>
#else
        /// <summary>
        /// アイテムデータを取得します。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <returns>アイテムデータ。</returns>
#endif
        public IntPtr GetItemData(int index)
        {
            return SendMessage(LB_GETITEMDATA, new IntPtr(index), IntPtr.Zero);
        }
        
#if ENG
        /// <summary>
        /// Returns the text of the indicated item.
        /// </summary>
        /// <param name="index">Index of the item.</param>
        /// <returns>Item text.</returns>
#else
        /// <summary>
        /// 指定のインデックスのテキストを取得します。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <returns>テキスト。</returns>
#endif
        public string GetItemText(int index)
        {
            return (string)App[GetType(), "GetTextInTarget"](Handle, index).Core;
        }
        
#if ENG
        /// <summary>
        /// Finds an item with the indicated text.
        /// </summary>
        /// <param name="findStart">Index where searching should start.</param>
        /// <param name="text">Text to search for.</param>
        /// <returns>Index of the item found. Returns -1 if the item is not found.</returns>
#else
        /// <summary>
        /// 指定のテキストのアイテムを検索します。
        /// </summary>
        /// <param name="findStart">検索開始インデックス。</param>
        /// <param name="text">テキスト。</param>
        /// <returns>アイテムインデックス。未発見時は-1が返ります。</returns>
#endif
        public int FindItem(int findStart, string text)
        {
            return (int)App[GetType(), "FindItemInTarget"](Handle, findStart, text).Core;
        }
        
#if ENG
        /// <summary>
        /// Returns the client rectangle of the item with the indicated index.
        /// </summary>
        /// <param name="index">Index of the item.</param>
        /// <returns>Returns -1 in the case of failure.</returns>
#else
        /// <summary>
        /// 指定のインデックスのアイテムのクライアント矩形を取得します。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <returns>失敗時は-1が返ります。</returns>
#endif
        public Rectangle GetItemRect(int index)
        {
            AppVar inTarget = App.Dim(new RECT());
            App[typeof(NativeMethods), "SendMessage"](Handle, LB_GETITEMRECT, new IntPtr(index), inTarget);
            return NativeDataUtility.ToRectangle((RECT)inTarget.Core);
        }
        
#if ENG
        /// <summary>
        /// Scrolls the list so that the item with the indicated index is the top item.
        /// When it is not possible to show the indicated item at the top, the list is scrolled so that the indicated item is visible.        /// </summary>
        /// <param name="index">head index.</param>
#else
        /// <summary>
        /// リストボックスに表示される文字列の先頭インデックスを設定します。
        /// 先頭になれない場合は、表示領域に指定のインデックスのアイテムが入るようにスクロールされます。
        /// </summary>
        /// <param name="index">先頭インデックス。</param>
#endif
        public void SetTopIndex(int index)
        {
            SendMessage(LB_SETTOPINDEX, new IntPtr(index), IntPtr.Zero);
        }
        
#if ENG
        /// <summary>
        /// Changes the currently selected index.
        /// Causes a LBN_SELCHANGE notification.
        /// Please use EmulateChangeSelect for multiple selection list boxes.
        /// </summary>
        /// <param name="index">Index to select.</param>
#else
        /// <summary>
        /// 現在選択されているインデックスを設定します。
        /// LBN_SELCHANGEの通知が発生します。
        /// 複数選択リストボックスの場合はEmulateChangeSelectを使用してください。
        /// </summary>
        /// <param name="index">選択インデックス。</param>
#endif
        public void EmulateChangeCurrentSelectedIndex(int index)
        {
            App[GetType(), "EmulateChangeCurrentSelectedIndexInTarget"](Handle, index);
        }
        
#if ENG
        /// <summary>
        /// Changes the currently selected index.
        /// Causes a LBN_SELCHANGE notification.
        /// Please use EmulateChangeSelect for multiple selection list boxes.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="index">Index to select.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 現在選択されているインデックスを設定します。
        /// LBN_SELCHANGEの通知が発生します。
        /// 複数選択リストボックスの場合はEmulateChangeSelectを使用してください。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="index">選択インデックス。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeCurrentSelectedIndex(int index, Async async)
        {
            App[GetType(), "EmulateChangeCurrentSelectedIndexInTarget", async](Handle, index);
        }
        
#if ENG
        /// <summary>
        /// Sets the selected state of a specific item in the control.
        /// Causes a LBN_SELCHANGE notification.
        /// Please use EmulateChangeCurrentSelectedIndex for single selection list boxes.
        /// </summary>
        /// <param name="index">Index of the item.</param>
        /// <param name="isSelect">Selects the item if true, deselects it if false.</param>
#else
        /// <summary>
        /// 指定のインデックスのアイテムの選択状態を変更します。
        /// LBN_SELCHANGEの通知が発生します。
        /// 単一選択リストボックスの場合はEmulateChangeCurrentSelectedIndexInTargetを使用してください。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <param name="isSelect">選択状態にする場合はtrueを設定します。</param>
#endif
        public void EmulateChangeSelect(int index, bool isSelect)
        {
            App[GetType(), "EmulateChangeSelectInTarget"](Handle, index, isSelect);
        }
        
#if ENG
        /// <summary>
        /// Sets the selected state of a specific item in the control.
        /// Causes a LBN_SELCHANGE notification.
        /// Please use EmulateChangeCurrentSelectedIndex for single selection list boxes.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="index">Index of the item.</param>
        /// <param name="isSelect">Selects the item if true, deselects it if false.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 指定のインデックスのアイテムの選択状態を変更します。
        /// LBN_SELCHANGEの通知が発生します。
        /// 単一選択リストボックスの場合はEmulateChangeCurrentSelectedIndexInTargetを使用してください。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <param name="isSelect">選択状態にする場合はtrueを設定します。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeSelect(int index, bool isSelect, Async async)
        {
            App[GetType(), "EmulateChangeSelectInTarget", async](Handle, index, isSelect);
        }

        /// <summary>
        /// 現在選択されているアイテムのインデックスの配列です。
        /// 単一選択リストボックスの場合はCurrentSelectedIndexを使用してください。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <returns>現在選択されているアイテムのインデックスの配列。</returns>
        internal static int[] GetSelectedIndicesInTarget(IntPtr handle)
        {
            int count = (int)NativeMethods.SendMessage(handle, LB_GETCOUNT, IntPtr.Zero, IntPtr.Zero);
            int[] selectState = new int[count];
            int selCount = (int)NativeMethods.SendMessage(handle, LB_GETSELITEMS, new IntPtr(count), selectState);
            if (selCount == -1)
            {
                return new int[0];
            }
            int[] trim = new int[selCount];
            for (int i = 0; i < selCount; i++)
            {
                trim[i] = selectState[i];
            }
            return trim;
        }

        /// <summary>
        /// 指定のテキストのアイテムを検索します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="findStart">検索開始インデックス。</param>
        /// <param name="text">テキスト。</param>
        /// <returns>アイテムインデックス。未発見時は-1が返ります。</returns>
        private static int FindItemInTarget(IntPtr handle, int findStart, string text)
        {
            int count = (int)NativeMethods.SendMessage(handle, LB_GETCOUNT, IntPtr.Zero, IntPtr.Zero);
            for (int i = findStart; i < count; i++)
            {
                if (GetTextInTarget(handle, i) == text)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 指定のインデックスのテキストを取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="index">インデックス。</param>
        /// <returns>テキスト。</returns>
        private static string GetTextInTarget(IntPtr handle, int index)
        {
            int len = NativeMethods.SendMessage(handle, LB_GETTEXTLEN, new IntPtr(index), IntPtr.Zero).ToInt32();
            StringBuilder builder = new StringBuilder((len + 1) * 8);
            NativeMethods.SendMessage(handle, LB_GETTEXT, new IntPtr(index), builder);
            return builder.ToString();
        }

        /// <summary>
        /// 現在選択されているインデックスを設定します。
        /// LBN_SELCHANGEの通知が発生します。
        /// 複数選択リストボックスの場合はEmulateChangeSelectを使用してください。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="index">選択インデックス。</param>
        private static void EmulateChangeCurrentSelectedIndexInTarget(IntPtr handle, int index)
        {
            NativeMethods.SetFocus(handle);
            NativeMethods.SendMessage(handle, LB_SETCURSEL, new IntPtr(index), IntPtr.Zero);
            EmulateUtility.SendCommand(handle, LBN_SELCHANGE);
        }

        /// <summary>
        /// 指定のインデックスのアイテムの選択状態を変更します。
        /// LBN_SELCHANGEの通知が発生します。
        /// 単一選択リストボックスの場合はEmulateChangeCurrentSelectedIndexInTargetを使用してください。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="index">インデックス。</param>
        /// <param name="isSelect">選択状態にする場合はtrueを設定します。</param>
        private static void EmulateChangeSelectInTarget(IntPtr handle, int index, bool isSelect)
        {
            NativeMethods.SetFocus(handle);
            NativeMethods.SendMessage(handle, LB_SETSEL, NativeDataUtility.ToIntPtr(isSelect), new IntPtr(index));
            EmulateUtility.SendCommand(handle, LBN_SELCHANGE);
        }
        
        /// <summary>
        /// 現在の選択インデックスを取得する。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <returns>現在の選択インデックス。</returns>
        internal static int GetCurrentSelectedIndexInTarget(IntPtr handle)
        {
            return (int)NativeMethods.SendMessage(handle, NativeListBox.LB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
