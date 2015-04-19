using System;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Dynamic;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using NativeStandardControls.TestLib;
using System.Linq;

namespace NativeStandardControls
{
    [TestClass]
    public class NativeDialogsTest
    {
        WindowsAppFriend app;
        WindowControl main;

        /// <summary>
        /// 初期化。
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            app = new WindowsAppFriend(Process.Start(TargetPath.NativeControls));
            EventChecker.Initialize(app);
            main = WindowControl.FromZTop(app);
        }

        /// <summary>
        /// 終了処理。
        /// </summary>
        [TestCleanup]
        public void TearDown()
        {
            Process process = Process.GetProcessById(app.ProcessId);
            process.CloseMainWindow();
        }

        [TestMethod]
        public void MessageBox()
        {
            {
                var a = new Async();
                app.Type<MessageBox>().Show(a, "", "", MessageBoxButtons.OKCancel);
                var msg = new NativeMessageBox(main.WaitForNextModal(), a);
                msg.ButtonOK.EmulateClick();
            }
            {
                var a = new Async();
                app.Type<MessageBox>().Show(a, "", "", MessageBoxButtons.OKCancel);
                var msg = new NativeMessageBox(main.WaitForNextModal(), a);
                msg.ButtonCancel.EmulateClick();
            }
            {
                var a = new Async();
                app.Type<MessageBox>().Show(a, "", "", MessageBoxButtons.YesNo);
                var msg = new NativeMessageBox(main.WaitForNextModal(), a);
                msg.ButtonYes.EmulateClick();
            }
            {
                var a = new Async();
                app.Type<MessageBox>().Show(a, "", "", MessageBoxButtons.YesNo);
                var msg = new NativeMessageBox(main.WaitForNextModal(), a);
                msg.ButtonNo.EmulateClick();
            }
            {
                var a = new Async();
                app.Type<MessageBox>().Show(a, "", "", MessageBoxButtons.AbortRetryIgnore);
                var msg = new NativeMessageBox(main.WaitForNextModal(), a);
                msg.ButtonAbort.EmulateClick();
            }
            {
                var a = new Async();
                app.Type<MessageBox>().Show(a, "", "", MessageBoxButtons.AbortRetryIgnore);
                var msg = new NativeMessageBox(main.WaitForNextModal(), a);
                msg.ButtonRetry.EmulateClick();
            }
            {
                var a = new Async();
                app.Type<MessageBox>().Show(a, "", "", MessageBoxButtons.AbortRetryIgnore);
                var msg = new NativeMessageBox(main.WaitForNextModal(), a);
                msg.ButtonIgnore.EmulateClick();
            }
        }

        [TestMethod]
        public void MessageBox_Sync()
        {
            var a = new Async();
            app.Type<MessageBox>().Show(a, "", "", MessageBoxButtons.OKCancel);
            bool isSync = false;
            var msg = new NativeMessageBox(main.WaitForNextModal(), () => { a.WaitForCompletion(); isSync = true; });
            msg.ButtonOK.EmulateClick();
            Assert.IsTrue(isSync);
        }

        [TestMethod]
        public void FileDialog()
        {
            {
                var a = new Async();
                var dlg = app.Type<OpenFileDialog>()();
                dlg.ShowDialog(a);
                var file = new NativeFileDialog(main.WaitForNextModal(), a);
                file.ButtonCancel.EmulateClick();
            }
            {
                var a = new Async();
                var dlg = app.Type<OpenFileDialog>()();
                dlg.CheckFileExists = false;
                dlg.ShowDialog(a);
                var file = new NativeFileDialog(main.WaitForNextModal(), a);
                file.ComboBoxFilePath.EmulateChangeEditText("abc");
                file.ButtonOpen.EmulateClick();
            }
            {
                var a = new Async();
                var dlg = app.Type<OpenFileDialog>()();
                dlg.ShowDialog(a);
                var file = new NativeFileDialog(main.WaitForNextModal(), a);
                file.ComboBoxFilePath.EmulateChangeEditText("abc");
                var msg = file.ButtonOpen.EmulateClick();
                msg.ButtonOK.EmulateClick();
                file.ButtonCancel.EmulateClick();
            }

            {
                var a = new Async();
                var dlg = app.Type<SaveFileDialog>()();
                dlg.CheckFileExists = false;
                dlg.ShowDialog(a);
                var file = new NativeFileDialog(main.WaitForNextModal(), a);
                file.ComboBoxFilePath.EmulateChangeEditText("abc");
                file.ButtonSave.EmulateClick();
            }

            {
                var a = new Async();
                var dlg = app.Type<SaveFileDialog>()();
                dlg.CheckFileExists = false;
                dlg.ShowDialog(a);
                var file = new NativeFileDialog(main.WaitForNextModal(), a);
                string path = "";
                for (int i = 0; i < 1024; i++)
                {
                    path += "a";
                }
                file.ComboBoxFilePath.EmulateChangeEditText(path);
                var msg = file.ButtonSave.EmulateClick();
                msg.ButtonOK.EmulateClick();
                file.ButtonCancel.EmulateClick();
            }
        }

        [TestMethod]
        public void FileDialogSync()
        {
            var a = new Async();
            var dlg = app.Type<OpenFileDialog>()();
            dlg.ShowDialog(a);
            bool isSync = false;
            var file = new NativeFileDialog(main.WaitForNextModal(), () => { a.WaitForCompletion(); isSync = true; });
            file.ButtonCancel.EmulateClick();
            Assert.IsTrue(isSync);
        }

        [TestMethod]
        public void FolderDialog()
        {
            {
                var a = new Async();
                var dlg = app.Type<FolderBrowserDialog>()();
                dlg.ShowDialog(a);
                var folder = new NativeFolderDialog(main.WaitForNextModal(), a);
                folder.ButtonOK.EmulateClick();
            }
            {
                var a = new Async();
                var dlg = app.Type<FolderBrowserDialog>()();
                dlg.ShowDialog(a);
                var folder = new NativeFolderDialog(main.WaitForNextModal(), a);
                folder.ButtonCancel.EmulateClick();
            }
            {
                var a = new Async();
                var dlg = app.Type<FolderBrowserDialog>()();
                dlg.ShowDialog(a);
                var folder = new NativeFolderDialog(main.WaitForNextModal(), a);
                folder.TreeFolder.EmulateSelectFolder(@"デスクトップ", @"PC", @"Windows (C:)", @"Program Files");
                folder.ButtonOK.EmulateClick();
                Assert.AreEqual(@"C:\Program Files", (string)dlg.SelectedPath);
            }
        }

        [TestMethod]
        public void FolderDialogSync()
        {
            var a = new Async();
            var dlg = app.Type<FolderBrowserDialog>()();
            dlg.ShowDialog(a);
            bool isSync = false;
            var folder = new NativeFolderDialog(main.WaitForNextModal(), () => { a.WaitForCompletion(); isSync = true; });
            folder.ButtonOK.EmulateClick();
            Assert.IsTrue(isSync);
        }
    }
}
