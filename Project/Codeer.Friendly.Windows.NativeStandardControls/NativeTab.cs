using System;
using System.Runtime.InteropServices;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type SysTabControl32.
    /// </summary>    
#else
    /// <summary>
    /// WindowClassがSysTabControl32のウィンドウに対応した操作を提供します。
    /// </summary>    
#endif
    [ControlDriver(WindowClassName = "SysTabControl32")]
    public class NativeTab : NativeWindow
    {
        internal const int TCN_FIRST = -550;
        internal const int TCN_SELCHANGE = TCN_FIRST - 1;
        internal const int TCN_SELCHANGING = TCN_FIRST - 2;
        internal const int TCM_FIRST = 0x1300;
        internal const int TCM_GETITEMA = (TCM_FIRST + 5);
        internal const int TCM_GETITEMW = (TCM_FIRST + 60);
        internal const int TCM_GETCURSEL = (TCM_FIRST + 11);
        internal const int TCM_SETCURSEL = (TCM_FIRST + 12);
        internal const int TCM_GETITEMCOUNT = (TCM_FIRST + 4);
        
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
        public NativeTab(WindowControl src)
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
        public NativeTab(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App);
        }
        
#if ENG
        /// <summary>
        /// Returns the number of tabs.
        /// </summary>
#else
        /// <summary>
        /// タブ数を取得します。
        /// </summary>
#endif
        public int ItemCount
        {
            get
            {
                return (int)SendMessage(TCM_GETITEMCOUNT, IntPtr.Zero, IntPtr.Zero);
            }
        }
        
#if ENG
        /// <summary>
        /// Returns the index of the selected tab.
        /// </summary>
#else
        /// <summary>
        /// 現在選択されているタブのインデックスです。
        /// </summary>
#endif
        public int SelectedItemIndex
        {
            get
            {
                return (int)App[GetType(), "GetSelectedItemIndexInTarget"](Handle).Core;
            }
        }
        
#if ENG
        /// <summary>
        /// Obtains the text for a certain tab item.
        /// </summary>
        /// <param name="tabIndex">Tab index.</param>
        /// <returns>Tab text.</returns>
#else
        /// <summary>
        /// 文字列を取得します。
        /// </summary>
        /// <param name="tabIndex">タブインデックス。</param>
        /// <returns>タブ文字列。</returns>
#endif
        public string GetItemText(int tabIndex)
        {
            TCITEM item = new TCITEM();
            item.mask = TCIF.TEXT;
            return GetItem(tabIndex, item) ? item.pszText : string.Empty;
        }
        
#if ENG
        /// <summary>
        /// Obtains tab item data.
        /// </summary>
        /// <param name="tabIndex">Tab index.</param>
        /// <returns>Item data.</returns>
#else
        /// <summary>
        /// アイテムデータを取得します。
        /// </summary>
        /// <param name="tabIndex">タブインデックス。</param>
        /// <returns>アイテムデータ。</returns>
#endif
        public IntPtr GetItemData(int tabIndex)
        {
            TCITEM item = new TCITEM();
            item.mask = TCIF.PARAM; 
            return GetItem(tabIndex, item) ? item.lParam : IntPtr.Zero;
        }
        
#if ENG
        /// <summary>
        /// Gets a certain tab item.
        /// </summary>
        /// <param name="tabIndex">Tab index.</param>
        /// <param name="item">Item storage buffer.</param>
        /// <returns>Success or failure.</returns>
#else
        /// <summary>
        /// アイテムを取得します。
        /// </summary>
        /// <param name="tabIndex">タブインデックス</param>
        /// <param name="item">アイテム格納バッファ</param>
        /// <returns>成否</returns>
#endif
        public bool GetItem(int tabIndex, TCITEM item)
        {
            AppVar inTarget = App.Dim(item);
            bool ret = (bool)App[GetType(), "GetItemInTarget"](Handle, tabIndex, inTarget).Core;
            TCITEM getData = (TCITEM)inTarget.Core;
            item._core = getData._core;
            item.pszText = getData.pszText;
            return ret;
        }
        
#if ENG
        /// <summary>
        /// Sets the selected tab.
        /// Produces TCN_SELCHANGING and TCN_SELCHANGE notifications if the selection changes.
        /// </summary>
        /// <param name="index">Index to select.</param>
#else
        /// <summary>
        /// タブを選択します。
        /// 選択位置の変更があった場合、TCN_SELCHANGING、TCN_SELCHANGEの通知が発生します。
        /// </summary>
        /// <param name="index">アイテムのインデックス。</param>
#endif
        public void EmulateSelectItem(int index)
        {
            App[GetType(), "EmulateSelectItemInTarget"](Handle, index);
        }
        
#if ENG
        /// <summary>
        /// Sets the selected tab.
        /// Produces TCN_SELCHANGING and TCN_SELCHANGE notifications if the selection changes.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="index">Index to select.</param>
        /// <param name="async">Asynchronous execution object.</param>
#else
        /// <summary>
        /// タブを選択します。
        /// 選択位置の変更があった場合、TCN_SELCHANGING、TCN_SELCHANGEの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="index">アイテムのインデックス。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateSelectItem(int index, Async async)
        {
            App[GetType(), "EmulateSelectItemInTarget", async](Handle, index);
        }

        /// <summary>
        /// アイテムを取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="tabIndex">タブインデックス。</param>
        /// <param name="item">アイテム。</param>
        /// <returns>成否。</returns>
        private static bool GetItemInTarget(IntPtr handle, int tabIndex, TCITEM item)
        {
            bool isUni = NativeMethods.IsWindowUnicode(handle);
            int TCM_GETITEM = isUni ? TCM_GETITEMW : TCM_GETITEMA;
            item._core.cchTextMax = 256;
            while (true)
            {
                //メモリを確保。
                if ((item.mask & TCIF.TEXT) != 0)
                {
                    item._core.pszText = Marshal.AllocCoTaskMem((item._core.cchTextMax + 1) * 8);
                }
                try
                {
                    //データ取得。
                    if (!NativeDataUtility.ToBool(NativeMethods.SendMessage(handle, TCM_GETITEM, new IntPtr(tabIndex), ref item._core)))
                    {
                        return false;
                    }

                    //文字列取得でなければ終了
                    if ((item.mask & TCIF.TEXT) == 0)
                    {
                        return true;
                    }

                    //文字列に変換
                    item._pszText = isUni ? Marshal.PtrToStringUni(item._core.pszText) :
                                            Marshal.PtrToStringAnsi(item._core.pszText);

                    //文字バッファが足りなければリトライ。
                    if (item.pszText.Length < item._core.cchTextMax - 1)
                    {
                        return true;
                    }
                    item._core.cchTextMax *= 2;
                }
                finally
                {
                    //メモリ解放。
                    if (item._core.pszText != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(item._core.pszText);
                    }
                }
            }
        }

        /// <summary>
        /// タブを選択します。
        /// 選択位置の変更があった場合、TCN_SELCHANGING、TCN_SELCHANGEの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="index">アイテムのインデックス。</param>
        private static void EmulateSelectItemInTarget(IntPtr handle, int index)
        {
            NativeMethods.SetFocus(handle);
            if (index == (int)NativeMethods.SendMessage(handle, TCM_GETCURSEL, IntPtr.Zero, IntPtr.Zero))
            {
                return;
            }
            SendNotify(handle, TCN_SELCHANGING);
            NativeMethods.SendMessage(handle, TCM_SETCURSEL, new IntPtr(index), IntPtr.Zero);
            SendNotify(handle, TCN_SELCHANGE);
        }

        /// <summary>
        /// WM_NOTIFYを通知します。
        /// 対象アプリケーション内部で実行する必要があります。
        /// </summary>
        /// <param name="handle">ハンドル。</param>
        /// <param name="code">通知コード。</param>
        private static void SendNotify(IntPtr handle, int code)
        {
            NMHDR header = new NMHDR();
            EmulateUtility.InitNotify(handle, code, ref header);
            NativeMethods.SendMessage(NativeMethods.GetParent(handle), NativeCommonDefine.WM_NOTIFY, header.idFrom, ref header);
        }

        /// <summary>
        /// 現在選択されているタブのインデックスを取得。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <returns>現在選択されているタブのインデックス。</returns>
        internal static int GetSelectedItemIndexInTarget(IntPtr handle)
        {
            return (int)NativeMethods.SendMessage(handle, NativeTab.TCM_GETCURSEL, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
