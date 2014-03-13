using System;
using System.Text;
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
    /// NativeEditテストクラス。
    /// </summary>
    [TestClass]
    public class NativeEditTest
    {
        const int EN_SELCHANGE = 0x0702;
        const int EN_CHANGE = 0x0300;
        const int EN_UPDATE = 0x0400;
        const int EN_HSCROLL = 0x0601;
        const int EN_VSCROLL = 0x0602;
        const int EM_LINESCROLL = 0x00B6;

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
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1039));
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
                NativeEdit edit = new NativeEdit(testDlg.IdentifyFromDialogId(1020));
                edit.EmulateChangeText("a");
                Assert.AreEqual("a", edit.Text);
            }
            //ハンドルから作成。
            {
                NativeEdit edit = new NativeEdit(app, testDlg.IdentifyFromDialogId(1020).Handle);
                edit.EmulateChangeText("b");
                Assert.AreEqual("b", edit.Text);
            }
        }

        /// <summary>
        /// EmulateChangeTextとTextのテスト。
        /// </summary>
        [TestMethod]
        public void TestTextValue()
        {
            foreach (NativeEdit edit in new NativeEdit[]{
                new NativeEdit(testDlg.IdentifyFromDialogId(1020)),
                new NativeEdit(testDlg.IdentifyFromDialogId(1038))})
            {
                string text = "あいう" + Environment.NewLine + "abc";
                edit.EmulateChangeText(text);
                Assert.AreEqual(text, edit.Text);

                //非同期でも正常に動作することを確認。
                text = "かきく" + Environment.NewLine + "efg";
                Async a = new Async();
                edit.EmulateChangeText(text, a);
                while (!a.IsCompleted)
                {
                    Thread.Sleep(10);
                }
                Assert.AreEqual(text, edit.Text);
            }
        }

        /// <summary>
        /// LineCountのテスト。
        /// </summary>
        [TestMethod]
        public void TestLineCount()
        {
            //複数行テスト用の文字列。
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                b.Append(i.ToString() + Environment.NewLine);
            }
            string manyLine = b.ToString();

            foreach (NativeEdit edit in new NativeEdit[]{
                new NativeEdit(testDlg.IdentifyFromDialogId(1020)),
                new NativeEdit(testDlg.IdentifyFromDialogId(1038))})
            {
                edit.EmulateChangeText(manyLine);
                Assert.AreEqual(101, edit.LineCount);
            }
        }

        /// <summary>
        /// FirstVisibleLineのテスト。
        /// </summary>
        [TestMethod]
        public void TestFirstVisibleLine()
        {
            //複数行テスト用の文字列。
            StringBuilder b = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                b.Append(i.ToString() + Environment.NewLine);
            }
            string manyLine = b.ToString();

            foreach (NativeEdit edit in new NativeEdit[]{
                new NativeEdit(testDlg.IdentifyFromDialogId(1020)),
                new NativeEdit(testDlg.IdentifyFromDialogId(1038))})
            {
                edit.EmulateChangeText(manyLine);
                edit.SendMessage(EM_LINESCROLL, IntPtr.Zero, new IntPtr(10));
                Assert.AreEqual(10, edit.FirstVisibleLine);
            }
        }

        /// <summary>
        /// EmulateChangeSelectionとGetSelectionのテスト。
        /// </summary>
        [TestMethod]
        public void TestSelection()
        {
            foreach (NativeEdit edit in new NativeEdit[]{
                new NativeEdit(testDlg.IdentifyFromDialogId(1020)),
                new NativeEdit(testDlg.IdentifyFromDialogId(1038))})
            {
                edit.EmulateChangeText("01234567890123456789");

                edit.EmulateChangeSelection(2, 3);
                int start = 0, end = 0;
                edit.GetSelection(ref start, ref end);
                Assert.AreEqual(2, start);
                Assert.AreEqual(3, end);

                //非同期でも正常にに動作することを確認。
                Async a = new Async();
                edit.EmulateChangeSelection(5, 10, a);
                while (!a.IsCompleted)
                {
                    Thread.Sleep(10);
                }
                edit.GetSelection(ref start, ref end);
                Assert.AreEqual(5, start);
                Assert.AreEqual(10, end);
            }
        }

        /// <summary>
        /// EmulateChangeTextのイベントのテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeTextEvent()
        {
            //Edit。
            NativeEdit edit = new NativeEdit(testDlg.IdentifyFromDialogId(1020));
            edit.EmulateChangeText(string.Empty);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { edit.EmulateChangeText("a"); },
                new CodeInfo(1020, NativeMethods.WM_COMMAND, EN_UPDATE),
                new CodeInfo(1020, NativeMethods.WM_COMMAND, EN_CHANGE)
                ));

            //RitchEdit イベント発生の順番がEditと異なる。
            NativeEdit rich = new NativeEdit(testDlg.IdentifyFromDialogId(1038));
            rich.EmulateChangeText(string.Empty);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { rich.EmulateChangeText("a"); },
                new CodeInfo(1038, NativeMethods.WM_COMMAND, EN_CHANGE),
                new CodeInfo(1038, NativeMethods.WM_COMMAND, EN_UPDATE)
                ));
        }

        /// <summary>
        /// tEmulateChangeSelectionのイベントのテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeSelectionEvent()
        {
            //Edit 何もイベントは発生しない。
            NativeEdit edit = new NativeEdit(testDlg.IdentifyFromDialogId(1020));
            edit.EmulateChangeText("123");
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { edit.EmulateChangeSelection(1, 2); }));

            //RitchEdit EN_SELCHANGEが発生することを確認。
            NativeEdit rich = new NativeEdit(testDlg.IdentifyFromDialogId(1038));
            rich.EmulateChangeText("123");
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { rich.EmulateChangeSelection(1, 2); },
                //無視
                new CodeInfo[] {new CodeInfo(1038, NativeMethods.WM_COMMAND, EN_UPDATE)},
                //期待値
                new CodeInfo(1038, NativeMethods.WM_NOTIFY, EN_SELCHANGE)
                ));
        }

        /// <summary>
        /// EmulateChangeTextのイベントの非同期実行テスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeTextEventAsync()
        {
            NativeEdit edit = new NativeEdit(testDlg.IdentifyFromDialogId(1022));
            NativeEdit rich = new NativeEdit(testDlg.IdentifyFromDialogId(1040));

            Async async = new Async();
            edit.EmulateChangeText("abcdef", async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));

            async = new Async();
            rich.EmulateChangeText("abcdef", async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateChangeSelectionのイベントの非同期実行テスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeSelectionEventAsync()
        {
            //文字列設定　テスト対象ではないがここでもメッセージボックスが表示される。
            NativeEdit rich = new NativeEdit(testDlg.IdentifyFromDialogId(1040));
            Async async = new Async();
            rich.EmulateChangeText("abcdef", async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));

            //EmulateChangeSelectionの非同期確認。
            async = new Async();
            rich.EmulateChangeSelection(1, 1, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
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
            //EditとRitchEditのテスト。
            foreach (NativeEdit edit in new NativeEdit[]{
                new NativeEdit(testDlg.IdentifyFromDialogId(1020)),
                new NativeEdit(testDlg.IdentifyFromDialogId(1038))})
            {
                edit.EmulateChangeText("𩸽𩸽𩸽");
                Assert.AreEqual("𩸽𩸽𩸽", edit.Text);
                edit.EmulateChangeSelection(1, 2);
                int start = 0, end = 0;
                edit.GetSelection(ref start, ref end);
                Assert.AreEqual(1, start);
                Assert.AreEqual(2, end);
            }
        }
    }
}
