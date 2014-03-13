using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using NativeStandardControls.TestLib;

namespace NativeStandardControls
{
    /// <summary>
    /// NativeListBoxテストクラス。
    /// </summary>
    [TestClass]
    public class NativeListBoxTest
    {
        const int LBN_SELCHANGE = 1;
        const int LBN_DBLCLK = 2;

        WindowsAppFriend app;
        WindowControl testDlg;

        /// <summary>
        /// 初期化。
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            app = new WindowsAppFriend(Process.Start(TargetPath.NativeControls));
            EventChecker.Initialize(app);
            WindowControl main = WindowControl.FromZTop(app);
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1036));
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
        /// コンストラクタのテスト。
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            //WindowControlから作成。
            {
                NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1037));
                Assert.AreEqual((NativeMethods.IsWindowUnicode(testDlg.Handle)) ? 101 : 100, listBox.Count);
            }
            //ハンドルから作成。
            {
                NativeListBox listBox = new NativeListBox(app, testDlg.IdentifyFromDialogId(1037).Handle);
                Assert.AreEqual((NativeMethods.IsWindowUnicode(testDlg.Handle)) ? 101 : 100, listBox.Count);
            }
        }

        /// <summary>
        /// Countのテスト。
        /// </summary>
        [TestMethod]
        public void TestCount()
        {
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1037));
            Assert.AreEqual((NativeMethods.IsWindowUnicode(testDlg.Handle)) ? 101 : 100, listBox.Count);
        }

        /// <summary>
        /// EmulateChangeCurrentSelectedIndexとCurrentSelectedIndexのテスト。
        /// </summary>
        [TestMethod]
        public void TestCurrentSelectedIndexValue()
        {
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1037));
            listBox.EmulateChangeCurrentSelectedIndex(1);
            Assert.AreEqual(1, listBox.CurrentSelectedIndex);

            //非同期でも正常に動作することを確認。
            Async a = new Async();
            listBox.EmulateChangeCurrentSelectedIndex(2, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.AreEqual(2, listBox.CurrentSelectedIndex);
        }

        /// <summary>
        /// EmulateChangeSelectとSelectedIndicesのテスト。
        /// </summary>
        [TestMethod]
        public void TestSelectedIndices()
        {
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1035));
            listBox.EmulateChangeSelect(0, true);
            listBox.EmulateChangeSelect(1, false);
            listBox.EmulateChangeSelect(2, true);
            AssertEx.AreEqual(new int[] { 0, 2 }, listBox.SelectedIndices);

            //非同期でも正常に動作することを確認。
            Async a = new Async();
            listBox.EmulateChangeSelect(2, false, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            AssertEx.AreEqual(new int[] { 0 }, listBox.SelectedIndices);
        }

        /// <summary>
        /// SetTopIndexとTopIndexのテスト。
        /// </summary>
        [TestMethod]
        public void TestTopIndex()
        {
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1037));
            listBox.SetTopIndex(30);
            Assert.AreEqual(30, listBox.TopIndex);

            //先頭になれない場合は表示領域に入ること。
            if (NativeMethods.IsWindowUnicode(testDlg.Handle))
            {
                listBox.SetTopIndex(100);
                Assert.AreEqual(93, listBox.TopIndex);
            }
            else
            {
                listBox.SetTopIndex(99);
                Assert.AreEqual(92, listBox.TopIndex);
            }
        }

        /// <summary>
        /// GetItemDataのテスト。
        /// </summary>
        [TestMethod]
        public void TestItemData()
        {
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1037));
            Assert.AreEqual(new IntPtr(5), listBox.GetItemData(5));
        }

        /// <summary>
        /// GetItemTextのテスト。
        /// </summary>
        [TestMethod]
        public void TestItemText()
        {
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1037));
            Assert.AreEqual("10", listBox.GetItemText(10));
        }

        /// <summary>
        /// GetItemRectのテスト。
        /// </summary>
        [TestMethod]
        public void TestItemRect()
        {
            if (!OSUtility.Is7or8())
            {
                //矩形は環境によって変わるので7のみ。しかし、7なら常に同じ矩形とも限らない。
                //このテストデータが使えるOSの設定は限られる。
                return;
            }
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1037));
            listBox.SetTopIndex(0);
            Assert.AreEqual(new Rectangle(0, 39, 63, 14), listBox.GetItemRect(3));
        }

        /// <summary>
        /// EmulateChangeCurrentSelectedIndexのイベント発生のテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeCurrentSelectedIndexEvent()
        {
            //LBN_SELCHANGEが発生していることを確認。
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1037));
            listBox.EmulateChangeCurrentSelectedIndex(0);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { listBox.EmulateChangeCurrentSelectedIndex(1); },
                new CodeInfo(1037, NativeMethods.WM_COMMAND, LBN_SELCHANGE)));
        }

        /// <summary>
        /// EmulateChangeSelectのイベント発生のテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeSelectEvent()
        {
            //LBN_SELCHANGEが発生していることを確認。
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1035));
            listBox.EmulateChangeSelect(0, false);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { listBox.EmulateChangeSelect(0, true); },
                new CodeInfo(1035, NativeMethods.WM_COMMAND, LBN_SELCHANGE)));
        }

        /// <summary>
        /// EmulateChangeCurrentSelectedIndexの非同期実行イベント発生のテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeCurrentSelectedIndexEventAsync()
        {
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1038));
            Async async = new Async();
            listBox.EmulateChangeCurrentSelectedIndex(1, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateChangeSelectの非同期実行イベント発生のテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeSelectEventAsync()
        {
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1036));
            Async async = new Async();
            listBox.EmulateChangeSelect(1, true, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// FindItemのテスト。
        /// </summary>
        [TestMethod]
        public void TestFindItem()
        {
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1037));
            Assert.AreEqual(2, listBox.FindItem(0, "2"));
            Assert.AreEqual(2, listBox.FindItem(2, "2"));
            Assert.AreEqual(-1, listBox.FindItem(3, "2"));
        }

        /// <summary>
        /// ユニコードのテスト。
        /// </summary>
        [TestMethod]
        public void TestUnicode()
        {
            if (!NativeMethods.IsWindowUnicode(testDlg.Handle))
            {
                return;
            }
            NativeListBox listBox = new NativeListBox(testDlg.IdentifyFromDialogId(1037));
            Assert.AreEqual("𩸽", listBox.GetItemText(100));
            Assert.AreEqual(100, listBox.FindItem(0, "𩸽"));
        }
    }
}
