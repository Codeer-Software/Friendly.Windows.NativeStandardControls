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
    /// NativeIPAddressテストクラス。
    /// </summary>
    [TestClass]
    public class NativeIPAddressTest
    {
        const int IPN_FIRST = -860;
        const int IPN_FIELDCHANGED = IPN_FIRST;
        const int EN_CHANGE = 0x0300;

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
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1018));
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
                testDlg.Close();
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
        public void TestConstractor()
        {
            //WindowControlから作成。
            {
                NativeIPAddress ipadress = new NativeIPAddress(testDlg.IdentifyFromDialogId(1016));
                ipadress.EmulateChangeAddress(0, 1, 2, 3);
                byte f0 = 0, f1 = 0, f2 = 0, f3 = 0;
                ipadress.GetAddress(ref f0, ref f1, ref f2,ref f3);
                Assert.AreEqual(0, f0);
                Assert.AreEqual(1, f1);
                Assert.AreEqual(2, f2);
                Assert.AreEqual(3, f3);
            }
            //ハンドルから作成。
            {
                NativeIPAddress ipadress = new NativeIPAddress(app, testDlg.IdentifyFromDialogId(1016).Handle);
                ipadress.EmulateChangeAddress(10, 11, 12, 13);
                byte f0 = 0, f1 = 0, f2 = 0, f3 = 0;
                ipadress.GetAddress(ref f0, ref f1, ref f2, ref f3);
                Assert.AreEqual(10, f0);
                Assert.AreEqual(11, f1);
                Assert.AreEqual(12, f2);
                Assert.AreEqual(13, f3);
            }
        }

        /// <summary>
        /// EmulateChangeAddressとGetAddressの値の設定、取得テスト。
        /// </summary>
        [TestMethod]
        public void TestAddressValue()
        {
            NativeIPAddress ipadress = new NativeIPAddress(testDlg.IdentifyFromDialogId(1016));

            ipadress.EmulateChangeAddress(0, 1, 2, 3);
            byte f0 = 0, f1 = 0, f2 = 0, f3 = 0;
            ipadress.GetAddress(ref f0, ref f1, ref f2, ref f3);
            Assert.AreEqual(0, f0);
            Assert.AreEqual(1, f1);
            Assert.AreEqual(2, f2);
            Assert.AreEqual(3, f3);

            //非同期でも同様の効果があることを確認。
            Async a = new Async();
            ipadress.EmulateChangeAddress(10, 11, 12, 13, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            ipadress.GetAddress(ref f0, ref f1, ref f2, ref f3);
            Assert.AreEqual(10, f0);
            Assert.AreEqual(11, f1);
            Assert.AreEqual(12, f2);
            Assert.AreEqual(13, f3);
        }

        /// <summary>
        /// EmulateChangeAddressイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeAddressEvent()
        {
            //イベント確認。
            NativeIPAddress ipadress = new NativeIPAddress(testDlg.IdentifyFromDialogId(1016));
            ipadress.EmulateChangeAddress(0, 0, 0, 0);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { ipadress.EmulateChangeAddress(1, 2, 3, 4); },
                new CodeInfo(1016, NativeMethods.WM_NOTIFY, IPN_FIELDCHANGED),
                new CodeInfo(1016, NativeMethods.WM_COMMAND, 512),
                new CodeInfo(1016, NativeMethods.WM_COMMAND, 256),
                new CodeInfo(1016, NativeMethods.WM_COMMAND, EN_CHANGE),
                new CodeInfo(1016, NativeMethods.WM_NOTIFY, IPN_FIELDCHANGED),
                new CodeInfo(1016, NativeMethods.WM_COMMAND, EN_CHANGE),
                new CodeInfo(1016, NativeMethods.WM_NOTIFY, IPN_FIELDCHANGED),
                new CodeInfo(1016, NativeMethods.WM_COMMAND, EN_CHANGE),
                new CodeInfo(1016, NativeMethods.WM_NOTIFY, IPN_FIELDCHANGED),
                new CodeInfo(1016, NativeMethods.WM_COMMAND, EN_CHANGE),
                new CodeInfo(1016, NativeMethods.WM_NOTIFY, IPN_FIELDCHANGED)));

            //詳細なNotify内容を確認。
            NMIPADDRESS[] expectation = new NMIPADDRESS[5];
            expectation[0].iField = 0;
            expectation[0].iValue = 1;
            expectation[1].iField = 0;
            expectation[1].iValue = 11;
            expectation[2].iField = 1;
            expectation[2].iValue = 12;
            expectation[3].iField = 2;
            expectation[3].iValue = 13;
            expectation[4].iField = 3;
            expectation[4].iValue = 14;
            Assert.IsTrue(EventChecker.CheckNotifyDetail(testDlg,
               delegate { ipadress.EmulateChangeAddress(11, 12, 13, 14); },
                expectation));
        }

        /// <summary>
        /// EmulateChangeAddress非同期イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeAddressEventAsync()
        {
            //非同期で実行されることを確認。
            NativeIPAddress ipadress = new NativeIPAddress(testDlg.IdentifyFromDialogId(1017));
            Async async = new Async();
            ipadress.EmulateChangeAddress(0, 0, 0, 0, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }
    }
}
