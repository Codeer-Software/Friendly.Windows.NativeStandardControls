using System;
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
    /// NativeProgressテストクラス。
    /// </summary>
    [TestClass]
    public class NativeProgressTest
    {
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
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1034));
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
                NativeProgress progress = new NativeProgress(testDlg.IdentifyFromDialogId(1033));
                Assert.AreEqual(30, progress.Pos);
            }
            //ハンドルから作成。
            {
                NativeProgress progress = new NativeProgress(app, testDlg.IdentifyFromDialogId(1033).Handle);
                Assert.AreEqual(30, progress.Pos);
            }
        }
               
        /// <summary>
        /// Minのテスト。
        /// </summary>
        [TestMethod]
        public void TestMin()
        {
            NativeProgress progress = new NativeProgress(testDlg.IdentifyFromDialogId(1033));
            Assert.AreEqual(10, progress.Min);
        }

        /// <summary>
        /// Maxのテスト。
        /// </summary>
        [TestMethod]
        public void TestMax()
        {
            NativeProgress progress = new NativeProgress(testDlg.IdentifyFromDialogId(1033));
            Assert.AreEqual(100, progress.Max);
        }

        /// <summary>
        /// Posのテスト。
        /// </summary>
        [TestMethod]
        public void TestPos()
        {
            NativeProgress progress = new NativeProgress(testDlg.IdentifyFromDialogId(1033));
            Assert.AreEqual(30, progress.Pos);
        }
    }
}
