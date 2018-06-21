using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace Codeer.Friendly.Windows.NativeStandardControls.Inside
{
    /// <summary>
    /// ネイティブメソッド
    /// </summary>
    static class NativeMethods
    {
        /// <summary>
        /// フォーカスウィンドウを取得
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetFocus();

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref TBBUTTON lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, StringBuilder lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref int lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, ref int wParam, ref int lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [In, Out]int[] lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref SYSTEMTIME lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, [In, Out]SYSTEMTIME[] lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref TCITEM_CORE lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref LVITEM_CORE lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref LVCOLUMN_CORE lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref TVITEMEX_CORE lParam);
        
        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref NMHDR lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref NMDATETIMECHANGE lParam);
        
        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref NMIPADDRESS lParam);
       
        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref NMTRBTHUMBPOSCHANGING lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref NMUPDOWN lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref NMSELCHANGE lParam);

        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref RECT lParam);
        
        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref NMTREEVIEW lParam);
       
        /// <summary>
        /// メッセージを送信します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="msg">メッセージ。</param>
        /// <param name="wParam">wParam。</param>
        /// <param name="lParam">lParam。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, ref TVHITTESTINFO lParam);

        /// <summary>
        /// スクリーン座標からクライアント座標に変換
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="lpPoint">ポイント</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool ScreenToClient(IntPtr hWnd, ref POINT lpPoint);
        
        /// <summary>
        /// ウィンドウが表示状態であるか。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowVisible(IntPtr hWnd);

        /// <summary>
        /// ウィンドウが有効状態であるか。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>ウィンドウが有効状態であるか。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowEnabled(IntPtr hWnd);
        
        /// <summary>
        /// ウィンドウテキストの設定。
        /// </summary>
        /// <param name="hwnd">ウィンドウハンドル。</param>
        /// <param name="lpString">設定文字列。</param>
        /// <returns>成否。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetWindowText(IntPtr hwnd, String lpString);

        /// <summary>
        /// Window文字列取得。
        /// </summary>
        /// <param name="hWnd">ハンドル。</param>
        /// <param name="lpString">文字列格納バッファ。</param>
        /// <param name="nMaxCount">最大文字列。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// ウィンドウテキストの長さを取得する。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>ウィンドウテキストの長さ。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetWindowTextLength(IntPtr hWnd);

        /// <summary>
        /// フォーカスの設定。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>前のフォーカスウィンドウハンドル。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SetFocus(IntPtr hWnd);

        /// <summary>
        /// スクロール位置を取得します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="nBar">バーの種類。</param>
        /// <returns>位置。</returns>
        [DllImport("user32.dll")]
        internal static extern int GetScrollPos(IntPtr hWnd, int nBar);

        /// <summary>
        /// スクロール位置を設定します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="nBar">スクロールバーの種類。</param>
        /// <param name="nPos">スクロール位置。</param>
        /// <param name="bRedraw">再描画を実施するか。</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        /// <summary>
        /// スクロールバーの種類を設定します。
        /// </summary>
        /// <param name="hWnd">ウインドウハンドル。</param>
        /// <param name="nBar">スクロールバーの種類。</param>
        /// <param name="lpMinPos">下限。</param>
        /// <param name="lpMaxPos">上限。</param>
        /// <returns>成否。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetScrollRange(IntPtr hWnd, int nBar, ref int lpMinPos, ref int lpMaxPos);

        /// <summary>
        /// クラス名称を取得。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="lpClassName">クラス名称格納バッファ。</param>
        /// <param name="nMaxCount">最大文字数。</param>
        /// <returns>文字サイズ。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        /// <summary>
        /// ダイアログIDの取得。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>ダイアログID。</returns>
        [DllImport("user32.dll")]
        internal static extern int GetDlgCtrlID(IntPtr hWnd);

        /// <summary>
        /// 親ウィンドウを取得する。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>親ウィンドウ。</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr GetParent(IntPtr hWnd);

        /// <summary>
        /// ユニコードウィンドウであるか。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <returns>ユニコードウィンドウであるか。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsWindowUnicode(IntPtr hWnd);

        /// <summary>
        /// キーボード状態取得。
        /// </summary>
        /// <param name="keys">キーボード状態。</param>
        /// <returns>成否。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetKeyboardState(byte[] keys);

        /// <summary>
        /// キーボード状態設定。
        /// </summary>
        /// <param name="keys">キーボード状態。</param>
        /// <returns>成否。</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetKeyboardState(byte[] keys);

        /// <summary>
        /// 指定されたウィンドウに関する情報を取得します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="nIndex">操作種別。</param>
        /// <returns>結果。</returns>
        internal static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
            {
                return GetWindowLongPtr64(hWnd, nIndex);
            }
            else
            {
                return new IntPtr(GetWindowLongPtr32(hWnd, nIndex));
            }
        }

        /// <summary>
        /// 指定されたウィンドウに関する情報を取得します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="nIndex">操作種別。</param>
        /// <returns>結果</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        [SuppressMessage("Microsoft.Interoperability", "CA1400:PInvokeEntryPointsShouldExist")]
        static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

        /// <summary>
        /// 指定されたウィンドウに関する情報を取得します。
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル。</param>
        /// <param name="nIndex">操作種別。</param>
        /// <returns>結果</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        static extern int GetWindowLongPtr32(IntPtr hWnd, int nIndex);

        /// <summary>
        /// GetMessgeへのインストール
        /// </summary>
        internal const int WH_GETMESSAGE = 3;

        /// <summary>
        /// WndProcへのインストール
        /// </summary>
        internal const int WH_CALLWNDPROC = 4;

        /// <summary>
        /// フックプロック
        /// </summary>
        /// <param name="code">コード</param>
        /// <param name="wParam">wParam</param>
        /// <param name="lParam">lParam</param>
        /// <returns>結果</returns>
        internal delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// ウィンドウフックの設定
        /// </summary>
        /// <param name="hookType">フック種別</param>
        /// <param name="lpfn">コールバック</param>
        /// <param name="hMod">モジュールハンドル</param>
        /// <param name="dwThreadId">スレッドID</param>
        /// <returns>フックハンドル</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr SetWindowsHookEx(int hookType, HookProc lpfn, IntPtr hMod, int dwThreadId);

        /// <summary>
        /// アンフック
        /// </summary>
        /// <param name="hookHandle">フックハンドル</param>
        /// <returns>成否</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool UnhookWindowsHookEx(IntPtr hookHandle);

        /// <summary>
        /// 次のフック呼び出し
        /// </summary>
        /// <param name="hookHandle">フックハンドル</param>
        /// <param name="nCode">コード</param>
        /// <param name="wParam">wParam</param>
        /// <param name="lParam">lParam</param>
        /// <returns>結果</returns>
        [DllImport("user32.dll")]
        internal static extern IntPtr CallNextHookEx(IntPtr hookHandle, int nCode, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// WndProcメッセージ情報
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct CWPSTRUCT
        {
            public IntPtr lparam;
            public IntPtr wparam;
            public int message;
            public IntPtr hwnd;
        }

        /// <summary>
        /// メッセージ情報
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct MSG
        {
            public IntPtr hwnd;
            public int message;
            public IntPtr wparam;
            public IntPtr lparam;
            public int time;
            public POINT pt;
        }

        /// <summary>
        /// 指定のウィンドウハンドルの所属するスレッドとプロセスの取得
        /// </summary>
        /// <param name="hWnd">ウィンドウハンドル</param>
        /// <param name="lpdwProcessId">プロセスID</param>
        /// <returns>スレッドID</returns>
        [DllImport("user32.dll")]
        internal static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        [Flags]
        internal enum MIIM
        {
            BITMAP = 0x00000080,
            CHECKMARKS = 0x00000008,
            DATA = 0x00000020,
            FTYPE = 0x00000100,
            ID = 0x00000002,
            STATE = 0x00000001,
            STRING = 0x00000040,
            SUBMENU = 0x00000004,
            TYPE = 0x00000010
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MENUITEMINFO
        {
            public Int32 cbSize = Marshal.SizeOf(typeof(MENUITEMINFO));
            public MIIM fMask;
            public UInt32 fType;
            public UInt32 fState;
            public UInt32 wID;
            public IntPtr hSubMenu;
            public IntPtr hbmpChecked;
            public IntPtr hbmpUnchecked;
            public IntPtr dwItemData;
            public string dwTypeData = null;
            public UInt32 cch; // length of dwTypeData
            public IntPtr hbmpItem;

            public MENUITEMINFO() { }
            public MENUITEMINFO(MIIM pfMask)
            {
                fMask = pfMask;
            }
        }

        [DllImport("user32.dll")]
        internal static extern int GetMenuItemCount(IntPtr hMenu);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern int GetMenuString(IntPtr hMenu, int uIDItem, [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount, uint uFlag);

        [DllImport("user32.dll")]
        internal static extern bool GetMenuItemRect(IntPtr hWnd, IntPtr hMenu, int uItem, out RECT lprcItem);
        internal const int MF_BYPOSITION = 0x00000400;
        internal const int MN_GETHMENU = 0x01E1;
        internal const int MIIM_STATE = 0x00000001;
        internal const int MIIM_ID = 0x00000002;
        internal const int MF_ENABLED = 0x00000000;
        internal const int MF_DISABLED = 0x00000002;

        [DllImport("user32.dll")]
        internal static extern int GetMenuItemID(IntPtr hMenu, int nPos);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern bool GetMenuItemInfo(IntPtr hMenu, UInt32 uItem, bool fByPosition, [In, Out] MENUITEMINFO lpmii);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetMenu(IntPtr hwnd);

        [DllImport("kernel32.dll")]
        internal static extern uint GetLastError();
    }
}
