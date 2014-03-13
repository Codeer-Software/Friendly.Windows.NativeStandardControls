using System;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using NativeStandardControls.TestLib;

namespace NativeStandardControls
{
    /// <summary>
    /// NativeButtonテストクラス。
    /// </summary>
    [TestClass]
    public class NativeButtonTest
    {
        const int BN_CLICKED = 0;
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
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1003));
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
                NativeButton checkButton = new NativeButton(testDlg.IdentifyFromDialogId(1004));
                checkButton.EmulateCheck(CheckState.Checked);
                Assert.AreEqual(CheckState.Checked, checkButton.CheckState);
            }
            //ハンドルから作成。
            {
                NativeButton checkButton = new NativeButton(app, testDlg.IdentifyFromDialogId(1004).Handle);
                checkButton.EmulateCheck(CheckState.Unchecked);
                Assert.AreEqual(CheckState.Unchecked, checkButton.CheckState);
            }
        }

        /// <summary>
        /// EmulateClickのイベントのテスト。
        /// </summary>
        [TestMethod]
        public void TestClickEvent()
        {
            //同期実行。BN_CLICKEDが発生していることを確認。
            NativeButton button = new NativeButton(testDlg.IdentifyFromDialogId(1000));
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { button.EmulateClick(); },
                new CodeInfo(1000, NativeMethods.WM_COMMAND, BN_CLICKED)));
        }

        /// <summary>
        /// EmulateClickのイベントの非同期テスト。
        /// </summary>
        [TestMethod]
        public void TestClickEventAsync()
        {
            NativeButton button = new NativeButton(testDlg.IdentifyFromDialogId(1001));
            Async async = new Async();
            button.EmulateClick(async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateCheckとCheckStateの値変更、設定テスト。
        /// </summary>
        [TestMethod]
        public void TestCheckStateValue()
        {
            //同期で実行
            NativeButton checkButton = new NativeButton(testDlg.IdentifyFromDialogId(1004));
            checkButton.EmulateCheck(CheckState.Checked);
            Assert.AreEqual(CheckState.Checked, checkButton.CheckState);
            checkButton.EmulateCheck(CheckState.Unchecked);
            Assert.AreEqual(CheckState.Unchecked, checkButton.CheckState);
            checkButton.EmulateCheck(CheckState.Indeterminate);
            Assert.AreEqual(CheckState.Indeterminate, checkButton.CheckState);

            //非同期メソッドでも同様の効果があることを確認。（内部的に間違ったメソッド呼び出しになっていないことの確認。）
            Async async = new Async();
            checkButton.EmulateCheck(CheckState.Unchecked, async);
            while (!async.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.AreEqual(CheckState.Unchecked, checkButton.CheckState);
        }

        /// <summary>
        /// EmulateCheckのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestCheckEvent()
        {
            NativeButton checkButton = new NativeButton(testDlg.IdentifyFromDialogId(1004));

            //同期実行。BN_CLICKEDが発生していることを確認。
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { checkButton.EmulateCheck(CheckState.Checked); },
                new CodeInfo(1004, NativeMethods.WM_COMMAND, BN_CLICKED)));

            //同じ状態であれば、BN_CLICKEDが発生しないことを確認。
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
               delegate { checkButton.EmulateCheck(CheckState.Checked); }));
        }

        /// <summary>
        /// EmulateCheckのイベント非同期テスト。
        /// </summary>
        [TestMethod]
        public void TestCheckEventAsync()
        {
            NativeButton checkButton = new NativeButton(testDlg.IdentifyFromDialogId(1005));
            Async async = new Async();
            checkButton.EmulateCheck(CheckState.Checked, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateCheckのラジオボタンのチェック変更のテスト。
        /// </summary>
        [TestMethod]
        public void TestRadioCheck()
        {
            NativeButton radio1 = new NativeButton(testDlg.IdentifyFromDialogId(1044));
            NativeButton radio2 = new NativeButton(testDlg.IdentifyFromDialogId(1045));
            radio1.EmulateCheck(CheckState.Checked);
            Assert.AreEqual(CheckState.Unchecked, radio2.CheckState);
            radio2.EmulateCheck(CheckState.Checked);
            Assert.AreEqual(CheckState.Unchecked, radio1.CheckState);
        }

        /// <summary>
        /// EmulateCheckでの例外テスト。
        /// </summary>
        [TestMethod]
        public void TestCheckException()
        {
            //イベントのテストではないのでメッセージボックスが出ないようにする。
            app[typeof(EventChecker), "ClearAsyncMsgBox"](testDlg.Handle);
            try
            {
                NativeButton radio = new NativeButton(testDlg.IdentifyFromDialogId(1044));
                radio.EmulateCheck(CheckState.Unchecked);
                Assert.Fail();
            }
            catch (FriendlyOperationException e)
            {
                Assert.IsTrue(e.Message.IndexOf("指定のチェック状態はサポートされていません。" + Environment.NewLine +
                            "ラジオボタンの場合、チェックをOFFにすることはできません。" + Environment.NewLine +
                            "他のラジオボタンのチェックをONにすることで指定のラジオボタンをOFFにしてください。") != -1);
            }
            try
            {
                NativeButton checkButton = new NativeButton(testDlg.IdentifyFromDialogId(1005));
                checkButton.EmulateCheck(CheckState.Indeterminate);
                Assert.Fail();
            }
            catch (FriendlyOperationException e)
            {
                Assert.IsTrue(e.Message.IndexOf("指定のチェック状態はサポートされていません。" + Environment.NewLine +
                            "ラジオボタンの場合、チェックをOFFにすることはできません。" + Environment.NewLine +
                            "他のラジオボタンのチェックをONにすることで指定のラジオボタンをOFFにしてください。") != -1);
            }
        }
    }
}