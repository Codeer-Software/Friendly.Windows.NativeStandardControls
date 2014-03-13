using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly;
using System.Collections.Generic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.NativeStandardControls;

namespace NativeStandardControls.TestLib
{
    /// <summary>
    /// イベントチェック。
    /// </summary>
    static class EventChecker
    {
        /// <summary>
        /// コード情報クリア。
        /// </summary>
        /// <param name="hWnd">CCommandAndNotifyCheckDlgのハンドル。</param>
        [DllImport("NativeControls.exe")]
        static extern void ClearCodeInfo(IntPtr hWnd);

        /// <summary>
        /// コード情報取得。
        /// </summary>
        /// <param name="hWnd">CCommandAndNotifyCheckDlgのハンドル。</param>
        /// <param name="arrayCount">配列の確保した数。</param>
        /// <param name="codeInfo">情報格納バッファ。</param>
        /// <returns>データの数。</returns>
        [DllImport("NativeControls.exe")]
        static extern int GetCodeInfo(IntPtr hWnd, int arrayCount, [In, Out]CodeInfo[] codeInfo);

        /// <summary>
        /// 非同期メッセージ表示のクリア（終了）
        /// </summary>
        /// <param name="hWnd">CCommandAndNotifyCheckDlgのハンドル。</param>
        [DllImport("NativeControls.exe")]
        static extern void ClearAsyncMsgBox(IntPtr hWnd);

        /// <summary>
        /// Notify通知の詳細な情報を取得する。
        /// </summary>
        /// <param name="hDlg">ウィンドウハンドル。</param>
        /// <param name="arrayCount">配列要素確保数。</param>
        /// <param name="pArray">配列。</param>
        /// <returns>実際の配列数。</returns>
        [DllImport("NativeControls.exe")]
        static extern int GetNMDATETIMECHANGE(IntPtr hDlg, int arrayCount, [In, Out]NMDATETIMECHANGE[] pArray);

        /// <summary>
        /// Notify通知の詳細な情報を取得する。
        /// </summary>
        /// <param name="hDlg">ウィンドウハンドル。</param>
        /// <param name="arrayCount">配列要素確保数。</param>
        /// <param name="pArray">配列。</param>
        /// <returns>実際の配列数。</returns>
        [DllImport("NativeControls.exe")]
        static extern int GetNMIPADDRESS(IntPtr hDlg, int arrayCount, [In, Out]NMIPADDRESS[] pArray);

        /// <summary>
        /// Notify通知の詳細な情報を取得する。
        /// </summary>
        /// <param name="hDlg">ウィンドウハンドル。</param>
        /// <param name="arrayCount">配列要素確保数。</param>
        /// <param name="pArray">配列。</param>
        /// <returns>実際の配列数。</returns>
        [DllImport("NativeControls.exe")]
        static extern int GetNMSELCHANGE(IntPtr hDlg, int arrayCount, [In, Out]NMSELCHANGE[] pArray);

        /// <summary>
        /// Notify通知の詳細な情報を取得する。
        /// </summary>
        /// <param name="hDlg">ウィンドウハンドル。</param>
        /// <param name="arrayCount">配列要素確保数。</param>
        /// <param name="pArray">配列。</param>
        /// <returns>実際の配列数。</returns>
        [DllImport("NativeControls.exe")]
        static extern int GetNMTRBTHUMBPOSCHANGING(IntPtr hDlg, int arrayCount, [In, Out]NMTRBTHUMBPOSCHANGING[] pArray);

        /// <summary>
        /// Notify通知の詳細な情報を取得する。
        /// </summary>
        /// <param name="hDlg">ウィンドウハンドル。</param>
        /// <param name="arrayCount">配列要素確保数。</param>
        /// <param name="pArray">配列。</param>
        /// <returns>実際の配列数。</returns>
        [DllImport("NativeControls.exe")]
        static extern int GetNMUPDOWN(IntPtr hDlg, int arrayCount, [In, Out]NMUPDOWN[] pArray);

        /// <summary>
        /// Notify通知の詳細な情報を取得する。
        /// </summary>
        /// <param name="hDlg">ウィンドウハンドル。</param>
        /// <param name="arrayCount">配列要素確保数。</param>
        /// <param name="pArray">配列。</param>
        /// <returns>実際の配列数。</returns>
        [DllImport("NativeControls.exe")]
        static extern int GetNMTREEVIEW(IntPtr hDlg, int arrayCount, [In, Out]NMTREEVIEW[] pArray);
        
        /// <summary>
        /// 初期化。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        internal static void Initialize(WindowsAppFriend app)
        {
            object dummy;
            if (!app.TryGetAppControlInfo(typeof(EventChecker).Name, out dummy))
            {
                WindowsAppExpander.LoadAssemblyFromFile(app, typeof(EventChecker).Assembly.Location);
                app.AddAppControlInfo(typeof(EventChecker).Name, true);
            }
        }

        /// <summary>
        /// 通知内容が期待値と一致するか。
        /// </summary>
        /// <param name="checkDialog">チェックダイアログ。</param>
        /// <param name="expectation">期待値。</param>
        /// <returns>一致するか。</returns>
        internal static bool CheckNotifyDetail(WindowControl checkDialog, MethodInvoker function, NMDATETIMECHANGE[] expectation)
        {
            NMDATETIMECHANGE[] value = GetNotifyInfo<NMDATETIMECHANGE>(checkDialog, function);
            if (expectation.Length != value.Length)
            {
                return false;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (expectation[i].dwFlags != value[i].dwFlags)
                {
                    return false;
                }
                if (!IsSameSystemTime(expectation[i].st, value[i].st))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 通知内容が期待値と一致するか。
        /// </summary>
        /// <param name="checkDialog">チェックダイアログ。</param>
        /// <param name="expectation">期待値。</param>
        /// <returns>一致するか。</returns>
        internal static bool CheckNotifyDetail(WindowControl checkDialog, MethodInvoker function, NMIPADDRESS[] expectation)
        {
            NMIPADDRESS[] value = GetNotifyInfo<NMIPADDRESS>(checkDialog, function);
            if (expectation.Length != value.Length)
            {
                return false;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (expectation[i].iField != value[i].iField)
                {
                    return false;
                }
                if (expectation[i].iValue != value[i].iValue)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 通知内容が期待値と一致するか。
        /// </summary>
        /// <param name="checkDialog">チェックダイアログ。</param>
        /// <param name="expectation">期待値。</param>
        /// <returns>一致するか。</returns>
        internal static bool CheckNotifyDetail(WindowControl checkDialog, MethodInvoker function, NMTRBTHUMBPOSCHANGING[] expectation)
        {
            NMTRBTHUMBPOSCHANGING[] value = GetNotifyInfo<NMTRBTHUMBPOSCHANGING>(checkDialog, function);
            if (expectation.Length != value.Length)
            {
                return false;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (expectation[i].nReason != value[i].nReason)
                {
                    return false;
                }
                if (expectation[i].dwPos != value[i].dwPos)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 通知内容が期待値と一致するか。
        /// </summary>
        /// <param name="checkDialog">チェックダイアログ。</param>
        /// <param name="expectation">期待値。</param>
        /// <returns>一致するか。</returns>
        internal static bool CheckNotifyDetail(WindowControl checkDialog, MethodInvoker function, NMSELCHANGE[] expectation)
        {
            NMSELCHANGE[] value = GetNotifyInfo<NMSELCHANGE>(checkDialog, function);
            if (expectation.Length != value.Length)
            {
                return false;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (!IsSameSystemTime(expectation[i].stSelStart, value[i].stSelStart))
                {
                    return false;
                }
                if (!IsSameSystemTime(expectation[i].stSelEnd, value[i].stSelEnd))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 通知内容が期待値と一致するか。
        /// </summary>
        /// <param name="checkDialog">チェックダイアログ。</param>
        /// <param name="expectation">期待値。</param>
        /// <returns>一致するか。</returns>
        internal static bool CheckNotifyDetail(WindowControl checkDialog, MethodInvoker function, NMUPDOWN[] expectation)
        {
            NMUPDOWN[] value = GetNotifyInfo<NMUPDOWN>(checkDialog, function);
            if (expectation.Length != value.Length)
            {
                return false;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (expectation[i].iDelta != value[i].iDelta)
                {
                    return false;
                }
                if (expectation[i].iPos != value[i].iPos)
                {
                    return false;
                }
            }
            return true;
        }
        
        /// <summary>
        /// 通知内容が期待値と一致するか。
        /// </summary>
        /// <param name="checkDialog">チェックダイアログ。</param>
        /// <param name="expectation">期待値。</param>
        /// <returns>一致するか。</returns>
        internal static bool CheckNotifyDetail(WindowControl checkDialog, MethodInvoker function, NMTREEVIEW[] expectation)
        {
            NMTREEVIEW[] value = GetNotifyInfo<NMTREEVIEW>(checkDialog, function);
            if (expectation.Length != value.Length)
            {
                return false;
            }
            for (int i = 0; i < value.Length; i++)
            {
                if (expectation[i].action != value[i].action)
                {
                    return false;
                }
                if (expectation[i].itemNew.cChildren != value[i].itemNew.cChildren)
                {
                    return false;
                }
                if (expectation[i].itemNew.cchTextMax != value[i].itemNew.cchTextMax)
                {
                    return false;
                }
                if (expectation[i].itemNew.hItem != value[i].itemNew.hItem)
                {
                    return false;
                }
                if (expectation[i].itemNew.iImage != value[i].itemNew.iImage)
                {
                    return false;
                }
                if (expectation[i].itemNew.iSelectedImage != value[i].itemNew.iSelectedImage)
                {
                    return false;
                }
                if (expectation[i].itemNew.lParam != value[i].itemNew.lParam)
                {
                    return false;
                }
                if (expectation[i].itemNew.mask != value[i].itemNew.mask)
                {
                    return false;
                }
                if (expectation[i].itemNew.pszText != value[i].itemNew.pszText)
                {
                    return false;
                }
                if (expectation[i].itemNew.state != value[i].itemNew.state)
                {
                    return false;
                }
                if (expectation[i].itemNew.stateMask != value[i].itemNew.stateMask)
                {
                    return false;
                }
            }
            return true;
        }
        
        /// <summary>
        /// 通知データを取得
        /// </summary>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="checkDialog">ダイアログ</param>
        /// <param name="function">実行する処理</param>
        /// <returns>通知データ</returns>
        private static T[] GetNotifyInfo<T>(WindowControl checkDialog, MethodInvoker function)
        {
            //バッファクリア
            checkDialog.App[typeof(EventChecker), "ClearCodeInfo"](checkDialog.Handle);

            //テスト処理実行
            function();

            //データ取得
            string getFunction = "Get" + typeof(T).Name;
            AppVar inTarget = checkDialog.App.Dim(new T[256]);
            int count = (int)checkDialog.App[typeof(EventChecker), getFunction](checkDialog.Handle, 256, inTarget).Core;
            if (count == -1)
            {
                //処理中に256以上の通知が溜まるテストはサポート対象外。
                throw new NotSupportedException();
            }
            T[] value = (T[])inTarget.Core;
            //バッファクリア
            checkDialog.App[typeof(EventChecker), "ClearCodeInfo"](checkDialog.Handle);

            T[] ret = new T[count];
            for (int i = 0; i < count; i++)
            {
                ret[i] = value[i];
            }
            return ret;
        }

        /// <summary>
        /// システムタイムが一致するか。
        /// </summary>
        /// <param name="s1">データ1。</param>
        /// <param name="s2">データ2。</param>
        /// <returns>一致するか。</returns>
        private static bool IsSameSystemTime(SYSTEMTIME s1, SYSTEMTIME s2)
        {
            return (s1.Year == s2.Year && s1.Month == s2.Month && s1.Day == s2.Day && s1.Hour == s2.Hour
                && s1.Minute == s2.Minute && s1.Second == s2.Second && s1.Milliseconds == s2.Milliseconds);
        }

        /// <summary>
        /// 処理中に発生したイベントが期待値と一致するかチェック。
        /// </summary>
        /// <param name="checkDialog">チェックダイアログ。</param>
        /// <param name="function">テスト処理。</param>
        /// <param name="expectation">発生イベント期待値。</param>
        /// <returns></returns>
        internal static bool IsSameTestEvent(WindowControl checkDialog, MethodInvoker function, params CodeInfo[] expectation)
        {
            return IsSame(GetTestEvent(checkDialog, function), expectation);
        }

        /// <summary>
        /// 処理中に発生したイベントが期待値と一致するかチェック。
        /// </summary>
        /// <param name="checkDialog">チェックダイアログ。</param>
        /// <param name="function">テスト処理。</param>
        /// <param name="expectation">発生イベント期待値。</param>
        /// <returns></returns>
        internal static bool IsSameTestEvent(WindowControl checkDialog, MethodInvoker function, CodeInfo[] ignore, params CodeInfo[] expectation)
        {
            CodeInfo[] ret = GetTestEvent(checkDialog, function);
            List<CodeInfo> listIgnore = new List<CodeInfo>(ignore);
            List<CodeInfo> listFilter = new List<CodeInfo>();
            for (int i = 0; i < ret.Length; i++)
            {
                if (listIgnore.IndexOf(ret[i]) == -1)
                {
                    listFilter.Add(ret[i]);
                }
            }
            return IsSame(listFilter.ToArray(), expectation);
        }

        /// <summary>
        /// 処理中に発生したイベントを取得
        /// </summary>
        /// <param name="checkDialog">チェックダイアログ。</param>
        /// <param name="function">テスト処理。</param>
        /// <returns>イベント</returns>
        internal static CodeInfo[] GetTestEvent(WindowControl checkDialog, MethodInvoker function)
        {
            //バッファクリア
            checkDialog.App[typeof(EventChecker), "ClearCodeInfo"](checkDialog.Handle);
            
            //テスト処理実行
            function();

            //溜まった通知を取得
            AppVar inTarget = checkDialog.App.Dim(new CodeInfo[1024]);
            object ret = checkDialog.App[typeof(EventChecker), "GetCodeInfo"](checkDialog.Handle, 1024, inTarget).Core;
            int len = (int)ret;
            if (1024 < len)
            {
                //処理中に1024以上の通知が溜まるテストはサポート対象外。
                throw new NotSupportedException();
            }
            
            //バッファクリア。
            checkDialog.App[typeof(EventChecker), "ClearCodeInfo"](checkDialog.Handle);

            //データのある分だけにトリミング。
            CodeInfo[] resultAll = (CodeInfo[])inTarget.Core;
            CodeInfo[] result = new CodeInfo[len];
            for (int i = 0; i < len; i++)
            {
                result[i] = resultAll[i];
            }
            return result;
        }

        /// <summary>
        /// 結果と期待値の完全一致判定。
        /// </summary>
        /// <param name="result">結果。</param>
        /// <param name="expectation">期待値。</param>
        /// <returns>判定結果。</returns>
        static bool IsSame(CodeInfo[] result, params CodeInfo[] expectation)
        {
            if (result.Length != expectation.Length)
            {
                return false;
            }
            for (int i = 0; i < result.Length; i++)
            {
                if (!result[i].Equals(expectation[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
