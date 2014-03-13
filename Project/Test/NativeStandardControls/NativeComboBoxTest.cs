using System;
using System.Threading;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using NativeStandardControls.TestLib;
using System.Collections.Generic;

namespace NativeStandardControls
{
    /// <summary>
    /// NativeComboBoxクラス。
    /// </summary>
    [TestClass]
    public class NativeComboBoxTest
    {
        const int CBN_ERRSPACE = (-1);
        const int CBN_SELCHANGE = 1;
        const int CBN_DBLCLK = 2;
        const int CBN_SETFOCUS = 3;
        const int CBN_KILLFOCUS = 4;
        const int CBN_EDITCHANGE = 5;
        const int CBN_EDITUPDATE = 6;
        const int CBN_DROPDOWN = 7;
        const int CBN_CLOSEUP = 8;
        const int CBN_SELENDOK = 9;
        const int CBN_SELENDCANCEL = 10;

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
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1008));
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
        /// 結果フィルタ
        /// </summary>
        /// <param name="result">結果</param>
        /// <returns></returns>
        internal CodeInfo[] FilterResult(CodeInfo[] result)
        {
            List<CodeInfo> list = new List<CodeInfo>();
            foreach (CodeInfo element in result)
            {
                if (element.message == NativeMethods.WM_COMMAND)
                {
                    switch (element.code)
                    {
                        case CBN_SETFOCUS:
                        case CBN_KILLFOCUS:
                            continue;
                    }
                }
                if (element.message == NativeMethods.WM_NOTIFY)
                {
                    continue;
                }
                list.Add(element);
            }
            return list.ToArray();
        }
        const int CBEN_FIRST = -800;
        const int CBEN_LAST = -830;
        CodeInfo[] CreateIgnorMessage(int id, params CodeInfo[] add)
        {
            List<CodeInfo> list = new List<CodeInfo>(add);
            list.Add(new CodeInfo(id, NativeMethods.WM_COMMAND, CBN_SETFOCUS));
            list.Add(new CodeInfo(id, NativeMethods.WM_COMMAND, CBN_KILLFOCUS));
            for (int i = CBEN_FIRST; CBEN_LAST <= i; i--)
            {
                list.Add(new CodeInfo(id, NativeMethods.WM_NOTIFY, i));
            }
            return list.ToArray();
        }

        /// <summary>
        /// コンストラクタのテスト。
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            //WindowControlから作成。
            {
                NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1006));
                combo.EmulateSelectItem(1);
                Assert.AreEqual(1, combo.SelectedItemIndex);
            }
            //ハンドルから作成。
            {
                NativeComboBox combo = new NativeComboBox(app, testDlg.IdentifyFromDialogId(1006).Handle);
                combo.EmulateSelectItem(1);
                Assert.AreEqual(1, combo.SelectedItemIndex);
            }
        }

        /// <summary>
        /// ItemCountのテスト。
        /// </summary>
        [TestMethod]
        public void TestItemCount()
        {
            NativeComboBox[] combos = new NativeComboBox[]{
                new NativeComboBox(testDlg.IdentifyFromDialogId(1006)),
                new NativeComboBox(testDlg.IdentifyFromDialogId(1007))
            };
            foreach (NativeComboBox combo in combos)
            {
                Assert.AreEqual((NativeMethods.IsWindowUnicode(testDlg.Handle)) ? 4 : 3, combo.ItemCount);
            }
        }

        /// <summary>
        /// EmulateSelectItem,SelectedItemIndexのテスト。
        /// </summary>
        [TestMethod]
        public void TestSelectItem()
        {
            NativeComboBox[] combos = new NativeComboBox[]{
                new NativeComboBox(testDlg.IdentifyFromDialogId(1006)),
                new NativeComboBox(testDlg.IdentifyFromDialogId(1007))
            };
            foreach (NativeComboBox combo in combos)
            {
                combo.EmulateSelectItem(1);
                Assert.AreEqual(1, combo.SelectedItemIndex);

                //非同期でも同様の効果があることを確認。
                Async a = new Async();
                combo.EmulateSelectItem(2, a);
                while (!a.IsCompleted)
                {
                    Thread.Sleep(10);
                }
                Assert.AreEqual(2, combo.SelectedItemIndex);
            }
        }

        /// <summary>
        /// EmulateChangeEditTextとTextのテスト。
        /// </summary>
        [TestMethod]
        public void TestText()
        {
            NativeComboBox[] combos = new NativeComboBox[]{
                new NativeComboBox(testDlg.IdentifyFromDialogId(1006)),
                new NativeComboBox(testDlg.IdentifyFromDialogId(1007))
            };
            foreach (NativeComboBox combo in combos)
            {
                combo.EmulateChangeEditText("123");
                Assert.AreEqual("123", combo.Text);

                //非同期でも同様の効果があることを確認。
                Async a = new Async();
                combo.EmulateChangeEditText("abcd", a);
                while (!a.IsCompleted)
                {
                    Thread.Sleep(10);
                }
                Assert.AreEqual("abcd", combo.Text);
            }
            //ドロップダウン形式の場合。
            NativeComboBox exDropDown = new NativeComboBox(testDlg.IdentifyFromDialogId(1010));
            exDropDown.EmulateSelectItem(1);
            Assert.AreEqual("b", exDropDown.Text);
        }

        /// <summary>
        /// GetItemTextのテスト。
        /// </summary>
        [TestMethod]
        public void TestItemText()
        {
            NativeComboBox[] combos = new NativeComboBox[]{
                new NativeComboBox(testDlg.IdentifyFromDialogId(1006)),
                new NativeComboBox(testDlg.IdentifyFromDialogId(1007))
            };
            foreach (NativeComboBox combo in combos)
            {
                Assert.AreEqual("あ", combo.GetItemText(0));
                Assert.AreEqual("b", combo.GetItemText(1));
                Assert.AreEqual("c", combo.GetItemText(2));
            }
        }

        /// <summary>
        /// GetItemDataのテスト。
        /// </summary>
        [TestMethod]
        public void TestItemData()
        {
            NativeComboBox[] combos = new NativeComboBox[]{
                new NativeComboBox(testDlg.IdentifyFromDialogId(1006)),
                new NativeComboBox(testDlg.IdentifyFromDialogId(1007))
            };
            foreach (NativeComboBox combo in combos)
            {
                Assert.AreEqual(new IntPtr(0), combo.GetItemData(0));
                Assert.AreEqual(new IntPtr(1), combo.GetItemData(1));
                Assert.AreEqual(new IntPtr(2), combo.GetItemData(2));
            }
        }

        /// <summary>
        /// ComboBoxに対するEmulateSelectItemのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxSelectItemEvent()
        {
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1006));

            //イベント発生テスト。指定の状態と現在の状態が同じ場合はイベントの通知は発生しない。
            //それ以外はCBN_SELENDOK,CBN_SELCHANGEが発生すること。
            combo.EmulateSelectItem(0);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateSelectItem(1); },
                //無視。
                CreateIgnorMessage(1006, new CodeInfo(1006, NativeMethods.WM_COMMAND, CBN_SELENDCANCEL)),
                //期待値。
                new CodeInfo(1006, NativeMethods.WM_COMMAND, CBN_SELENDOK),
                new CodeInfo(1006, NativeMethods.WM_COMMAND, CBN_SELCHANGE)));

            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateSelectItem(1); },
                //無視。
                CreateIgnorMessage(1006, new CodeInfo(1006, NativeMethods.WM_COMMAND, CBN_SELENDCANCEL)),
                new CodeInfo[0]));
        }

        /// <summary>
        /// ComboBoxEx32に対するEmulateSelectItemのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxEx32SelectItemEvent()
        {
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1007));

            //イベント発生テスト。指定の状態と現在の状態が同じ場合はイベントの通知は発生しない。
            //それ以外はCBN_SELENDOK,CBN_SELCHANGEが発生すること。
            //ComboBoxEx32でドロップダウンリストでない場合はCBN_EDITCHANGEも発生すること。
            combo.EmulateSelectItem(0);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateSelectItem(1); },
                //無視。
                CreateIgnorMessage(1007, new CodeInfo(1007, NativeMethods.WM_COMMAND, CBN_SELENDCANCEL)),
                //期待値。
                new CodeInfo(1007, NativeMethods.WM_COMMAND, CBN_EDITCHANGE),
                new CodeInfo(1007, NativeMethods.WM_COMMAND, CBN_SELENDOK),
                new CodeInfo(1007, NativeMethods.WM_COMMAND, CBN_SELCHANGE)
                ));

            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateSelectItem(1); },
                //無視。
                CreateIgnorMessage(1007, new CodeInfo(1007, NativeMethods.WM_COMMAND, CBN_SELENDCANCEL)),
                //期待値。
                new CodeInfo[0]));
        }

        /// <summary>
        /// ComboBoxEx32のドロップダウンリストに対するEmulateSelectItemのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxEx32DropDownListSelectItemEvent()
        {
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1010));

            //イベント発生テスト。指定の状態と現在の状態が同じ場合はイベントの通知は発生しない。
            //それ以外はCBN_SELENDOK,CBN_SELCHANGEが発生すること。
            combo.EmulateSelectItem(0);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateSelectItem(1); },
                //無視。
                CreateIgnorMessage(1010, new CodeInfo(1010, NativeMethods.WM_COMMAND, CBN_SELENDCANCEL)),
                //期待値。
                new CodeInfo(1010, NativeMethods.WM_COMMAND, CBN_SELENDOK),
                new CodeInfo(1010, NativeMethods.WM_COMMAND, CBN_SELCHANGE)));

            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateSelectItem(1); },
                //無視。
                CreateIgnorMessage(1010, new CodeInfo(1010, NativeMethods.WM_COMMAND, CBN_SELENDCANCEL)),
                //期待値。
                new CodeInfo[0]
            ));
        }

        /// <summary>
        /// ComboBoxに対するEmulateSelectItemの非同期実行イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxSelectItemEventAsync()
        {
            //非同期実行ができること。CBN_SELCHANGEのイベントでメッセージボックスが表示されるのでそれを閉じる。
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1008));
            Async async = new Async();
            combo.EmulateSelectItem(1, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// ComboBoxEx32に対するEmulateSelectItemの非同期実行イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxEx32SelectItemEventAsync()
        {
            //ComboEx32 CBN_SELCHANGE、CBN_EDITCHANGEのイベントでメッセージボックスが出るのでそれを閉じる。
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1009));
            Async async = new Async();
            combo.EmulateSelectItem(2, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// ComboBoxに対するEmulateChangeEditTextのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxTextChangeEvent()
        {
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1006));

            //イベント発生テスト。
            //CBN_EDITCHANGEが発生すること。
            //ComboBoxはCBN_EDITUPDATEも発生すること。
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateChangeEditText("abc"); },
                CreateIgnorMessage(1006),
                new CodeInfo(1006, NativeMethods.WM_COMMAND, CBN_EDITCHANGE),
                new CodeInfo(1006, NativeMethods.WM_COMMAND, CBN_EDITUPDATE)));
            combo.EmulateChangeEditText(string.Empty);
        }

        /// <summary>
        /// ComboBoxEx32に対するEmulateChangeEditTextのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxEx32TextChangeEvent()
        {
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1007));

            //イベント発生テスト。
            //CBN_EDITCHANGEが発生すること。
            combo.EmulateSelectItem(0);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
               delegate { combo.EmulateChangeEditText("abc"); },
                //無視。
                CreateIgnorMessage(1007, new CodeInfo(1007, NativeMethods.WM_COMMAND, CBN_SELENDCANCEL)),
                //期待値。
                new CodeInfo(1007, NativeMethods.WM_COMMAND, CBN_EDITCHANGE)));
            combo.EmulateChangeEditText(string.Empty);
        }

        /// <summary>
        /// ComboBoxに対するEmulateChangeEditTextの非同期イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxTextChangeEventAsync()
        {
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1008));
            Async async = new Async();
            combo.EmulateChangeEditText("abc", async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// ComboBoxEx32に対するEmulateChangeEditTextの非同期イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxEx32TextChangeEventAsync()
        {
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1009));
            Async async = new Async();
            combo.EmulateChangeEditText("abc", async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// ComboBoxに対するEmulateChangeDropDownVisibleのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxDropDownVisibleEvent()
        {
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1006));

            //イベント発生テスト。指定の状態と現在の状態が同じ場合はイベントの通知は発生しない。
            //それ以外は開いたときにCBN_DROPDOWN、閉じたときにCBN_CLOSEUPが発生すること。
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateChangeDropDownVisible(true); },
                CreateIgnorMessage(1006),
                new CodeInfo(1006, NativeMethods.WM_COMMAND, CBN_DROPDOWN)));

            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateChangeDropDownVisible(true); },
                CreateIgnorMessage(1006),
                new CodeInfo[0]));

            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateChangeDropDownVisible(false); },
                CreateIgnorMessage(1006),
                new CodeInfo(1006, NativeMethods.WM_COMMAND, CBN_SELENDCANCEL),
                new CodeInfo(1006, NativeMethods.WM_COMMAND, CBN_CLOSEUP)));

            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateChangeDropDownVisible(false); },
                CreateIgnorMessage(1006),
                new CodeInfo[0]));
        }

        /// <summary>
        /// ComboBoxEx32に対するEmulateChangeDropDownVisibleのイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxEx32DropDownVisibleEvent()
        {
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1007));

            //イベント発生テスト。指定の状態と現在の状態が同じ場合はイベントの通知は発生しない。
            //それ以外は開いたときにCBN_DROPDOWN、閉じたときにCBN_CLOSEUPが発生すること。
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateChangeDropDownVisible(true); },
                CreateIgnorMessage(1007),
                new CodeInfo(1007, NativeMethods.WM_COMMAND, CBN_DROPDOWN)));

            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateChangeDropDownVisible(true); },
                CreateIgnorMessage(1007),
                new CodeInfo[0]));

            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { combo.EmulateChangeDropDownVisible(false); },
                CreateIgnorMessage(1007),
                new CodeInfo(1007, NativeMethods.WM_COMMAND, CBN_SELENDCANCEL), 
                new CodeInfo(1007, NativeMethods.WM_COMMAND, CBN_CLOSEUP)));

            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                  delegate { combo.EmulateChangeDropDownVisible(false); },
                CreateIgnorMessage(1007),
                new CodeInfo[0]));
        }

        /// <summary>
        /// ComboBoxに対するEmulateChangeDropDownVisibleの非同期イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxDropDownVisibleEventAsync()
        {
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1008));
            Async async = new Async();
            combo.EmulateChangeDropDownVisible(true, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
            combo.EmulateChangeDropDownVisible(false);
        }

        /// <summary>
        /// ComboBoxEx32に対するEmulateChangeDropDownVisibleの非同期イベントテスト。
        /// </summary>
        [TestMethod]
        public void TestComboBoxEx32DropDownVisibleEventAsync()
        {
            //非同期実行ができること。CBN_DROPDOWNのイベントでメッセージボックスが表示されるのでそれを閉じる。
            NativeComboBox combo = new NativeComboBox(testDlg.IdentifyFromDialogId(1009));
            Async async = new Async();
            combo.EmulateChangeDropDownVisible(true, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
            combo.EmulateChangeDropDownVisible(false);
        }

        /// <summary>
        /// FindItemのテスト
        /// </summary>
        [TestMethod]
        public void TestFindItem()
        {
            foreach (NativeComboBox combo in new NativeComboBox[]{
                new NativeComboBox(testDlg.IdentifyFromDialogId(1006)),
                new NativeComboBox(testDlg.IdentifyFromDialogId(1007))})
            {
                Assert.AreEqual(1, combo.FindItem(0, "b"));
                Assert.AreEqual(1, combo.FindItem(1, "b"));
                Assert.AreEqual(-1, combo.FindItem(2, "b"));
            }
        }

        /// <summary>
        /// ユニコードのテスト
        /// </summary>
        [TestMethod]
        public void TestUnicode()
        {
            if (!NativeMethods.IsWindowUnicode(testDlg.Handle))
            {
                return;
            }
            foreach(NativeComboBox combo in new NativeComboBox[]{
                new NativeComboBox(testDlg.IdentifyFromDialogId(1006)),
                new NativeComboBox(testDlg.IdentifyFromDialogId(1007))})
            {
                combo.EmulateChangeEditText("𩸽");
                Assert.AreEqual("𩸽", combo.Text);
                Assert.AreEqual("𩸽", combo.GetItemText(3));
                Assert.AreEqual(3, combo.FindItem(0, "𩸽"));
            }
        }
    }
}