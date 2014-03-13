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
    ///  NativeWindowテストクラス
    /// </summary>
    [TestClass]
    public class NativeWindowTest
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
        /// Visibleのテスト。
        /// </summary>
        [TestMethod]
        public void TestVisible()
        {
            NativeButton button = new NativeButton(testDlg.IdentifyFromDialogId(1000));
            Assert.IsTrue(button.Visible);
            NativeMethods.ShowWindow(button.Handle, 0);
            Assert.IsFalse(button.Visible);
        }

        /// <summary>
        /// Enableのテスト。
        /// </summary>
        [TestMethod]
        public void TestEnable()
        {
            NativeButton button = new NativeButton(testDlg.IdentifyFromDialogId(1000));
            Assert.IsTrue(button.Enabled);
            NativeMethods.EnableWindow(button.Handle, false);
            Assert.IsFalse(button.Enabled);
        }
    }
}
