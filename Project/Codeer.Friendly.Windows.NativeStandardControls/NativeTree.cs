using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type SysTreeView32.
    /// </summary>    
#else
    /// <summary>
    /// WindowClassがSysTreeView32のウィンドウに対応した操作を提供します。
    /// </summary>    
#endif
    public class NativeTree : NativeWindow
    {
        internal const int TV_FIRST = 0x1100;
        internal const int TVM_GETITEMRECT = (TV_FIRST + 4);
        internal const int TVM_GETITEMA = (TV_FIRST + 12);
        internal const int TVM_GETITEMW = (TV_FIRST + 62);
        internal const int TVM_SETITEMA = (TV_FIRST + 13);
        internal const int TVM_SETITEMW = (TV_FIRST + 63);
        internal const int TVM_SELECTITEM = (TV_FIRST + 11);
        internal const int TVM_EDITLABELA = (TV_FIRST + 14);
        internal const int TVM_EDITLABELW = (TV_FIRST + 65);
        internal const int TVM_GETNEXTITEM = (TV_FIRST + 10);
        internal const int TVM_GETEDITCONTROL = (TV_FIRST + 15);
        internal const int TVM_EXPAND = (TV_FIRST + 2);
        internal const int TVM_ENDEDITLABELNOW = (TV_FIRST + 22);
        internal const int TVM_ENSUREVISIBLE = (TV_FIRST + 20);
        internal const int TVE_COLLAPSE = 0x0001;
        internal const int TVE_EXPAND = 0x0002;
        internal const int TVGN_ROOT = 0x0000;
        internal const int TVGN_NEXT = 0x0001;
        internal const int TVGN_PREVIOUS = 0x0002;
        internal const int TVGN_PARENT = 0x0003;
        internal const int TVGN_CHILD = 0x0004;
        internal const int TVGN_CARET = 0x0009;
        internal const int TVN_FIRST = -400;
        internal const int TVN_ITEMEXPANDINGA = (TVN_FIRST - 5);
        internal const int TVN_ITEMEXPANDINGW = (TVN_FIRST - 54);
        internal const int TVN_ITEMEXPANDEDA = (TVN_FIRST - 6);
        internal const int TVN_ITEMEXPANDEDW = (TVN_FIRST - 55);
        internal const int TVN_SELCHANGEDA = (TVN_FIRST - 2);
        internal const int TVN_SELCHANGEDW = (TVN_FIRST - 51);
        internal const int TVN_ENDLABELEDITA = (TVN_FIRST - 11);
        internal const int TVN_ENDLABELEDITW = (TVN_FIRST - 60);
        internal const int TVM_HITTEST = (TV_FIRST + 17);
        internal const int TVN_KEYDOWN = (TVN_FIRST - 12);
        internal const int TVIS_STATEIMAGEMASK = 0xF000;
        
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
        public NativeTree(WindowControl src)
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
        public NativeTree(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App);
        }
        
#if ENG
        /// <summary>
        /// Returns the item handle of the selected item.
        /// </summary>
#else
        /// <summary>
        /// 選択されているアイテムハンドルです。
        /// </summary>
#endif
        public IntPtr SelectedItem
        {
            get
            {
                return SendMessage(TVM_GETNEXTITEM, new IntPtr(TVGN_CARET), IntPtr.Zero);
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the item handles of the top-level nodes.
        /// </summary>
#else
        /// <summary>
        /// トップレベルのノードのアイテムハンドルです。
        /// </summary>
#endif
        public IntPtr[] Nodes
        {
            get
            {
                return (IntPtr[])App[GetType(), "GetRootNodesInTarget"](Handle).Core;
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the child nodes of the indicated node.
        /// </summary>
        /// <param name="hParentItem">Parent item handle.</param>
        /// <returns>The item handles of the child nodes.</returns>
#else
        /// <summary>
        /// 指定のアイテムの子ノードを取得します。
        /// </summary>
        /// <param name="hParentItem">親アイテムハンドル。</param>
        /// <returns>子ノードのアイテムハンドル。</returns>
#endif
        public IntPtr[] GetChildNodes(IntPtr hParentItem)
        {
            return (IntPtr[])App[GetType(), "GetChildNodesInTarget"](Handle, hParentItem).Core;
        }
        
#if ENG
        /// <summary>
        /// Returns sibling nodes of the indicated node.
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <returns>The item handles of the nodes at the same level as the indicated node.</returns>
#else
        /// <summary>
        /// 指定のアイテムと同列のノードを取得します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>指定のアイテムと同列のノードのアイテムハンドル。</returns>
#endif
        public IntPtr[] GetBrotherNodes(IntPtr hItem)
        {
            return (IntPtr[])App[GetType(), "GetBrotherNodesInTarget"](Handle, hItem).Core;
        }
        
#if ENG
        /// <summary>
        /// Returns an item's parent node.
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <returns>The item handle of the parent node.</returns>
#else
        /// <summary>
        /// 親ノードを取得します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>親ノードのアイテムハンドル。</returns>
#endif
        public IntPtr GetParentNode(IntPtr hItem)
        {
            return SendMessage(TVM_GETNEXTITEM, new IntPtr(TVGN_PARENT), hItem);
        }
        
#if ENG
        /// <summary>
        /// Searches for a node.
        /// </summary>
        /// <param name="nodeText">The text of each node.</param>
        /// <returns>The item handle of the found node or IntPtr.Zero if it is not found.</returns>
#else
        /// <summary>
        /// ノードを検索します。
        /// </summary>
        /// <param name="nodeText">各ノードのテキスト。</param>
        /// <returns>検索されたノードのアイテムハンドル。未発見時はIntPtr.Zeroが返ります。</returns>
#endif
        public IntPtr FindNode(params string[] nodeText)
        {
            return (IntPtr)App[GetType(), "FindNodeInTarget"](Handle, nodeText).Core;
        }
        
#if ENG
        /// <summary>
        /// Searches for a node.
        /// </summary>
        /// <param name="nodeIndex"> The index of each node.</param>
        /// <returns>The item handle of the found node or IntPtr.Zero if it is not found.</returns>
#else
        /// <summary>
        /// ノードを検索します。
        /// </summary>
        /// <param name="nodeIndex">各ノードでのインデックス。</param>
        /// <returns>検索されたノードのアイテムハンドル。未発見時はIntPtr.Zeroが返ります。</returns>
#endif
        public IntPtr FindNode(params int[] nodeIndex)
        {
            return (IntPtr)App[GetType(), "FindNodeInTarget"](Handle, nodeIndex).Core;
        }
        
#if ENG
        /// <summary>
        /// Obtains item information.
        /// </summary>
        /// <param name="item">Item information.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// アイテム情報を取得します。
        /// </summary>
        /// <param name="item">アイテム情報。</param>
        /// <returns>成否。</returns>
#endif
        public bool GetItem(TVITEMEX item)
        {
            AppVar inTarget = App.Dim(item);
            bool ret = (bool)App[GetType(), "GetItemInTarget"](Handle, inTarget).Core;
            TVITEMEX getData = (TVITEMEX)inTarget.Core;
            item._core = getData._core;
            item.pszText = getData.pszText;
            return ret;
        }
        
#if ENG
        /// <summary>
        /// Obtains an item's text.
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <returns>Item text.</returns>
#else
        /// <summary>
        /// アイテム文字列を取得します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>アイテム文字列。</returns>
#endif
        public string GetItemText(IntPtr hItem)
        {
            return (string)App[GetType(), "GetItemTextInTarget"](Handle, hItem).Core;
        }
        
#if ENG
        /// <summary>
        /// Obtains item data for an indicated item.
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <returns>Item data.</returns>
#else
        /// <summary>
        /// アイテムデータを取得します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>アイテムデータ。</returns>
#endif
        public IntPtr GetItemData(IntPtr hItem)
        {
            TVITEMEX item = new TVITEMEX();
            item.hItem = hItem;
            item.mask = TVIF.PARAM;
            GetItem(item);
            return item.lParam;
        }
        
#if ENG
        /// <summary>
        /// Obtains an item's bounds.
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <param name="isTextOnly">True to obtain bounds of only the text portion.</param>
        /// <returns>Item boundary rectangle.</returns>
#else
        /// <summary>
        /// アイテム矩形を取得します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="isTextOnly">テキスト部分のみの取得であるか。</param>
        /// <returns>アイテム矩形。</returns>
#endif
        public Rectangle GetItemRect(IntPtr hItem, bool isTextOnly)
        {
            RECT rc = new RECT();
            if (IntPtr.Size == 4)
            {
                rc.Left = hItem.ToInt32();
            }
            else
            {
                uint ui = (uint)(hItem.ToInt64() & 0xffffffff);
                rc.Left = (int)ui;
                ulong ul = (((ulong)hItem.ToInt64() & 0xffffffff00000000) >> 32);
                ui = (uint)ul;
                rc.Top = (int)ui;
            }
            AppVar inTarget = App.Dim(rc);
            App[typeof(NativeMethods), "SendMessage"](Handle, TVM_GETITEMRECT, NativeDataUtility.ToIntPtr(isTextOnly), inTarget);
            return NativeDataUtility.ToRectangle((RECT)inTarget.Core);
        }
        
#if ENG
        /// <summary>
        /// Returns the indicated item's collapse/expand state.
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <returns>True if the item is expanded, false if not.</returns>
#else
        /// <summary>
        /// 指定のアイテムが展開状態であるかを取得します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>指定のアイテムが展開状態であるか。</returns>
#endif
        public bool IsExpanded(IntPtr hItem)
        {
            return (bool)App[GetType(), "IsExpandedInTarget"](Handle, hItem).Core; 
        }
        
#if ENG
        /// <summary>
        /// Returns the indicated item's check state.
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <returns>True if checked, false if unchecked.</returns>
#else
        /// <summary>
        /// 指定のアイテムがチェック状態であるかを取得します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>チェック状態であるか。</returns>
#endif
        public bool IsChecked(IntPtr hItem)
        {
            return (bool)App[GetType(), "IsCheckedInTarget"](Handle, hItem).Core;
        }
        
#if ENG
        /// <summary>
        /// Makes the indicated item visible.
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// 指定のアイテムを可視状態にします。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>成否。</returns>
#endif
        public bool EnsureVisible(IntPtr hItem)
        {
            return NativeDataUtility.ToBool(SendMessage(TVM_ENSUREVISIBLE, IntPtr.Zero, hItem));
        }
        
#if ENG
        /// <summary>
        /// Sets the value of an item.
        /// Produces TVN_ITEMCHANGING and TVN_ITEMCHANGED notifications when the information in connection with a state changes, depending on the setup of the control.
        /// </summary>
        /// <param name="item">Item information.</param>
#else
        /// <summary>
        /// アイテム情報を設定します。
        /// 状態にかかわる情報が変化した場合、コントロールの設定によっては、TVN_ITEMCHANGING、TVN_ITEMCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="item">アイテム情報。</param>
#endif
        public void EmulateChangeItem(TVITEMEX item)
        {
            App[GetType(), "EmulateChangeItemInTarget"](Handle, item);
        }
        
#if ENG
        /// <summary>
        /// Sets the value of an item.
        /// Produces TVN_ITEMCHANGING and TVN_ITEMCHANGED notifications when the information in connection with a state changes, depending on the setup of the control.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="item">Item information.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// アイテム情報を設定します。
        /// 状態にかかわる情報が変化した場合、コントロールの設定によっては、TVN_ITEMCHANGING、TVN_ITEMCHANGEDの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="item">アイテム情報。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeItem(TVITEMEX item, Async async)
        {
            App[GetType(), "EmulateChangeItemInTarget", async](Handle, item);
        }
        
#if ENG
        /// <summary>
        /// Sets the expanded or collapsed state of an item.
        /// Produces TVN_ITEMEXPANDING and TVN_ITEMEXPANDED notifications if the state changes.
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <param name="isExpanded"> True to expand.</param>
#else
        /// <summary>
        /// 指定のアイテムの展開状態を変更します。
        /// 展開状態に変化があれば、TVN_ITEMEXPANDING、TVN_ITEMEXPANDEDの通知が発生します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="isExpanded">展開状態にするか</param>
#endif
        public void EmulateExpand(IntPtr hItem, bool isExpanded)
        {
            App[GetType(), "EmulateExpandInTarget"](Handle, hItem, IsExpanded(hItem), isExpanded);
        }
        
#if ENG
        /// <summary>
        /// Sets the expanded or collapsed state of an item.
        /// Produces TVN_ITEMEXPANDING and TVN_ITEMEXPANDED notifications if the state changes.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <param name="isExpanded"> True to expand.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 指定のアイテムの展開状態を変更します。
        /// 展開状態に変化があれば、TVN_ITEMEXPANDING、TVN_ITEMEXPANDEDの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="isExpanded">展開状態にするか</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateExpand(IntPtr hItem, bool isExpanded, Async async)
        {
            App[GetType(), "EmulateExpandInTarget", async](Handle, hItem, IsExpanded(hItem), isExpanded);
        }
        
#if ENG
        /// <summary>
        /// Sets the check state of the indicated item.
        /// Produces TVN_ITEMCHANGING and TVN_ITEMCHANGED notifications if the check state changes, depending on the setup of the control.
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <param name="check">True to check.</param>
#else
        /// <summary>
        /// 指定のアイテムをチェック状態にします。
        /// チェック状態が変わった場合、コントロールの設定によっては、TVN_ITEMCHANGING、TVN_ITEMCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="check">チェック。</param>
#endif
        public void EmulateCheck(IntPtr hItem, bool check)
        {
            EmulateChangeItem(CreateCheckChangeItem(hItem, check));
        }
        
#if ENG
        /// <summary>
        /// Sets the check state of the indicated item.
        /// Produces TVN_ITEMCHANGING and TVN_ITEMCHANGED notifications if the check state changes, depending on the setup of the control.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <param name="check">True to check.</param>
        /// <param name="async">Asynchronous execution object.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// 指定のアイテムをチェック状態にします。
        /// チェック状態が変わった場合、コントロールの設定によっては、TVN_ITEMCHANGING、TVN_ITEMCHANGEDの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="check">チェック。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>成否。</returns>
#endif
        public void EmulateCheck(IntPtr hItem, bool check, Async async)
        {
            EmulateChangeItem(CreateCheckChangeItem(hItem, check), async);
        }
        
#if ENG
        /// <summary>
        /// Sets the selected item.
        /// Produces TVN_SELCHANGING and TVN_SELCHANGED notifications if the selection state changes.
        /// </summary>
        /// <param name="hItem">Item handle.</param>
#else
        /// <summary>
        /// 指定のアイテムを選択状態にします。
        /// 選択状態が変化した場合、TVN_SELCHANGING、TVN_SELCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
#endif
        public void EmulateSelectItem(IntPtr hItem)
        {
            App[GetType(), "EmulateSelectItemInTarget"](Handle, hItem);
        }
        
#if ENG
        /// <summary>
        /// Sets the selected item.
        /// Produces TVN_SELCHANGING and TVN_SELCHANGED notifications if the selection state changes.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 指定のアイテムを選択状態にします。
        /// 選択状態が変化した場合、TVN_SELCHANGING、TVN_SELCHANGEDの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateSelectItem(IntPtr hItem, Async async)
        {
            App[GetType(), "EmulateSelectItemInTarget", async](Handle, hItem);
        }
        
#if ENG
        /// <summary>
        /// Edits the text of the indicated item.
        /// Produces TVN_BEGINLABELEDIT and TVN_ENDLABELEDIT notifications.
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <param name="text">Text to set.</param>
#else
        /// <summary>
        /// 指定のアイテムを編集します。
        /// TVN_BEGINLABELEDIT、TVN_ENDLABELEDITが発生します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="text">テキスト。</param>
#endif
        public void EmulateEdit(IntPtr hItem, string text)
        {
            App[GetType(), "EmulateEditInTarget"](Handle, hItem, text);
        }
        
#if ENG
        /// <summary>
        /// Edits the text of the indicated item.
        /// Produces TVN_BEGINLABELEDIT and TVN_ENDLABELEDIT notifications.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="hItem">Item handle.</param>
        /// <param name="text">Text to set.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 指定のアイテムを編集します。
        /// TVN_BEGINLABELEDIT、TVN_ENDLABELEDITが発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="text">テキスト。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateEdit(IntPtr hItem, string text, Async async)
        {
            App[GetType(), "EmulateEditInTarget", async](Handle, hItem, text);
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
            SendMessage(TVM_ENDEDITLABELNOW, NativeDataUtility.ToIntPtr(true), IntPtr.Zero);
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
            SendMessage(TVM_ENDEDITLABELNOW, NativeDataUtility.ToIntPtr(true), IntPtr.Zero, async);
        }

        /// <summary>
        /// アイテム情報を取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="item">アイテム情報。</param>
        /// <returns>アイテム。</returns>
        internal static bool GetItemInTarget(IntPtr handle, TVITEMEX item)
        {
            bool isUni = NativeMethods.IsWindowUnicode(handle);
            int TVM_GETITEM = isUni ? TVM_GETITEMW : TVM_GETITEMA;

            //文字列取得の場合バッファを確保する。
            if ((item.mask & TVIF.TEXT) == TVIF.TEXT)
            {
                item._core.cchTextMax = 256;
            }

            while (true)
            {
                if (0 < item._core.cchTextMax)
                {
                    item._core.pszText = Marshal.AllocCoTaskMem((item._core.cchTextMax + 1) * 8);
                }
                try
                {
                    if (!NativeDataUtility.ToBool(NativeMethods.SendMessage(handle, TVM_GETITEM, IntPtr.Zero, ref item._core)))
                    {
                        return false;
                    }

                    //文字列取得がなければ終了。
                    if (item._core.pszText == IntPtr.Zero)
                    {
                        return true;
                    }

                    //文字列に変換。
                    item.pszText = isUni ? Marshal.PtrToStringUni(item._core.pszText) :
                            Marshal.PtrToStringAnsi(item._core.pszText);

                    //サイズが足りていれば終了。
                    if (item.pszText.Length < item._core.cchTextMax - 1)
                    {
                        return true;
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
        /// トップレベルのノードのアイテムハンドルを取得します。
        /// </summary>
        /// <param name="wndHandle">ウィンドウハンドル。</param>
        /// <returns>トップレベルのノードのアイテムハンドル。</returns>
        private static IntPtr[] GetRootNodesInTarget(IntPtr wndHandle)
        {
            return GetBrotherNodesInTarget(wndHandle, NativeMethods.SendMessage(wndHandle, TVM_GETNEXTITEM, new IntPtr(TVGN_ROOT), IntPtr.Zero));
        }

        /// <summary>
        /// 指定のアイテムの子ノードを取得します。
        /// </summary>
        /// <param name="wndHandle">ウィンドウハンドル。</param>
        /// <param name="hParentItem">親アイテムハンドル。</param>
        /// <returns>子ノードのアイテムハンドル。</returns>
        private static IntPtr[] GetChildNodesInTarget(IntPtr wndHandle, IntPtr hParentItem)
        {
            return GetBrotherNodesInTarget(wndHandle, NativeMethods.SendMessage(wndHandle, TVM_GETNEXTITEM, new IntPtr(TVGN_CHILD), hParentItem));
        }

        /// <summary>
        /// 指定のアイテムと同列のノードを取得します。
        /// </summary>
        /// <param name="wndHandle">ウィンドウハンドル。</param>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>指定のアイテムと同列のノードのアイテムハンドル。</returns>
        private static IntPtr[] GetBrotherNodesInTarget(IntPtr wndHandle, IntPtr hItem)
        {
            if (hItem == IntPtr.Zero)
            {
                return new IntPtr[0];
            }

            //同階層の先頭を探す。
            while (true)
            {
                IntPtr hPrev = GetNextItemInTarget(wndHandle, hItem, TVGN_PREVIOUS);
                if (hPrev == IntPtr.Zero)
                {
                    break;
                }
                hItem = hPrev;
            }

            //順番にリストに詰める。
            List<IntPtr> list = new List<IntPtr>();
            while (hItem != IntPtr.Zero)
            {
                list.Add(hItem);
                hItem = GetNextItemInTarget(wndHandle, hItem, TVGN_NEXT);
            }
            return list.ToArray();
        }

        /// <summary>
        /// ノードを検索します。
        /// </summary>
        /// <param name="wndHandle">ウィンドウハンドル。</param>
        /// <param name="nodeText">各ノードのテキスト。</param>
        /// <returns>検索されたノードのアイテムハンドル。未発見時はIntPtr.Zeroが返ります。</returns>
        private static IntPtr FindNodeInTarget(IntPtr wndHandle, string[] nodeText)
        {
            //ルートに連なるノードを取得
            IntPtr[] nodes = GetRootNodesInTarget(wndHandle);

            //検索
            IntPtr hit = IntPtr.Zero;
            for (int i = 0; i < nodeText.Length; i++)
            {
                hit = IntPtr.Zero;
                for (int j = 0; j < nodes.Length; j++)
                {
                    TVITEMEX item = new TVITEMEX();
                    item.hItem = nodes[j];
                    item.mask = TVIF.TEXT;
                    GetItemInTarget(wndHandle, item);
                    if (item.pszText == nodeText[i])
                    {
                        hit = nodes[j];
                        nodes = GetChildNodesInTarget(wndHandle, nodes[j]);
                        break;
                    }
                }
                if (hit == IntPtr.Zero)
                {
                    return IntPtr.Zero;
                }
            }
            return hit;
        }

        /// <summary>
        /// ノードを検索します。
        /// </summary>
        /// <param name="wndHandle">ウィンドウハンドル。</param>
        /// <param name="nodeIndex">各ノードでのインデックス。</param>
        /// <returns>検索されたノードのアイテムハンドル。未発見時はIntPtr.Zeroが返ります。</returns>
        private static IntPtr FindNodeInTarget(IntPtr wndHandle, int[] nodeIndex)
        {
            //ルートに連なるノードを取得
            IntPtr[] nodes = GetRootNodesInTarget(wndHandle);

            //検索
            IntPtr hit = IntPtr.Zero;
            for (int i = 0; i < nodeIndex.Length; i++)
            {
                hit = IntPtr.Zero;
                if (0 <= nodeIndex[i] && nodeIndex[i] < nodes.Length)
                {
                    hit = nodes[nodeIndex[i]];
                    nodes = GetChildNodesInTarget(wndHandle, nodes[nodeIndex[i]]);
                }
                else
                {
                    return IntPtr.Zero;
                }
            }
            return hit;
        }

        /// <summary>
        /// hItemとcodeで指定された関係のアイテムを取得します。
        /// </summary>
        /// <param name="treeeHandle">ツリーのウィンドウハンドル。</param>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="code">取得する種類。</param>
        /// <returns>hItemとcodeで指定された関係のアイテム。</returns>
        private static IntPtr GetNextItemInTarget(IntPtr treeeHandle, IntPtr hItem, int code)
        {
            return NativeMethods.SendMessage(treeeHandle, TVM_GETNEXTITEM, new IntPtr(code), hItem);
        }

        /// <summary>
        /// アイテム情報を設定します。
        /// 状態にかかわる情報が変化した場合、コントロールの設定によっては、TVN_ITEMCHANGING、TVN_ITEMCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="item">アイテム情報。</param>
        private static void EmulateChangeItemInTarget(IntPtr handle, TVITEMEX item)
        {
            NativeMethods.SetFocus(handle);
            bool isUni = NativeMethods.IsWindowUnicode(handle);
            int TVM_SETITEM = isUni ? TVM_SETITEMW : TVM_SETITEMA;
            item._core.pszText = isUni ? Marshal.StringToCoTaskMemUni(item.pszText) :
                                            Marshal.StringToCoTaskMemAnsi(item.pszText);
            try
            {
                NativeDataUtility.ToBool(NativeMethods.SendMessage(handle, TVM_SETITEM, IntPtr.Zero, ref item._core));
            }
            finally
            {
                Marshal.FreeCoTaskMem(item._core.pszText);
            }
        }

        /// <summary>
        /// チェック状態を変更するためのアイテムを作成します。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="check">チェック状態。</param>
        /// <returns>アイテム。</returns>
        private static TVITEMEX CreateCheckChangeItem(IntPtr hItem, bool check)
        {
            TVITEMEX item = new TVITEMEX();
            item.mask = TVIF.HANDLE | TVIF.STATE;
            item.hItem = hItem;
            item.stateMask = (TVIS)TVIS_STATEIMAGEMASK;
            item.state = (TVIS)((check ? 2 : 1) << 12);
            return item;
        }

        /// <summary>
        /// 指定のアイテムの展開状態を変更します。
        /// 展開状態に変化があれば、TVN_ITEMEXPANDING、TVN_ITEMEXPANDEDの通知が発生します。
        /// </summary>
        /// <param name="wndHandle">ウィンドウハンドル。</param>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="isExpanded">現在の展開状態。</param>
        /// <param name="isExpandedNext">次の展開状態。</param>
        private static void EmulateExpandInTarget(IntPtr wndHandle, IntPtr hItem, bool isExpanded, bool isExpandedNext)
        {
            NativeMethods.SetFocus(wndHandle);
            if (isExpanded == isExpandedNext)
            {
                return;
            }
            int action = isExpandedNext ? TVE_EXPAND : TVE_COLLAPSE;

            //一度展開したことがある場合はこのメッセージで通知が発生しないので追加で通知。
            bool isExpandedOnce = IsExpandedOnce(wndHandle, hItem);
            if (isExpandedOnce)
            {
                NotifyExpandInTarget(true, wndHandle, hItem, action);  
            }
            NativeMethods.SendMessage(wndHandle, TVM_EXPAND, new IntPtr(action), hItem);
            if (isExpandedOnce)
            {
                NotifyExpandInTarget(false, wndHandle, hItem, action);
            }
        }

        /// <summary>
        /// 展開通知
        /// </summary>
        /// <param name="isBefore">展開前通知であるか。</param>
        /// <param name="wndHandle">ウィンドウハンドル。</param>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="action">動作。</param>
        private static void NotifyExpandInTarget(bool isBefore, IntPtr wndHandle, IntPtr hItem, int action)
        {
            bool isUni = NativeMethods.IsWindowUnicode(wndHandle);
            int code = 0;
            if (isBefore)
            {
                code = isUni ? TVN_ITEMEXPANDINGW : TVN_ITEMEXPANDINGA;
            }
            else
            {
                code = isUni ? TVN_ITEMEXPANDEDW : TVN_ITEMEXPANDEDA;
            }

            //情報取得
            TVITEMEX_CORE item = new TVITEMEX_CORE();
            item.hItem = hItem;
            item.mask = (int)(TVIF.IMAGE | TVIF.PARAM | TVIF.STATE | TVIF.HANDLE | TVIF.SELECTEDIMAGE);
            item.stateMask = 0xffff;
            NativeMethods.SendMessage(wndHandle, (isUni ? TVM_GETITEMW : TVM_GETITEMA), IntPtr.Zero, ref item);

            //通知
            NMTREEVIEW notify = new NMTREEVIEW();
            EmulateUtility.InitNotify(wndHandle, code, ref notify.hdr);
            notify.action = action;
            item.GetTVITEMEX(ref notify.itemNew);
            NativeMethods.SendMessage(NativeMethods.GetParent(wndHandle), NativeCommonDefine.WM_NOTIFY, notify.hdr.idFrom, ref notify);
        }

        /// <summary>
        /// 指定のアイテムが展開状態であるかを取得します。
        /// </summary>
        /// <param name="wndHandle">ウィンドウハンドル。</param>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>指定のアイテムが展開状態であるか。</returns>
        private static bool IsExpandedOnce(IntPtr wndHandle, IntPtr hItem)
        {
            TVITEMEX item = new TVITEMEX();
            item.hItem = hItem;
            item.mask = TVIF.STATE;
            item.stateMask = TVIS.EXPANDEDONCE;
            item.state = 0;
            GetItemInTarget(wndHandle, item);
            return (item.state & TVIS.EXPANDEDONCE) != 0;
        }

        /// <summary>
        /// 指定のアイテムを編集します。
        /// TVN_BEGINLABELEDIT、TVN_ENDLABELEDITが発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="windowHandle">ウィンドウハンドル。</param>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="text">テキスト。</param>
        private static void EmulateEditInTarget(IntPtr windowHandle, IntPtr hItem, string text)
        {
            NativeMethods.SetFocus(windowHandle);
            int TVM_EDITLABEL = NativeMethods.IsWindowUnicode(windowHandle) ? TVM_EDITLABELW : TVM_EDITLABELA;
            NativeMethods.SendMessage(windowHandle, TVM_EDITLABEL, IntPtr.Zero, hItem);
            NativeMethods.SetWindowText(NativeMethods.SendMessage(windowHandle, TVM_GETEDITCONTROL, IntPtr.Zero, IntPtr.Zero), text);
            NativeMethods.SendMessage(windowHandle, TVM_ENDEDITLABELNOW, NativeDataUtility.ToIntPtr(false), IntPtr.Zero);
        }

        /// <summary>
        /// 指定のアイテムを選択状態にします。
        /// 選択状態が変化した場合、TVN_SELCHANGING、TVN_SELCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="hItem">アイテムハンドル。</param>
        private static void EmulateSelectItemInTarget(IntPtr handle, IntPtr hItem)
        {
            NativeMethods.SetFocus(handle);
            NativeMethods.SendMessage(handle, TVM_SELECTITEM, new IntPtr(TVGN_CARET), hItem);
        }

        /// <summary>
        /// 指定のアイテムがチェック状態であるかを取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>チェック状態であるか。</returns>
        internal static bool IsCheckedInTarget(IntPtr handle, IntPtr hItem)
        {
            TVITEMEX item = new TVITEMEX();
            item.mask = TVIF.HANDLE | TVIF.STATE;
            item.hItem = hItem;
            item.stateMask = (TVIS)TVIS_STATEIMAGEMASK;
            GetItemInTarget(handle, item);
            return (((int)item.state >> 12) - 1) != 0;
        }

        /// <summary>
        /// 指定のアイテムが展開状態であるかを取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>指定のアイテムが展開状態であるか。</returns>
        internal static bool IsExpandedInTarget(IntPtr handle, IntPtr hItem)
        {
            TVITEMEX item = new TVITEMEX();
            item.hItem = hItem;
            item.mask = TVIF.STATE;
            item.stateMask = TVIS.EXPANDED;
            item.state = 0;
            GetItemInTarget(handle, item);
            return (item.state & TVIS.EXPANDED) != 0;
        }

        /// <summary>
        /// アイテムテキストの取得。
        /// </summary>
        /// <param name="Handle">ウィンドウハンドル。</param>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>アイテムテキスト。</returns>
        internal static string GetItemTextInTarget(IntPtr Handle, IntPtr hItem)
        {
            TVITEMEX item = new TVITEMEX();
            item.hItem = hItem;
            item.mask = TVIF.TEXT;
            GetItemInTarget(Handle, item);
            return item.pszText;
        }
    }
}
