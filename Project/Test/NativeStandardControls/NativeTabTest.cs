using System;
using System.Threading;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using NativeStandardControls.TestLib;
using System.Text;

namespace NativeStandardControls
{
    /// <summary>
    /// NativeTabテストクラス。
    /// </summary>
    [TestClass]
    public class NativeTabTest
    {
        WindowsAppFriend app;
        WindowControl testDlg;
        const int TCN_FIRST = -550;
        const int TCN_SELCHANGE = TCN_FIRST - 1;
        const int TCN_SELCHANGING = TCN_FIRST - 2;

        /// <summary>
        /// 初期化。
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            app = new WindowsAppFriend(Process.Start(TargetPath.NativeControls));
            EventChecker.Initialize(app);
            WindowControl main = WindowControl.FromZTop(app);
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1023));
            buttonTest.EmulateClick(new Async());
            testDlg = main.WaitForNextModal();
        }

        /// <summary>
        /// 終了処理。
        /// </summary>
        [TestCleanup]
        public void TearDown()
        {
            if (app != null)
            {
                new NativeButton(testDlg.IdentifyFromWindowText("OK")).EmulateClick();
                MessageBoxUtility.CloseAll(testDlg);
                app.Dispose();
                Process process = Process.GetProcessById(app.ProcessId);
                process.CloseMainWindow();
                app = null;
            }
        }

        /// <summary>
        /// コンストラクタテスト。
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            //WindowControlから作成。
            int itemCount = (NativeMethods.IsWindowUnicode(testDlg.Handle)) ? 4 : 3;
            {
                NativeTab tab = new NativeTab(testDlg.IdentifyFromDialogId(1022));
                Assert.AreEqual(itemCount, tab.ItemCount);
            }
            //ハンドルから作成。
            {
                NativeTab tab = new NativeTab(app, testDlg.IdentifyFromDialogId(1022).Handle);
                Assert.AreEqual(itemCount, tab.ItemCount);
            }
        }

        /// <summary>
        /// ItemCountのテスト。
        /// </summary>
        [TestMethod]
        public void TestItemCount()
        {
            NativeTab tab = new NativeTab(testDlg.IdentifyFromDialogId(1022));
            Assert.AreEqual(NativeMethods.IsWindowUnicode(testDlg.Handle) ? 4 : 3, tab.ItemCount);
        }

        /// <summary>
        /// GetItemTextのテスト。
        /// </summary>
        [TestMethod]
        public void TestItemText()
        {
            NativeTab tab = new NativeTab(testDlg.IdentifyFromDialogId(1022));
            Assert.AreEqual("a", tab.GetItemText(0));
            Assert.AreEqual("あ", tab.GetItemText(1));
            if (NativeMethods.IsWindowUnicode(testDlg.Handle))
            {
                Assert.AreEqual("𩸽", tab.GetItemText(3));
            }
        }

        /// <summary>
        /// 編集テスト
        /// </summary>
        [TestMethod]
        public void TestGetItemTextOver256()
        {
            NativeTab tab = new NativeTab(testDlg.IdentifyFromDialogId(1022));
            StringBuilder overText = new StringBuilder();
            for (int i = 0; i < 259; i++)
            {
                overText.Append("a");
            }
            Assert.AreEqual(overText.ToString(), tab.GetItemText(2));
        }

        /// <summary>
        /// GetItemDataのテスト。
        /// </summary>
        [TestMethod]
        public void TestGetItemData()
        {
            NativeTab tab = new NativeTab(testDlg.IdentifyFromDialogId(1022));
            Assert.AreEqual(new IntPtr(10), tab.GetItemData(0));
        }

        /// <summary>
        /// GetItemのテスト。
        /// </summary>
        [TestMethod]
        public void TestGetItem()
        {
            NativeTab tab = new NativeTab(testDlg.IdentifyFromDialogId(1022));

            TCITEM item = new TCITEM();
            item.mask = (TCIF.TEXT | TCIF.IMAGE | TCIF.PARAM | TCIF.STATE);
            item.dwStateMask = (TCIS.BUTTONPRESSED | TCIS.HIGHLIGHTED);

            tab.GetItem(0, item);
            Assert.AreEqual("a", item.pszText);
            Assert.AreEqual(0, item.iImage);
            Assert.AreEqual(new IntPtr(10), item.lParam);
            Assert.AreEqual(TCIS.BUTTONPRESSED, item.dwState);
            tab.GetItem(1, item);
            Assert.AreEqual("あ", item.pszText);
            Assert.AreEqual(1, item.iImage);
            Assert.AreEqual(new IntPtr(11), item.lParam);
            Assert.AreEqual((TCIS)0, item.dwState);
        }

        /// <summary>
        /// EmulateSelectItemとSelectedItemIndexの値テスト。
        /// </summary>
        [TestMethod]
        public void TestSelectedItemIndexValue()
        {
            NativeTab tab = new NativeTab(testDlg.IdentifyFromDialogId(1022));
            tab.EmulateSelectItem(2);
            Assert.AreEqual(2, tab.SelectedItemIndex);

            //非同期でも同様の効果があることを確認
            Async a = new Async();
            tab.EmulateSelectItem(1, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.AreEqual(1, tab.SelectedItemIndex);
        }

        /// <summary>
        /// EmulateSelectItemのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestSelectedItemIndexEvent()
        {
            NativeTab tab = new NativeTab(testDlg.IdentifyFromDialogId(1022));
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { tab.EmulateSelectItem(1); },
                new CodeInfo(1022, NativeMethods.WM_NOTIFY, TCN_SELCHANGING),
                new CodeInfo(1022, NativeMethods.WM_NOTIFY, TCN_SELCHANGE)));

            //同じインデックスの場合はイベントは発生しない。
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { tab.EmulateSelectItem(1); }));
        }

        /// <summary>
        /// EmulateSelectItemの非同期実行イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestSelectedItemIndexEventAsync()
        {
            NativeTab tab = new NativeTab(testDlg.IdentifyFromDialogId(1023));
            Async async = new Async();
            tab.EmulateSelectItem(1, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }
    }
}
