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
using Codeer.Friendly.Windows.Grasp.ScreenTransition;

namespace NativeStandardControls
{
    [TestClass]
    public class ScreenTransition
    {
        WindowsAppFriend app;
        WindowControl main;
        const int IDC_BUTTON_BUTTON_TEST = 1003;
        const int IDC_BUTTON_MODELLESS = 1047;
        const int IDCANCEL = 2;
        const int IDC_BUTTON_SYNC = 1000;

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

        class ModalDriver
        {
            internal NativeButtonSync ButtonCancel { get; private set; }

            internal NativeButtonModal<ModalDriver> ButtonModalNotShow { get; private set; }
            internal NativeButtonModeless<ModalDriver> ButtonModelessNotShow { get; private set; }

            internal ModalDriver(WindowControl w, InvokeSync.Sync a)
            {
                IsSync = false;
                ButtonCancel = new NativeButtonSync(w.IdentifyFromDialogId(IDCANCEL), a);
                ButtonModalNotShow = new NativeButtonModal<ModalDriver>(w.IdentifyFromDialogId(IDC_BUTTON_SYNC), (ww, aa) => { throw new NotSupportedException(); });
                ButtonModelessNotShow = new NativeButtonModeless<ModalDriver>(w.IdentifyFromDialogId(IDC_BUTTON_SYNC), () => { return null; });
            }

            internal ModalDriver(WindowControl w, Async a) : this(w, () => { a.WaitForCompletion(); IsSync = true; }) { }

            public static bool IsSync { get; set; }
        }

        class ModalDriverForModeless
        {
            internal NativeButton ButtonCancel { get; private set; }
            internal ModalDriverForModeless(WindowControl w)
            {
                ButtonCancel = new NativeButton(w.IdentifyFromDialogId(IDCANCEL));
            }
        }

        class ModalDriverForModalOrSyncTest
        {
            //実際にモーダルは出ない
            //モーダルが出ないテスト用
            internal NativeButtonModalOrSync<ModalDriver> ButtonCancel { get; private set; }
            internal ModalDriverForModalOrSyncTest(WindowControl w, Async a)
            {
                ButtonCancel = new NativeButtonModalOrSync<ModalDriver>(w.IdentifyFromDialogId(IDCANCEL),(ww, aa) => new ModalDriver(ww, aa), a);
            }
        }

        [TestMethod]
        public void NativeButtonModal_NativeButtonSync_Test()
        {
            NativeButtonModal<ModalDriver> button = new NativeButtonModal<ModalDriver>(main.IdentifyFromDialogId(IDC_BUTTON_BUTTON_TEST), (w, a) => new ModalDriver(w, a));
            button.EmulateClick().ButtonCancel.EmulateClick();
            //同期したことの確認
            Assert.IsTrue(ModalDriver.IsSync);
        }

        [TestMethod]
        public void NativeButtonModal_Modeless_NotDialogShow_Test()
        {
            NativeButtonModal<ModalDriver> button = new NativeButtonModal<ModalDriver>(main.IdentifyFromDialogId(IDC_BUTTON_BUTTON_TEST), (w, a) => new ModalDriver(w, a));
            var dlg = button.EmulateClick();
            Assert.IsNull(dlg.ButtonModalNotShow.EmulateClick());
            Assert.IsNull(dlg.ButtonModelessNotShow.EmulateClick());
            dlg.ButtonCancel.EmulateClick();
        }

        [TestMethod]
        public void NativeButtonModal_ModelessShow()
        {
            NativeButtonModal<ModalDriver> button = new NativeButtonModal<ModalDriver>(main.IdentifyFromDialogId(IDC_BUTTON_MODELLESS), (w, a) => new ModalDriver(w, a));
            Assert.IsNull(button.EmulateClick());
        }

        [TestMethod]
        public void NativeButtonModalOrSync_Test()
        {
            NativeButtonModalOrSync<ModalDriverForModalOrSyncTest> button =
                new NativeButtonModalOrSync<ModalDriverForModalOrSyncTest>(main.IdentifyFromDialogId(IDC_BUTTON_BUTTON_TEST), (w, a) => new ModalDriverForModalOrSyncTest(w, a),
                    //同期が使われることはない
                    () => { throw new NotSupportedException(); });
            Assert.IsNull(button.EmulateClick().ButtonCancel.EmulateClick());
        }

        [TestMethod]
        public void NativeButtonModeless_Test()
        {
            NativeButtonModeless<ModalDriverForModeless> button = new NativeButtonModeless<ModalDriverForModeless>(main.IdentifyFromDialogId(IDC_BUTTON_MODELLESS),
                () => new ModalDriverForModeless(WindowControl.WaitForIdentifyFromWindowText(app, "Dialog")));
            var dlg = button.EmulateClick();
            dlg.ButtonCancel.EmulateClick();
        }
    }
}
