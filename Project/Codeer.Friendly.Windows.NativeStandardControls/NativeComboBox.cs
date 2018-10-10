using System;
using System.Text;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows whose WindowClass is ComboBox orComboBoxEx32. 
    /// </summary>
#else
    /// <summary>
    /// WindowClassがComboBox、ComboBoxEx32のウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    [ControlDriver(WindowClassName = "ComboBox|ComboBoxEx32")]
    public class NativeComboBox : NativeWindow
    {
        Type MyType => typeof(NativeComboBox);

        internal const int CB_SHOWDROPDOWN = 0x014F;
        internal const int CB_GETDROPPEDSTATE = 0x0157;
        internal const int CB_SETCURSEL = 0x014E;
        internal const int CB_GETCURSEL = 0x0147;
        internal const int CB_GETLBTEXT = 0x0148;
        internal const int CB_GETLBTEXTLEN = 0x0149;
        internal const int CB_SETEDITSEL = 0x0142;
        internal const int CB_GETEDITSEL = 0x0140;
        internal const int CB_GETITEMDATA = 0x0150;
        internal const int CB_GETCOUNT = 0x0146;
        internal const int CBN_SELCHANGE = 1;
        internal const int CBN_EDITCHANGE = 5;
        internal const int CBN_EDITUPDATE = 6;
        internal const int CBN_DROPDOWN = 7;
        internal const int CBN_CLOSEUP = 8;
        internal const int CBN_SELENDOK = 9;
        internal const int CBN_SELENDCANCEL = 10;
        internal const int CBS_DROPDOWNLIST = 0x0003;
        
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
        public NativeComboBox(WindowControl src)
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
        public NativeComboBox(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App);
        }
        
#if ENG
        /// <summary>
        /// Returns the number of items in this combo box.
        /// </summary>
#else
        /// <summary>
        /// アイテム数です。
        /// </summary>
#endif
        public int ItemCount
        {
            get
            {
                return (int)SendMessage(CB_GETCOUNT, IntPtr.Zero, IntPtr.Zero);
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the index of the currently selected item.
        /// </summary>
#else
        /// <summary>
        /// 現在選択されているアイテムのインデックスです。
        /// </summary>
#endif
        public int SelectedItemIndex
        {
            get
            {
                return (int)App[MyType, "GetCurSelInTarget"](Handle).Core;
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the window text.
        /// </summary>
#else
        /// <summary>
        /// 現在選択の文字列です。
        /// </summary>
#endif
        public string Text
        {
            get
            {
                int style = (int)(IntPtr)App[typeof(NativeMethods), "GetWindowLongPtr"](Handle, NativeCommonDefine.GWL_STYLE).Core;
                if ((style & CBS_DROPDOWNLIST) == CBS_DROPDOWNLIST)
                {
                    int index = SelectedItemIndex;
                    return (index < 0 || ItemCount <= index) ? string.Empty : GetItemText(index);
                }
                return GetWindowText();
            }
        }
        
#if ENG
        /// <summary>
        /// The 32-bit value can be set with the dwItemData parameter of a SetItemData member function call. Uses the GetItemDataPtr member function if the 32-bit value to be retrieved is a pointer (void*).
        /// </summary>
        /// <param name="index">The zero-based index of an item in the combo box's list box.</param>
        /// <returns>The 32-bit value associated with the item, or CB_ERR if an error occurs.</returns>
#else
        /// <summary>
        /// アイテムデータを取得します。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <returns>アイテムデータ。</returns>
#endif
        public IntPtr GetItemData(int index)
        {
            return SendMessage(CB_GETITEMDATA, new IntPtr(index), IntPtr.Zero);
        }
        
#if ENG
        /// <summary>
        /// Returns the text for the indicated item. 
        /// </summary>
        /// <param name="index">Target index.</param>
        /// <returns>Text of the item.</returns>
#else
        /// <summary>
        /// 指定のインデックスのテキストを取得します。
        /// </summary>
        /// <param name="index">インデックス。</param>
        /// <returns>テキスト。</returns>
#endif
        public string GetItemText(int index)
        {
            return (string)App[MyType, "GetLBTextInTarget"](Handle, index).Core;
        }
        
#if ENG
        /// <summary>
        /// Searches for an item with the indicated text. 
        /// </summary>
        /// <param name="findStart">Index where searching should start.</param>
        /// <param name="text">Target text.</param>
        /// <returns>
        /// Index of the found item.
        /// Returns -1 if the item is not found. 
        /// </returns>
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
            return (int)App[MyType, "FindItemInTarget"](Handle, findStart, text).Core;
        }
        
#if ENG
        /// <summary>
        /// Sets the selected item.
        /// If the selected index is changed, CBN_SELENDOK and CBN_SELCHANGE are notified.
        /// For a ComboBoxEx32 that is not a dropdown list, CBN_EDITCHANGE is notified as well.
        /// </summary>
        /// <param name="index">Index of the item to select.</param>
#else
        /// <summary>
        /// アイテムを選択します。
        /// 選択位置の変更があった場合、CBN_SELENDOK、CBN_SELCHANGEの通知が発生します。
        /// ComboBoxEx32でドロップダウンリストでない場合はCBN_EDITCHANGEも発生します。
        /// </summary>
        /// <param name="index">アイテムのインデックス。</param>
#endif
        public void EmulateSelectItem(int index)
        {
            App[MyType, "EmulateSelectItemInTarget"](Handle, index);
        }
        
#if ENG
        /// <summary>
        /// Sets the selected item.
        /// If the selected index is changed, CBN_SELENDOK and CBN_SELCHANGE are notified.
        /// For a ComboBoxEx32 that is not a dropdown list, CBN_EDITCHANGE is notified as well.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="index">Index of the item to select.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// アイテムを選択します。
        /// 選択位置の変更があった場合、CBN_SELENDOK、CBN_SELCHANGEの通知が発生します。
        /// ComboBoxEx32でドロップダウンリストでない場合はCBN_EDITCHANGEも発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="index">アイテムのインデックス。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateSelectItem(int index, Async async)
        {
            App[MyType, "EmulateSelectItemInTarget", async](Handle, index);
        }
        
#if ENG
        /// <summary>
        /// Changes the editor text.
        /// Notifies CBN_EDITCHANGE.
        /// If the window is a ComboBox, CBN_EDITUPDATE is notified as well.
        /// </summary>
        /// <param name="newText">The text to set.</param>
#else
        /// <summary>
        /// エディタのテキストを変更します。
        /// CBN_EDITCHANGEの通知が発生します。
        /// ComboBoxの場合はCBN_EDITUPDATEの通知も発生します。
        /// </summary>
        /// <param name="newText">新たなテキスト。</param>
#endif
        public void EmulateChangeEditText(string newText)
        {
            App[MyType, "EmulateChangeEditTextInTarget"](Handle, newText);
        }
        
#if ENG
        /// <summary>
        /// Changes the editor text.
        /// Notifies CBN_EDITCHANGE.
        /// If the window is a ComboBox, CBN_EDITUPDATE is notified as well.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="newText">The text to set.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// エディタのテキストを変更します。
        /// CBN_EDITCHANGEの通知が発生します。
        /// ComboBoxの場合はCBN_EDITUPDATEの通知も発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="newText">新たなテキスト。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeEditText(string newText, Async async)
        {
            App[MyType, "EmulateChangeEditTextInTarget", async](Handle, newText);
        }
        
#if ENG
        /// <summary>
        /// Changes the DropDown Visible state. 
        /// If a dropdown is opened, CBN_DROPDOWN is notified.
        /// If a drop down is closed, CBN_CLOSEUP is notified.
        /// </summary>
        /// <param name="visible">Visible state of the window.</param>
#else
        /// <summary>
        /// リストの表示、非表示を設定します。
        /// 表示した場合はCBN_DROPDOWN、閉じた場合はCBN_CLOSEUPの通知が発生します。
        /// </summary>
        /// <param name="visible">表示するか。</param>
#endif
        public void EmulateChangeDropDownVisible(bool visible)
        {
            App[MyType, "EmulateChangeDropDownVisibleInTarget"](Handle, visible);
        }
        
#if ENG
        /// <summary>
        /// Changes the DropDown Visible state. 
        /// If a dropdown is opened, CBN_DROPDOWN is notified.
        /// If a drop down is closed, CBN_CLOSEUP is notified.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="visible">Visible state of the window.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// リストの表示、非表示を設定します。
        /// 表示した場合はCBN_DROPDOWN、閉じた場合はCBN_CLOSEUPの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="visible">表示するか。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeDropDownVisible(bool visible, Async async)
        {
            App[MyType, "EmulateChangeDropDownVisibleInTarget", async](Handle, visible);
        }

        /// <summary>
        /// 指定のインデックスのテキストを取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="index">インデックス。</param>
        /// <returns>テキスト。</returns>
        internal static string GetLBTextInTarget(IntPtr handle, int index)
        {
            int len = NativeMethods.SendMessage(handle, CB_GETLBTEXTLEN, new IntPtr(index), IntPtr.Zero).ToInt32();
            StringBuilder builder = new StringBuilder((len + 1) * 8);
            NativeMethods.SendMessage(handle, CB_GETLBTEXT, new IntPtr(index), builder);
            return builder.ToString();
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
            int count = (int)NativeMethods.SendMessage(handle, CB_GETCOUNT, IntPtr.Zero, IntPtr.Zero);
            for (int i = findStart; i < count; i++)
            {
                if (GetLBTextInTarget(handle, i) == text)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// アイテムを選択します。
        /// 選択位置の変更があった場合、CBN_SELENDOK、CBN_SELCHANGEの通知が発生します。
        /// ComboBoxEx32でドロップダウンリストでない場合はCBN_EDITCHANGEも発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="index">アイテムのインデックス。</param>
        private static void EmulateSelectItemInTarget(IntPtr handle, int index)
        {
            NativeMethods.SetFocus(handle);
            if (index == (int)NativeMethods.SendMessage(handle, CB_GETCURSEL, IntPtr.Zero, IntPtr.Zero))
            {
                return;
            }
            NativeMethods.SendMessage(handle, CB_SETCURSEL, new IntPtr(index), IntPtr.Zero);
            EmulateUtility.SendCommand(handle, CBN_SELENDOK);
            EmulateUtility.SendCommand(handle, CBN_SELCHANGE);
        }

        /// <summary>
        /// エディタのテキストを変更します。
        /// CBN_EDITCHANGEの通知が発生します。
        /// ComboBoxの場合はCBN_EDITUPDATEの通知も発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="newText">新たなテキスト。</param>
        private static void EmulateChangeEditTextInTarget(IntPtr handle, string newText)
        {
            NativeMethods.SetFocus(handle);
            NativeMethods.SetWindowText(handle, newText);
            if (!IsExInTarget(handle))
            {
                EmulateUtility.SendCommand(handle, CBN_EDITCHANGE);
                EmulateUtility.SendCommand(handle, CBN_EDITUPDATE);
            }
        }

        /// <summary>
        /// リストの表示、非表示を設定します。
        /// 表示した場合はCBN_DROPDOWN、閉じた場合はCBN_SELENDCANCEL、CBN_CLOSEUPの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="visible">表示するか。</param>
        private static void EmulateChangeDropDownVisibleInTarget(IntPtr handle, bool visible)
        {
            if (NativeDataUtility.ToBool(NativeMethods.SendMessage(handle, CB_GETDROPPEDSTATE, IntPtr.Zero, IntPtr.Zero)) == visible)
            {
                return;
            }
            NativeMethods.SetFocus(handle);
            NativeMethods.SendMessage(handle, CB_SHOWDROPDOWN, NativeDataUtility.ToIntPtr(visible), IntPtr.Zero);
        }

        /// <summary>
        /// ComboBoxEx32であるか。
        /// </summary>
        /// <returns>ComboBoxEx32であるか。</returns>
        private static bool IsExInTarget(IntPtr handle)
        {
            StringBuilder className = new StringBuilder(1024);
            NativeMethods.GetClassName(handle, className, 1024);
            return string.Compare(className.ToString(), "ComboBoxEx32", true) == 0;
        }

        /// <summary>
        /// 現在の選択インデックスを取得。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <returns>現在の選択インデックス。</returns>
        internal static int GetCurSelInTarget(IntPtr handle)
        {
            return (int)NativeMethods.SendMessage(handle, NativeComboBox.CB_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
