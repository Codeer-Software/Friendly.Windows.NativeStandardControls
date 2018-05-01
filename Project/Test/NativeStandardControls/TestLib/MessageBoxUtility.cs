using Codeer.Friendly;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using System;

namespace NativeStandardControls.TestLib
{
    /// <summary>
    /// メッセージボックスユーティリティー
    /// </summary>
    static class MessageBoxUtility
    {
        /// <summary>
        /// すべて閉じる
        /// </summary>
        /// <param name="testDlg">ダイアログ</param>
        /// <param name="async">きっかけとなった非同期処理</param>
        /// <returns>閉じたダイアログの個数</returns>
        internal static int CloseAll(WindowControl testDlg, Async async)
        {
            int i = 0;
            while (!async.IsCompleted)
            {
                foreach (var e in WindowControl.GetTopLevelWindows(testDlg.App))
                {
                    if (e.Handle != testDlg.Handle)
                    {
                        i++;
                        e.Close();
                    }
                }
            }
            return i;
        }

        internal static void CloseAll(WindowControl testDlg)
        {
            while (testDlg.IsWindow())
            {
                WindowControl next = WindowControl.FromZTop(testDlg.App);
                if (next.Handle == testDlg.Handle || next.Handle == testDlg.ParentWindow.Handle)
                {
                    continue;
                }
                new NativeMessageBox(next).EmulateButtonClick("OK");
            }
        }
    }
}
