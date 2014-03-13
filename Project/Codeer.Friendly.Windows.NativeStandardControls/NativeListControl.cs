using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type SysListView32.
    /// </summary>    
#else
    /// <summary>
    /// WindowClassがSysListView32のウィンドウに対応した操作を提供します。
    /// </summary>    
#endif
    public class NativeListControl : NativeWindow
    {
        internal const int LVM_FIRST = 0x1000;
        internal const int LVM_GETITEMRECT = (LVM_FIRST + 14);
        internal const int LVM_GETSUBITEMRECT = (LVM_FIRST + 56);
        internal const int LVM_GETITEMA = (LVM_FIRST + 5);
        internal const int LVM_GETITEMW = (LVM_FIRST + 75);
        internal const int LVM_SETITEMA = (LVM_FIRST + 6);
        internal const int LVM_SETITEMW = (LVM_FIRST + 76);
        internal const int LVM_GETTOPINDEX = (LVM_FIRST + 39);
        internal const int LVM_GETNEXTITEM = (LVM_FIRST + 12);
        internal const int LVM_GETITEMTEXTA = (LVM_FIRST + 45);
        internal const int LVM_GETITEMTEXTW = (LVM_FIRST + 115);
        internal const int LVM_GETITEMCOUNT = (LVM_FIRST + 4);
        internal const int LVM_GETEDITCONTROL = (LVM_FIRST + 24);
        internal const int LVM_GETCOLUMNA = (LVM_FIRST + 25);
        internal const int LVM_GETCOLUMNW = (LVM_FIRST + 95);
        internal const int LVM_EDITLABELA = (LVM_FIRST + 23);
        internal const int LVM_EDITLABELW = (LVM_FIRST + 118);
        internal const int LVM_GETVIEW = (LVM_FIRST + 143);
        internal const int LVM_ENSUREVISIBLE = (LVM_FIRST + 19);
        internal const int LVM_CANCELEDITLABEL = (LVM_FIRST + 179);
        internal const int LVM_GETITEMSTATE = (LVM_FIRST + 44);
        internal const int LVM_SETITEMSTATE = (LVM_FIRST + 43);
        internal const int LVS_ICON = 0x0000;
        internal const int LVS_REPORT = 0x0001;
        internal const int LVS_SMALLICON = 0x0002;
        internal const int LVS_LIST = 0x0003;
        internal const int LVN_FIRST = -100;
        internal const int LVN_ITEMCHANGED = (LVN_FIRST - 1);
        internal const int LVN_ENDLABELEDITA = (LVN_FIRST - 6);
        internal const int LVN_ENDLABELEDITW = (LVN_FIRST - 76);
        
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
        public NativeListControl(WindowControl src)
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
        public NativeListControl(WindowsAppFriend app, IntPtr windowHandle)
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
        /// アイテムの数です。
        /// </summary>
#endif
        public int ItemCount
        {
            get
            {
                return (int)SendMessage(LVM_GETITEMCOUNT, IntPtr.Zero, IntPtr.Zero);
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the currently selected indices.
        /// </summary>
#else
        /// <summary>
        /// 選択されているアイテムのインデックスです。
        /// </summary>
#endif
        public int[] SelectedIndices 
        {
            get
            { 
                return (int[])App[GetType(), "SelectedIndicesInTarget"](Handle).Core; 
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the index of the top item displayed in the list control.
        /// </summary>
#else
        /// <summary>
        /// 先頭インデックスです。
        /// </summary>
#endif
        public int TopIndex
        {
            get
            {
                return (int)SendMessage(LVM_GETTOPINDEX, IntPtr.Zero, IntPtr.Zero);
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the display mode.
        /// </summary>
#else
        /// <summary>
        /// 表示モードを取得します。
        /// </summary>
#endif
        public View View
        {
            get
            {
                int style = (int)(IntPtr)App[typeof(NativeMethods), "GetWindowLongPtr"](Handle, NativeCommonDefine.GWL_STYLE).Core;
                if ((style & LVS_REPORT) == LVS_REPORT)
                {
                    return View.Details;
                }
                else if ((style & LVS_SMALLICON) == LVS_SMALLICON)
                {
                    return View.SmallIcon;
                }
                else if ((style & LVS_LIST) == LVS_LIST)
                {
                    return View.List;
                }
                else
                {
                    return (View)(int)SendMessage(LVM_GETVIEW, IntPtr.Zero, IntPtr.Zero);
                }
            }
        }
        
#if ENG
        /// <summary>
        /// Obtains an item.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// アイテムを取得します。
        /// </summary>
        /// <param name="item">アイテム。</param>
        /// <returns>成否。</returns>
#endif
        public bool GetItem(LVITEM item)
        {
            bool isUni = (bool)App[typeof(NativeMethods), "IsWindowUnicode"](Handle).Core;
            int LVM_GETITEM = isUni ? LVM_GETITEMW : LVM_GETITEMA;
            return NativeDataUtility.ToBool(GetItemCore(isUni, LVM_GETITEM, IntPtr.Zero, item));
        }
        
#if ENG
        /// <summary>
        /// Obtains an item's state.
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <param name="stateMask">State mask.</param>
        /// <returns>Item state.</returns>
#else
        /// <summary>
        /// アイテム状態を取得します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="stateMask">状態マスク。</param>
        /// <returns>アイテム状態。</returns>
#endif
        public LVIS GetItemState(int itemIndex, LVIS stateMask)
        {
            return (LVIS)App[GetType(), "GetItemStateInTarget"](Handle, itemIndex, stateMask).Core;
        }
        
#if ENG
        /// <summary>
        /// Returns an item's bounds.
        /// </summary>
        /// <param name="itemIndex">Item rectangle.</param>
        /// <param name="area">Specifies for which portion bounds should be obtained.</param>
        /// <returns>Item rectangle.</returns>
#else
        /// <summary>
        /// アイテム矩形を取得します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="area">どの部分に概説する矩形かを指定します。</param>
        /// <returns>アイテム矩形。</returns>
#endif
        public Rectangle GetItemRect(int itemIndex, LVIR area)
        {
            RECT rc = new RECT();
            rc.Left = (int)area;
            AppVar inTarget = App.Dim(rc);
            App[typeof(NativeMethods), "SendMessage"](Handle, LVM_GETITEMRECT, new IntPtr(itemIndex), inTarget);
            return NativeDataUtility.ToRectangle((RECT)inTarget.Core);
        }
        
#if ENG
        /// <summary>
        /// Returns the bounds of a subitem.
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <param name="subItemIndex">Sub-item index.</param>
        /// <param name="area">Specifies for which portion bounds should be obtained.</param>
        /// <returns>Subitem rectangle.</returns>
#else
        /// <summary>
        /// サブアイテムに外接する矩形を取得します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="subItemIndex">サブアイテムインデックス。</param>
        /// <param name="area">どの部分に概説する矩形かを指定します。</param>
        /// <returns>サブアイテム矩形。</returns>
#endif
        public Rectangle GetSubItemRect(int itemIndex, int subItemIndex, LVIR area)
        {
            RECT nativeRect = new RECT();
            nativeRect.Top = subItemIndex;
            nativeRect.Left = (int)area;
            AppVar inTarget = App.Dim(nativeRect);
            App[typeof(NativeMethods), "SendMessage"](Handle, LVM_GETSUBITEMRECT, new IntPtr(itemIndex), inTarget);
            return NativeDataUtility.ToRectangle((RECT)inTarget.Core);
        }
        
#if ENG
        /// <summary>
        /// Obtains column information.
        /// </summary>
        /// <param name="col">Column.</param>
        /// <param name="column">Column information.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// 列情報を取得します。
        /// </summary>
        /// <param name="col">列。</param>
        /// <param name="column">列情報。</param>
        /// <returns>成否。</returns>
#endif
        public bool GetColumn(int col, LVCOLUMN column)
        {
            AppVar inTarget = App.Dim(column);
            bool ret = (bool)App[GetType(), "GetColumnInTarget"](Handle, col, inTarget).Core;
            LVCOLUMN data = (LVCOLUMN)inTarget.Core;
            column._core = data._core;
            column._pszText = data._pszText;
            return ret;
        }
        
#if ENG
        /// <summary>
        /// Gets the text for an item.
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <param name="subItemIndex">Sub-item index.</param>
        /// <returns>Item text.</returns>
#else
        /// <summary>
        /// アイテム文字列を取得します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="subItemIndex">サブアイテムインデックス。</param>
        /// <returns>アイテム文字列。</returns>
#endif
        public string GetItemText(int itemIndex, int subItemIndex)
        {
            bool isUni = (bool)App[typeof(NativeMethods), "IsWindowUnicode"](Handle).Core;
            int LVM_GETITEMTEXT = isUni ? LVM_GETITEMTEXTW : LVM_GETITEMTEXTA;
            LVITEM lvi = new LVITEM();
            lvi.iSubItem = subItemIndex;
            GetItemCore(isUni, LVM_GETITEMTEXT, new IntPtr(itemIndex), lvi);
            return lvi.pszText;
        }
        
#if ENG
        /// <summary>
        /// Finds an item with the indicated text.
        /// </summary>
        /// <param name="findStart">Index where searching should start.</param>
        /// <param name="targetSubItemIndex">Sub-item index of the target item.</param>
        /// <param name="text">Text to search for.</param>
        /// <returns>Index of the item found. Returns -1 if the item was not found.</returns>
#else
        /// <summary>
        /// 指定のテキストのアイテムを検索します。
        /// </summary>
        /// <param name="findStart">検索開始インデックス。</param>
        /// <param name="targetSubItemIndex">検索対象のサブアイテムインデックス。</param>
        /// <param name="text">テキスト。</param>
        /// <returns>アイテムインデックス。未発見時は-1が返ります。</returns>
#endif
        public int FindItem(int findStart, int targetSubItemIndex, string text)
        {
            return (int)App[GetType(), "FindItemInTarget"](Handle, findStart, targetSubItemIndex, text).Core;
        }
        
#if ENG
        /// <summary>
        /// Returns item data.
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <returns>Item data.</returns>
#else
        /// <summary>
        /// アイテムデータを取得します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <returns>アイテムデータ。</returns>
#endif
        public IntPtr GetItemData(int itemIndex)
        {
            LVITEM lvi = new LVITEM();
            lvi.iItem = itemIndex;
            lvi.mask = LVIF.PARAM;
            GetItem(lvi);
            return lvi.lParam;
        }
        
#if ENG
        /// <summary>
        /// Displays the indicated item in a visible region.
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <param name="partialOK">Indicates whether partial display is permissible.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// 指定のアイテムを可視領域に表示します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="partialOK">部分表示を許容するかを指定します。</param>
        /// <returns>成否。</returns>
#endif
        public bool EnsureVisible(int itemIndex, bool partialOK)
        {
            return NativeDataUtility.ToBool(SendMessage(LVM_ENSUREVISIBLE, new IntPtr(itemIndex), NativeDataUtility.MAKELPARAM(partialOK ? 1 : 0, 0)));
        }
        
#if ENG
        /// <summary>
        /// Changes the contents of an item.
        /// Causes a notice according to the changes.
        /// </summary>
        /// <param name="item">Item.</param>
#else
        /// <summary>
        /// アイテムの内容を変更します。
        /// 変更内容に応じて通知が発生します。
        /// </summary>
        /// <param name="item">アイテム。</param>
#endif
        public void EmulateChangeItem(LVITEM item)
        {
            App[GetType(), "EmulateChangeItemInTarget"](Handle, item);
        }
        
#if ENG
        /// <summary>
        /// Changes the contents of an item.
        /// Causes a notice according to the changes.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="item">Item.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// アイテムの内容を変更します。
        /// 変更内容に応じて通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="item">アイテム。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeItem(LVITEM item, Async async)
        {
            App[GetType(), "EmulateChangeItemInTarget", async](Handle, item);
        }
        
#if ENG
        /// <summary>
        /// Edits an item in the list.
        /// Causes an LVN_BEGINLABELEDIT and LVN_ENDLABELEDIT notification.
        /// </summary>
        /// <param name="itemIndex">Index of the item to change.</param>
        /// <param name="text">Text to set.</param>
#else
        /// <summary>
        /// 指定のアイテムを編集します。
        /// LVN_BEGINLABELEDIT、LVN_ENDLABELEDITが発生します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="text">テキスト。</param>
#endif
        public void EmulateEdit(int itemIndex, string text)
        {
            App[GetType(), "EmulateEditInTarget"](Handle, itemIndex, text, SelectedIndices);
        }
        
#if ENG
        /// <summary>
        /// Edits an item in the list.
        /// Causes an LVN_BEGINLABELEDIT and LVN_ENDLABELEDIT notification.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <param name="text">Text to set.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 指定のアイテムを編集します。
        /// LVN_BEGINLABELEDIT、LVN_ENDLABELEDITが発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="text">テキスト。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateEdit(int itemIndex, string text, Async async)
        {
            App[GetType(), "EmulateEditInTarget", async](Handle, itemIndex, text, SelectedIndices);
        }
        
#if ENG
        /// <summary>
        /// Cancels editing.
        /// </summary>
#else
        /// <summary>
        /// 編集キャンセル。
        /// </summary>
#endif
        public void EmulateCancelEdit()
        {
            WindowControl edit = new WindowControl(App, SendMessage(LVM_GETEDITCONTROL, IntPtr.Zero, IntPtr.Zero));
            edit.SequentialMessage(
                new MessageInfo(NativeCommonDefine.WM_KEYDOWN, 0x1B, 0x10001),
                new MessageInfo(NativeCommonDefine.WM_CHAR, 0x1B, 0x10001),
                new MessageInfo(NativeCommonDefine.WM_KEYUP, 0x1B, 0xC0010001));
        }
        
#if ENG
        /// <summary>
        ///  Cancels editing asynchronously.
        /// </summary>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        ///  編集キャンセル。
        /// </summary>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateCancelEdit(Async async)
        {
            WindowControl edit = new WindowControl(App, SendMessage(LVM_GETEDITCONTROL, IntPtr.Zero, IntPtr.Zero));
            edit.SequentialMessage(async,
                new MessageInfo(NativeCommonDefine.WM_KEYDOWN, 0x1B, 0x10001),
                new MessageInfo(NativeCommonDefine.WM_CHAR, 0x1B, 0x10001),
                new MessageInfo(NativeCommonDefine.WM_KEYUP, 0x1B, 0xC0010001));
        }
        
#if ENG
        /// <summary>
        /// Changes the selection state of an item.
        /// Causes an LVN_ITEMCHANGING and LVN_ITEMCHANGED notification.
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <param name="isSelect">State to set.</param>
#else
        /// <summary>
        /// アイテムの選択状態を変更します。
        /// LVN_ITEMCHANGING, LVN_ITEMCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="isSelect">選択状態。</param>
#endif
        public void EmulateSelect(int itemIndex, bool isSelect)
        {
            EmulateChangeItemState(itemIndex, LVIS.SELECTED, (isSelect ? LVIS.SELECTED : 0));
        }
        
#if ENG
        /// <summary>
        /// Changes the selection state of an item.
        /// Causes an LVN_ITEMCHANGING and LVN_ITEMCHANGED notification.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <param name="isSelect">State to set.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// アイテムの選択状態を変更します。
        /// LVN_ITEMCHANGING, LVN_ITEMCHANGEDの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="isSelect">選択状態。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateSelect(int itemIndex, bool isSelect, Async async)
        {
            EmulateChangeItemState(itemIndex, LVIS.SELECTED, (isSelect ? LVIS.SELECTED : 0), async);
        }
        
#if ENG
        /// <summary>
        /// Changes an item's state.
        /// Causes an LVN_ITEMCHANGING and LVN_ITEMCHANGED notice.
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <param name="mask">State mask.</param>
        /// <param name="state">State.</param>
#else
        /// <summary>
        /// 状態を変更します。
        /// LVN_ITEMCHANGING, LVN_ITEMCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="mask">状態マスク。</param>
        /// <param name="state">状態。</param>
#endif
        public void EmulateChangeItemState(int itemIndex, LVIS mask, LVIS state)
        {
            App[GetType(), "EmulateChangeItemStateInTarget"](Handle, itemIndex, mask, state);
        }
        
#if ENG
        /// <summary>
        /// Changes an item's state.
        /// Causes an LVN_ITEMCHANGING and LVN_ITEMCHANGED notice.
        /// Executes asynchronously.
        /// </summary>
        /// <param name="itemIndex">Index of the item.</param>
        /// <param name="mask">State mask.</param>
        /// <param name="state">State.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 状態を変更します。
        /// LVN_ITEMCHANGING, LVN_ITEMCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="mask">状態マスク。</param>
        /// <param name="state">状態。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeItemState(int itemIndex, LVIS mask, LVIS state, Async async)
        {
            App[GetType(), "EmulateChangeItemStateInTarget", async](Handle, itemIndex, mask, state);
        }

        /// <summary>
        /// 状態を変更します。
        /// LVN_ITEMCHANGING, LVN_ITEMCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="mask">状態マスク。</param>
        /// <param name="state">状態。</param>
        private static void EmulateChangeItemStateInTarget(IntPtr handle, int itemIndex, LVIS mask, LVIS state)
        {
            NativeMethods.SetFocus(handle);
            LVITEM_CORE lvi = new LVITEM_CORE();
            lvi.iItem = itemIndex;
            lvi.mask = (int)LVIF.STATE;
            lvi.stateMask = (int)mask;
            lvi.state = (int)state;
            NativeMethods.SendMessage(handle, LVM_SETITEMSTATE, new IntPtr(itemIndex), ref lvi);
        }

        /// <summary>
        /// 列情報を取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="col">列。</param>
        /// <param name="column">列情報。</param>
        /// <returns>成否。</returns>
        private static bool GetColumnInTarget(IntPtr handle, int col, LVCOLUMN column)
        {
            bool isUni = NativeMethods.IsWindowUnicode(handle);
            int LVM_GETCOLUMN = isUni ? LVM_GETCOLUMNW : LVM_GETCOLUMNA;

            //文字列取得の場合バッファサイズを指定する。
            if ((column.mask & LVCF.TEXT) == LVCF.TEXT)
            {
                column._core.cchTextMax = 256;
            }

            while (true)
            {
                //バッファ確保。
                if (0 < column._core.cchTextMax)
                {
                    column._core.pszText = Marshal.AllocCoTaskMem((column._core.cchTextMax + 1) * 8);
                }
                try
                {
                    if (!NativeDataUtility.ToBool(NativeMethods.SendMessage(handle, LVM_GETCOLUMN, new IntPtr(col), ref column._core)))
                    {
                        return false;
                    }
                    
                    //文字列取得がない場合は終了。
                    if (column._core.pszText == IntPtr.Zero)
                    {
                        return true;
                    }

                    //文字列に変換。
                    column.pszText = isUni ? Marshal.PtrToStringUni(column._core.pszText) :
                                                Marshal.PtrToStringAnsi(column._core.pszText);

                    //サイズに収まっていれば終了。
                    if (column.pszText.Length < column._core.cchTextMax - 1)
                    {
                        return true;
                    }

                    //リトライ。
                    column._core.cchTextMax *= 2;
                }
                finally
                {
                    if (column._core.pszText != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(column._core.pszText);
                    }
                }
            }
        }

        /// <summary>
        /// 指定のテキストのアイテムを検索します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="findStart">検索開始インデックス。</param>
        /// <param name="targetSubItemIndex">検索対象のサブアイテムインデックス。</param>
        /// <param name="text">テキスト。</param>
        /// <returns>アイテムインデックス。未発見時は-1が返ります。</returns>
        private static int FindItemInTarget(IntPtr handle, int findStart, int targetSubItemIndex, string text)
        {
            bool isUni = NativeMethods.IsWindowUnicode(handle);
            int LVM_GETITEMTEXT = isUni ? LVM_GETITEMTEXTW : LVM_GETITEMTEXTA;
            int count = (int)NativeMethods.SendMessage(handle, LVM_GETITEMCOUNT, IntPtr.Zero, IntPtr.Zero);
            for (int i = findStart; i < count; i++)
            {
                LVITEM lvi = new LVITEM();
                lvi.iSubItem = targetSubItemIndex;
                GetItemCoreInTarget(handle, isUni, LVM_GETITEMTEXT, new IntPtr(i), lvi);
                if (lvi.pszText == text)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// アイテムを取得します。
        /// </summary>
        /// <param name="isUni">ユニコードであるか。</param>
        /// <param name="message">メッセージ。</param>
        /// <param name="wParam">WPARAM。</param>
        /// <param name="item">アイテム。</param>
        /// <returns>成否。</returns>
        private IntPtr GetItemCore(bool isUni, int message, IntPtr wParam, LVITEM item)
        {
            AppVar inTarget = App.Dim(item);
            IntPtr ret = (IntPtr)App[GetType(), "GetItemCoreInTarget"](Handle, isUni, message, wParam, inTarget).Core;
            LVITEM getData = (LVITEM)inTarget.Core;
            item._core = getData._core;
            item.pszText = getData.pszText;
            return ret;
        }

        /// <summary>
        /// アイテムを取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="isUni">ユニコードであるか。</param>
        /// <param name="message">メッセージ。</param>
        /// <param name="wParam">WPARAM。</param>
        /// <param name="item">アイテム。</param>
        /// <returns>成否。</returns>
        private static IntPtr GetItemCoreInTarget(IntPtr handle, bool isUni, int message, IntPtr wParam, LVITEM item)
        {
            //文字列取得の場合バッファを確保する。
            if (((item.mask & LVIF.TEXT) == LVIF.TEXT) ||
                 (message == LVM_GETITEMTEXTW) ||
                 (message == LVM_GETITEMTEXTA))
            {
                item._core.cchTextMax = 256;
            }

            while (true)
            {
                if (0 < item._core.cchTextMax)
                {
                    item._core.pszText = Marshal.AllocCoTaskMem((item._core.cchTextMax + 1) * 8);
                }
                IntPtr ret = IntPtr.Zero;
                try
                {
                    ret = NativeMethods.SendMessage(handle, message, wParam, ref item._core);
                    if (item._core.pszText == IntPtr.Zero)
                    {
                        return ret;
                    }
                    item.pszText = isUni ? Marshal.PtrToStringUni(item._core.pszText) :
                                        Marshal.PtrToStringAnsi(item._core.pszText);

                    //文字列がサイズに収まっていれば終了。
                    if (item.pszText.Length < item._core.cchTextMax - 1)
                    {
                        return ret;
                    }

                    //リトライ。
                    item._core.cchTextMax *= 2;
                }
                finally
                {
                    if (item._core.pszText != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(item._core.pszText);
                    }
                }
            }
        }

        /// <summary>
        /// 選択されているアイテムのインデックスを取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <returns>選択されているアイテムのインデックス。</returns>
        private static int[] SelectedIndicesInTarget(IntPtr handle)
        {
            IntPtr pos = GetFirstSelectedItemPositionInTarget(handle);
            List<int> indices = new List<int>();
            while (pos != IntPtr.Zero)
            {
                indices.Add(GetNextSelectedItemInTarget(handle, ref pos));
            }
            return indices.ToArray();
        }

        /// <summary>
        /// 指定の条件を満たすアイテムを取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="flags">取得方向フラグ。</param>
        /// <returns>アイテムインデックス。未発見時は-1を返します。</returns>
        private static int GetNextItemInTarget(IntPtr handle, int itemIndex, LVIS flags)
        {
            return (int)NativeMethods.SendMessage(handle, LVM_GETNEXTITEM, new IntPtr(itemIndex), NativeDataUtility.MAKELPARAM((int)flags, 0));
        }

        /// <summary>
        /// 最初に選択した項目の位置を取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <returns>アイテム位置情報。選択されていない場合はIntPtr.Zeroが返ります。</returns>
        private static IntPtr GetFirstSelectedItemPositionInTarget(IntPtr handle)
        {
            return new IntPtr(1 + GetNextItemInTarget(handle, -1, LVIS.SELECTED));
        }

        /// <summary>
        /// 選択されているアイテムのインデックスを返します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="pos">アイテム位置情報。次の値に更新されます。</param>
        /// <returns>アイテムインデックス。</returns>
        private static int GetNextSelectedItemInTarget(IntPtr handle, ref IntPtr pos)
        {
            IntPtr nOldPos = new IntPtr((int)pos - 1);
            pos = new IntPtr(1 + GetNextItemInTarget(handle, (int)nOldPos, LVIS.SELECTED));
            return (int)nOldPos;
        }

        /// <summary>
        /// アイテムの内容を変更します。
        /// LVN_ITEMCHANGING, LVN_ITEMCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="item">アイテム。</param>
        private static void EmulateChangeItemInTarget(IntPtr handle, LVITEM item)
        {
            NativeMethods.SetFocus(handle);
            bool isUni = NativeMethods.IsWindowUnicode(handle);
            int LVM_SETITEM = isUni ? LVM_SETITEMW : LVM_SETITEMA;

            item._core.pszText = isUni ? Marshal.StringToCoTaskMemUni(item.pszText) :
                                            Marshal.StringToCoTaskMemAnsi(item.pszText);
            try
            {
                NativeMethods.SendMessage(handle, LVM_SETITEM, IntPtr.Zero, ref item._core);
            }
            finally
            {
                Marshal.FreeCoTaskMem(item._core.pszText);
            }
        }

        /// <summary>
        /// 指定のアイテムを編集します。
        /// LVN_BEGINLABELEDIT、LVN_ENDLABELEDITが発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="text">テキスト。</param>
        /// <param name="currentSelected">現在の選択項目。</param>
        private static void EmulateEditInTarget(IntPtr handle, int itemIndex, string text, int[] currentSelected)
        {
            //全選択を外す
            for (int i = 0; i < currentSelected.Length; i++)
            {
                EmulateChangeItemStateInTarget(handle, currentSelected[i], LVIS.SELECTED, 0);
            }

            //編集開始
            bool isUni = NativeMethods.IsWindowUnicode(handle);
            int LVM_EDITLABEL = isUni ? LVM_EDITLABELW : LVM_EDITLABELA;
            NativeMethods.SetFocus(handle);
            NativeMethods.SendMessage(handle, LVM_EDITLABEL, new IntPtr(itemIndex), IntPtr.Zero);

            //文字列設定
            NativeMethods.SetWindowText(NativeMethods.SendMessage(handle, LVM_GETEDITCONTROL, IntPtr.Zero, IntPtr.Zero), text);

            //フォーカス設定 編集終了
            NativeMethods.SetFocus(handle);
        }

        /// <summary>
        /// アイテム状態を取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="itemIndex">アイテムインデックス。</param>
        /// <param name="stateMask">状態マスク。</param>
        /// <returns>アイテム状態。</returns>
        internal static LVIS GetItemStateInTarget(IntPtr handle, int itemIndex, LVIS stateMask)
        {
            return (LVIS)NativeMethods.SendMessage(handle, NativeListControl.LVM_GETITEMSTATE, new IntPtr(itemIndex), new IntPtr((int)stateMask));
        }
    }
}
