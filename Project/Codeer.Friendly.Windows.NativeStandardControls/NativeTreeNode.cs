using System;
using System.Drawing;
using System.Collections.Generic;
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
    public class NativeTreeNode
    {
        NativeTree _tree;

        public IntPtr ItemHandle { get; }

#if ENG
        /// <summary>
        /// Returns the indicated item's selected state.
        /// </summary>
#else
        /// <summary>
        /// 指定のアイテムが選択されているかを取得します。
        /// </summary>
#endif
        public bool IsSelected => _tree.SelectedItem == ItemHandle;

#if ENG
        /// <summary>
        /// Returns the indicated item's text.
        /// </summary>
#else
        /// <summary>
        /// 指定のアイテムのテキストを取得します。
        /// </summary>
#endif
        public string Text => _tree.GetItemText(ItemHandle);

#if ENG
        /// <summary>
        /// Returns the indicated item's collapse/expand state.
        /// </summary>
#else
        /// <summary>
        /// 指定のアイテムが展開状態であるかを取得します。
        /// </summary>
#endif
        public bool IsExpanded => _tree.IsExpanded(ItemHandle);

#if ENG
        /// <summary>
        /// Returns the indicated item's check state.
        /// </summary>
#else
        /// <summary>
        /// 指定のアイテムがチェック状態であるかを取得します。
        /// </summary>
#endif
        public bool IsChecked => _tree.IsChecked(ItemHandle);

        internal NativeTreeNode(NativeTree tree, IntPtr handle)
        {
            _tree = tree;
            ItemHandle = handle;
        }

#if ENG
        /// <summary>
        /// Returns the child nodes of the indicated node.
        /// </summary>
        /// <returns>The item handles of the child nodes.</returns>
#else
        /// <summary>
        /// 指定のアイテムの子ノードを取得します。
        /// </summary>
        /// <returns>子ノードのアイテムハンドル。</returns>
#endif
        public NativeTreeNode[] GetChildNodes()
        {
            var list = new List<NativeTreeNode>();
            foreach (var e in _tree.GetChildNodes(ItemHandle))
            {
                list.Add(new NativeTreeNode(_tree, e));
            }
            return list.ToArray();
        }

#if ENG
        /// <summary>
        /// Returns an item's parent node.
        /// </summary>
        /// <returns>The item handle of the parent node.</returns>
#else
        /// <summary>
        /// 親ノードを取得します。
        /// </summary>
        /// <returns>親ノードのアイテムハンドル。</returns>
#endif
        public NativeTreeNode GetParentNode()
        {
            var e = _tree.GetParentNode(ItemHandle);
            return e == IntPtr.Zero ? null : new NativeTreeNode(_tree, e);
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
            item.hItem = ItemHandle;
            return _tree.GetItem(item);
        }

#if ENG
        /// <summary>
        /// Obtains item data for an indicated item.
        /// </summary>
        /// <returns>Item data.</returns>
#else
        /// <summary>
        /// アイテムデータを取得します。
        /// </summary>
        /// <returns>アイテムデータ。</returns>
#endif
        public IntPtr GetItemData() => _tree.GetItemData(ItemHandle);

#if ENG
        /// <summary>
        /// Obtains an item's bounds.
        /// </summary>
        /// <param name="isTextOnly">True to obtain bounds of only the text portion.</param>
        /// <returns>Item boundary rectangle.</returns>
#else
        /// <summary>
        /// アイテム矩形を取得します。
        /// </summary>
        /// <param name="isTextOnly">テキスト部分のみの取得であるか。</param>
        /// <returns>アイテム矩形。</returns>
#endif
        public Rectangle GetItemRect(bool isTextOnly) => _tree.GetItemRect(ItemHandle, isTextOnly);

#if ENG
        /// <summary>
        /// Makes the indicated item visible.
        /// </summary>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// 指定のアイテムを可視状態にします。
        /// </summary>
        /// <returns>成否。</returns>
#endif
        public bool EnsureVisible() => _tree.EnsureVisible(ItemHandle);

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
            item.hItem = ItemHandle;
            _tree.EmulateChangeItem(item);
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
            item.hItem = ItemHandle;
            _tree.EmulateChangeItem(item, async);
        }

#if ENG
        /// <summary>
        /// Sets the expanded or collapsed state of an item.
        /// Produces TVN_ITEMEXPANDING and TVN_ITEMEXPANDED notifications if the state changes.
        /// </summary>
        /// <param name="isExpanded"> True to expand.</param>
#else
        /// <summary>
        /// 指定のアイテムの展開状態を変更します。
        /// 展開状態に変化があれば、TVN_ITEMEXPANDING、TVN_ITEMEXPANDEDの通知が発生します。
        /// </summary>
        /// <param name="isExpanded">展開状態にするか</param>
#endif
        public void EmulateExpand(bool isExpanded) => _tree.EmulateExpand(ItemHandle, isExpanded);

#if ENG
        /// <summary>
        /// Sets the expanded or collapsed state of an item.
        /// Produces TVN_ITEMEXPANDING and TVN_ITEMEXPANDED notifications if the state changes.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="isExpanded"> True to expand.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 指定のアイテムの展開状態を変更します。
        /// 展開状態に変化があれば、TVN_ITEMEXPANDING、TVN_ITEMEXPANDEDの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="isExpanded">展開状態にするか</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateExpand(bool isExpanded, Async async) => _tree.EmulateExpand(ItemHandle, isExpanded, async);

#if ENG
        /// <summary>
        /// Sets the check state of the indicated item.
        /// Produces TVN_ITEMCHANGING and TVN_ITEMCHANGED notifications if the check state changes, depending on the setup of the control.
        /// </summary>
        /// <param name="check">True to check.</param>
#else
        /// <summary>
        /// 指定のアイテムをチェック状態にします。
        /// チェック状態が変わった場合、コントロールの設定によっては、TVN_ITEMCHANGING、TVN_ITEMCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="check">チェック。</param>
#endif
        public void EmulateCheck(bool check) => _tree.EmulateCheck(ItemHandle, check);

#if ENG
        /// <summary>
        /// Sets the check state of the indicated item.
        /// Produces TVN_ITEMCHANGING and TVN_ITEMCHANGED notifications if the check state changes, depending on the setup of the control.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="check">True to check.</param>
        /// <param name="async">Asynchronous execution object.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// 指定のアイテムをチェック状態にします。
        /// チェック状態が変わった場合、コントロールの設定によっては、TVN_ITEMCHANGING、TVN_ITEMCHANGEDの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="check">チェック。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
        /// <returns>成否。</returns>
#endif
        public void EmulateCheck(bool check, Async async) => _tree.EmulateCheck(ItemHandle, check, async);

#if ENG
        /// <summary>
        /// Sets the selected item.
        /// Produces TVN_SELCHANGING and TVN_SELCHANGED notifications if the selection state changes.
        /// </summary>
#else
        /// <summary>
        /// 指定のアイテムを選択状態にします。
        /// 選択状態が変化した場合、TVN_SELCHANGING、TVN_SELCHANGEDの通知が発生します。
        /// </summary>
#endif
        public void EmulateSelectItem() => _tree.EmulateSelectItem(ItemHandle);

#if ENG
        /// <summary>
        /// Sets the selected item.
        /// Produces TVN_SELCHANGING and TVN_SELCHANGED notifications if the selection state changes.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 指定のアイテムを選択状態にします。
        /// 選択状態が変化した場合、TVN_SELCHANGING、TVN_SELCHANGEDの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateSelectItem(Async async) => _tree.EmulateSelectItem(ItemHandle, async);

#if ENG
        /// <summary>
        /// Edits the text of the indicated item.
        /// Produces TVN_BEGINLABELEDIT and TVN_ENDLABELEDIT notifications.
        /// </summary>
        /// <param name="text">Text to set.</param>
#else
        /// <summary>
        /// 指定のアイテムを編集します。
        /// TVN_BEGINLABELEDIT、TVN_ENDLABELEDITが発生します。
        /// </summary>
        /// <param name="text">テキスト。</param>
#endif
        public void EmulateEdit(string text) => _tree.EmulateEdit(ItemHandle, text);

#if ENG
        /// <summary>
        /// Edits the text of the indicated item.
        /// Produces TVN_BEGINLABELEDIT and TVN_ENDLABELEDIT notifications.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="text">Text to set.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// 指定のアイテムを編集します。
        /// TVN_BEGINLABELEDIT、TVN_ENDLABELEDITが発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="text">テキスト。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateEdit(string text, Async async) => _tree.EmulateEdit(ItemHandle, text, async);
    }
}