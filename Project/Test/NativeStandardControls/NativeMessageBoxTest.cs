using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using System.Diagnostics;
using NativeStandardControls.TestLib;
using Codeer.Friendly.Windows.NativeStandardControls;
using Codeer.Friendly;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NativeStandardControls
{
    /// <summary>
    /// メッセージボックスのテスト。
    /// </summary>
    [TestClass]
    public class NativeMessageBoxTest
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
            if (app != null)
            {
                app.Dispose();
                Process process = Process.GetProcessById(app.ProcessId);
                process.CloseMainWindow();
                app = null;
            }
        }

        /// <summary>
        /// ネイティブのメッセージボックス。
        /// </summary>
        /// <param name="h">親ウィンドウハンドル。</param>
        /// <param name="m">メッセージ。</param>
        /// <param name="c">キャプション。</param>
        /// <param name="type">タイプ。</param>
        /// <returns>結果。</returns>
        [DllImport("user32.dll")]
        public static extern int MessageBoxA(int h, string m, string c, int type);

        /// <summary>
        /// Nativeのメッセージボックステスト。
        /// </summary>
        [TestMethod]
        public void TestNative()
        {
            Async async = new Async();
            app[GetType(), "MessageBoxA", async](0, "Message", "Title", 0);
            NativeMessageBox msg = new NativeMessageBox(WindowControl.WaitForIdentifyFromWindowText(app, "Title"));
            Assert.AreEqual("Title", msg.Title);
            Assert.AreEqual("Message", msg.Message);
            msg.EmulateButtonClick("OK");
            async.WaitForCompletion();
        }

        /// <summary>
        /// .Netのメッセージボックステスト。
        /// </summary>
        [TestMethod]
        public void TestNet()
        {
            Async async = new Async();
            app[typeof(MessageBox), "Show", async]("Message", "Title", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NativeMessageBox msg = new NativeMessageBox(main.WaitForNextModal());
            Assert.AreEqual("Title", msg.Title);
            Assert.AreEqual("Message", msg.Message);
            msg.EmulateButtonClick("OK");
            async.WaitForCompletion();
        }
    }
}
