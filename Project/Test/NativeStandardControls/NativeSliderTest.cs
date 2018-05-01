using System;
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
    /// NativeSliderテストクラス。
    /// </summary>
    [TestClass]
    public class NativeSliderTest
    {
        const int SB_THUMBPOSITION = 4;
        const int TB_THUMBTRACK = 5;
        const int TRBN_FIRST = -1501;
        const int TRBN_THUMBPOSCHANGING = TRBN_FIRST - 1;

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
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1024));
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
                NativeSlider slider = new NativeSlider(testDlg.IdentifyFromDialogId(1019));
                Assert.AreEqual(1000, slider.Max);
            }
            //ハンドルから作成。
            {
                NativeSlider slider = new NativeSlider(app, testDlg.IdentifyFromDialogId(1019).Handle);
                Assert.AreEqual(1000, slider.Max);
            }
        }

        /// <summary>
        /// Minのテスト。
        /// </summary>
        [TestMethod]
        public void TestMin()
        {
            NativeSlider slider = new NativeSlider(testDlg.IdentifyFromDialogId(1019));
            Assert.AreEqual(10, slider.Min);
        }

        /// <summary>
        /// Maxのテスト。
        /// </summary>
        [TestMethod]
        public void TestMax()
        {
            NativeSlider slider = new NativeSlider(testDlg.IdentifyFromDialogId(1019));
            Assert.AreEqual(1000, slider.Max);
        }

        /// <summary>
        /// IsVerticalのテスト。
        /// </summary>
        [TestMethod]
        public void TestIsVertical()
        {
            NativeSlider slider = new NativeSlider(testDlg.IdentifyFromDialogId(1019));
            NativeSlider vslider = new NativeSlider(testDlg.IdentifyFromDialogId(1026));
            Assert.IsFalse(slider.IsVertical);
            Assert.IsTrue(vslider.IsVertical);
        }

        /// <summary>
        /// EmulateChangePosとPosの値テスト。
        /// </summary>
        [TestMethod]
        public void TestPosValue()
        {
            NativeSlider slider = new NativeSlider(testDlg.IdentifyFromDialogId(1019));
            slider.EmulateChangePos(150);
            Assert.AreEqual(150, slider.Pos);

            //非同期でも同様の効果があることを確認。
            Async a = new Async();
            slider.EmulateChangePos(200, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.AreEqual(200, slider.Pos);
        }

        /// <summary>
        /// EmulateChangePosの非同期実行イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestPosEventAsync()
        {
            NativeSlider slider = new NativeSlider(testDlg.IdentifyFromDialogId(1020));
            Async async = new Async();
            slider.EmulateChangePos(100, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }
    }
}
