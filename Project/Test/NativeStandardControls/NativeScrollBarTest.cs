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
    /// NativeScrollBarテストクラス。
    /// </summary>
    [TestClass]
    public class NativeScrollBarTest
    {
        internal const int SB_THUMBPOSITION = 4;
        internal const int SB_THUMBTRACK = 5;
        internal const int SB_ENDSCROLL = 8;

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
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1030));
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
                NativeScrollBar scroll = new NativeScrollBar(testDlg.IdentifyFromDialogId(1027));
                Assert.AreEqual(50, scroll.ScrollPos);
            }
            //ハンドルから作成。
            {
                NativeScrollBar scroll = new NativeScrollBar(app, testDlg.IdentifyFromDialogId(1027).Handle);
                Assert.AreEqual(50, scroll.ScrollPos);
            }
        }

        /// <summary>
        /// GetScrollRangeのテスト。
        /// </summary>
        [TestMethod]
        public void TesScrollRange()
        {
            NativeScrollBar hscroll = new NativeScrollBar(testDlg.IdentifyFromDialogId(1027));
            int min = 0, max = 0;
            hscroll.GetScrollRange(ref min, ref max);
            Assert.AreEqual(1, min);
            Assert.AreEqual(100, max);
        }

        /// <summary>
        /// IsVerticalのテスト。
        /// </summary>
        [TestMethod]
        public void TestIsVertical()
        {
            NativeScrollBar hscroll = new NativeScrollBar(testDlg.IdentifyFromDialogId(1027));
            NativeScrollBar vscroll = new NativeScrollBar(testDlg.IdentifyFromDialogId(1028));
            Assert.IsTrue(vscroll.IsVertical);
            Assert.IsFalse(hscroll.IsVertical);
        }

        /// <summary>
        /// EmulateScrollとScrollPosの値テスト。
        /// </summary>
        [TestMethod]
        public void TestScrollPosValue()
        {
            NativeScrollBar hscroll = new NativeScrollBar(testDlg.IdentifyFromDialogId(1027));
            hscroll.EmulateScroll(30);
            Assert.AreEqual(30, hscroll.ScrollPos);

            NativeScrollBar vscroll = new NativeScrollBar(testDlg.IdentifyFromDialogId(1028));
            vscroll.EmulateScroll(30);
            Assert.AreEqual(30, vscroll.ScrollPos);

            //非同期で実行しても同様の効果があることを確認。
            Async a = new Async();
            vscroll.EmulateScroll(50, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.AreEqual(50, vscroll.ScrollPos);
        }

        /// <summary>
        /// EmulateScrollのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestScrollPosEvent()
        {
            NativeScrollBar hscroll = new NativeScrollBar(testDlg.IdentifyFromDialogId(1027));
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { hscroll.EmulateScroll(30); },
                new CodeInfo(1027, NativeMethods.WM_HSCROLL, SB_THUMBTRACK, 30),
                new CodeInfo(1027, NativeMethods.WM_HSCROLL, SB_THUMBPOSITION, 30),
                new CodeInfo(1027, NativeMethods.WM_HSCROLL, SB_ENDSCROLL, 0)));

            NativeScrollBar vscroll = new NativeScrollBar(testDlg.IdentifyFromDialogId(1028));
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { vscroll.EmulateScroll(30); },
                new CodeInfo(1028, NativeMethods.WM_VSCROLL, SB_THUMBTRACK, 30),
                new CodeInfo(1028, NativeMethods.WM_VSCROLL, SB_THUMBPOSITION, 30),
                new CodeInfo(1028, NativeMethods.WM_VSCROLL, SB_ENDSCROLL, 0)));
        }

        /// <summary>
        /// EmulateScrollの非同期実行イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestScrollPosEventAsync()
        {
            //非同期実行。
            NativeScrollBar scroll = new NativeScrollBar(testDlg.IdentifyFromDialogId(1029));
            Async async = new Async();
            scroll.EmulateScroll(20, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }
    }
}
