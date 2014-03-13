using System;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using NativeStandardControls.TestLib;
using System.Text;

namespace NativeStandardControls
{
    /// <summary>
    /// ツリーテストクラス。
    /// </summary>
    [TestClass]
    public class NativeTreeTest
    {
        const int EN_SETFOCUS = 0x0100;
        const int EN_CHANGE = 0x0300;
        const int EN_UPDATE = 0x0400;
        const int TVN_FIRST = -400;
        const int TVN_BEGINLABELEDITA = (TVN_FIRST - 10);
        const int TVN_BEGINLABELEDITW = (TVN_FIRST - 59);
        const int TVN_ENDLABELEDITA = (TVN_FIRST - 11);
        const int TVN_ENDLABELEDITW = (TVN_FIRST - 60);
        const int TVN_ITEMCHANGINGA = (TVN_FIRST - 16);
        const int TVN_ITEMCHANGINGW = (TVN_FIRST - 17);
        const int TVN_ITEMCHANGEDA = (TVN_FIRST - 18);
        const int TVN_ITEMCHANGEDW = (TVN_FIRST - 19);
        const int TVN_ASYNCDRAW = (TVN_FIRST - 20);
        const int TVN_SELCHANGINGA = (TVN_FIRST - 1);
        const int TVN_SELCHANGINGW = (TVN_FIRST - 50);
        const int TVN_SELCHANGEDA = (TVN_FIRST - 2);
        const int TVN_SELCHANGEDW = (TVN_FIRST - 51);
        const int TVN_ITEMEXPANDINGA = (TVN_FIRST - 5);
        const int TVN_ITEMEXPANDINGW = (TVN_FIRST - 54);
        const int TVN_ITEMEXPANDEDA = (TVN_FIRST - 6);
        const int TVN_ITEMEXPANDEDW = (TVN_FIRST - 55);
        const int TV_FIRST = 0x1100;
        const int TVM_EDITLABELA = (TV_FIRST + 14);
        const int TVM_EDITLABELW = (TV_FIRST + 65);
        const int TVM_GETEDITCONTROL = (TV_FIRST + 15);
        const int TVE_EXPAND = 0x0002;
        int TVN_BEGINLABELEDIT { get { return isUni ? TVN_BEGINLABELEDITW : TVN_BEGINLABELEDITA; } }
        int TVN_ENDLABELEDIT { get { return isUni ? TVN_ENDLABELEDITW : TVN_ENDLABELEDITA; } }
        int TVN_ITEMCHANGING { get { return isUni ? TVN_ITEMCHANGINGW : TVN_ITEMCHANGINGA; } }
        int TVN_ITEMCHANGED { get { return isUni ? TVN_ITEMCHANGEDW : TVN_ITEMCHANGEDA; } }
        int TVN_SELCHANGING { get { return isUni ? TVN_SELCHANGINGW : TVN_SELCHANGINGA; } }
        int TVN_SELCHANGED { get { return isUni ? TVN_SELCHANGEDW : TVN_SELCHANGEDA; } }
        int TVN_ITEMEXPANDING { get { return isUni ? TVN_ITEMEXPANDINGW : TVN_ITEMEXPANDINGA; } }
        int TVN_ITEMEXPANDED { get { return isUni ? TVN_ITEMEXPANDEDW : TVN_ITEMEXPANDEDA; } }

        WindowsAppFriend app;
        WindowControl testDlg;
        bool isUni;

        /// <summary>
        /// 初期化。
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            app = new WindowsAppFriend(Process.Start(TargetPath.NativeControls));
            EventChecker.Initialize(app);
            WindowControl main = WindowControl.FromZTop(app);
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1043));
            buttonTest.EmulateClick(new Async());
            testDlg = main.WaitForNextModal();
            isUni = NativeMethods.IsWindowUnicode(testDlg.Handle);
        }

        /// <summary>
        /// 終了処理。
        /// </summary>
        [TestCleanup]
        public void TearDown()
        {
            if (app != null)
            {
                //閉じるときにイベント発生でメッセージボックスが出ないようにする
                app[typeof(EventChecker), "ClearAsyncMsgBox"](testDlg.Handle);
                new NativeButton(testDlg.IdentifyFromWindowText("OK")).EmulateClick();
                MessageBoxUtility.CloseAll(testDlg);
                Process process = Process.GetProcessById(app.ProcessId);
                app.Dispose();
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
                NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
                Assert.AreEqual("0", tree.GetItemText(tree.Nodes[0]));
            }
            //ハンドルから作成。
            {
                NativeTree tree = new NativeTree(app, testDlg.IdentifyFromDialogId(1041).Handle);
                Assert.AreEqual("0", tree.GetItemText(tree.Nodes[0]));
            }
        }

        /// <summary>
        /// GetItemTextのテスト。
        /// </summary>
        [TestMethod]
        public void TestGetItemText()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            Assert.AreEqual("0", tree.GetItemText(tree.Nodes[0]));
        }

        /// <summary>
        /// Nodesのテスト。
        /// </summary>
        [TestMethod]
        public void TestNodes()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            IntPtr[] nodes = tree.Nodes;
            Assert.AreEqual(2, nodes.Length);
            Assert.AreEqual("0", tree.GetItemText(nodes[0]));
            Assert.AreEqual("10", tree.GetItemText(nodes[1]));
        }

        /// <summary>
        /// EmulateSelectItemとSelectedItemのテスト。
        /// </summary>
        [TestMethod]
        public void TestSelectedItem()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            tree.EmulateSelectItem(tree.FindNode(0, 1));
            Assert.AreEqual("2", tree.GetItemText(tree.SelectedItem));

            //非同期でも同様の効果があることの確認。
            Async a = new Async();
            tree.EmulateSelectItem(tree.FindNode(1, 0), a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.AreEqual("0", tree.GetItemText(tree.SelectedItem));
        }

        /// <summary>
        /// FindNodeのテスト。
        /// </summary>
        [TestMethod]
        public void TestFindNode()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            Assert.AreEqual("2", tree.GetItemText(tree.FindNode(0, 1)));
            Assert.AreEqual("1", tree.GetItemText(tree.FindNode("0", "1")));
        }

        /// <summary>
        /// GetBrotherNodesのテスト。
        /// </summary>
        [TestMethod]
        public void TestGetBrotherNodes()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            AssertEx.AreEqual(tree.Nodes, tree.GetBrotherNodes(tree.Nodes[0]));
            IntPtr[] brother = tree.GetBrotherNodes(tree.FindNode(0, 0));
            Assert.AreEqual("1", tree.GetItemText(brother[0]));
            Assert.AreEqual("2", tree.GetItemText(brother[1]));

            //途中から取得してもすべて列挙されること
            brother = tree.GetBrotherNodes(tree.FindNode(0, 1));
            Assert.AreEqual("1", tree.GetItemText(brother[0]));
            Assert.AreEqual("2", tree.GetItemText(brother[1]));
        }

        /// <summary>
        /// GetChildNodesのテスト。
        /// </summary>
        [TestMethod]
        public void TestGetChildNodes()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            IntPtr[] children = tree.GetChildNodes(tree.Nodes[0]);
            Assert.AreEqual("1", tree.GetItemText(children[0]));
            Assert.AreEqual("2", tree.GetItemText(children[1]));
        }

        /// <summary>
        /// GetParentNodeのテスト。
        /// </summary>
        [TestMethod]
        public void TestGetParentNode()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            IntPtr[] children = tree.GetChildNodes(tree.Nodes[0]);
            Assert.AreEqual("0", tree.GetItemText(tree.GetParentNode(children[0])));
        }

        /// <summary>
        /// GetItemDataのテスト。
        /// </summary>
        [TestMethod]
        public void TestGetItemData()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            Assert.AreEqual(new IntPtr(100), tree.GetItemData(tree.Nodes[0]));
        }

        /// <summary>
        /// GetItemRectのテスト。
        /// </summary>
        [TestMethod]
        public void TestGetItemRect()
        {
            if (!OSUtility.Is7or8())
            {
                //矩形は環境によって変わるので7のみ。しかし、7なら常に同じ矩形とも限らない。
                //このテストデータが使えるOSの設定は限られる。
                return;
            }
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            Assert.AreEqual(new  Rectangle(0, 0, 128, 17), tree.GetItemRect(tree.Nodes[0], false));
            Assert.AreEqual(new Rectangle(35, 0, 11, 17), tree.GetItemRect(tree.Nodes[0], true));
        }

        /// <summary>
        /// EmulateExpandとIsExpandedのテスト。
        /// </summary>
        [TestMethod]
        public void TestExpanded()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            tree.EmulateExpand(tree.Nodes[0], true);
            Assert.IsTrue(tree.IsExpanded(tree.Nodes[0]));
            tree.EmulateExpand(tree.Nodes[0], false);
            Assert.IsFalse(tree.IsExpanded(tree.Nodes[0]));

            //非同期でも同様の効果があることの確認。
            Async a = new Async();
            tree.EmulateExpand(tree.Nodes[0], true, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.IsTrue(tree.IsExpanded(tree.Nodes[0]));
        }

        /// <summary>
        /// EmulateCheckとIsCheckedのテスト。
        /// </summary>
        [TestMethod]
        public void TestChecked()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            tree.EmulateCheck(tree.Nodes[0], true);
            Assert.IsTrue(tree.IsChecked(tree.Nodes[0]));
            tree.EmulateCheck(tree.Nodes[0], false);
            Assert.IsFalse(tree.IsChecked(tree.Nodes[0]));

            //非同期でも同様の効果があることの確認。
            Async a = new Async();
            tree.EmulateCheck(tree.Nodes[0], true, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.IsTrue(tree.IsChecked(tree.Nodes[0]));
        }

        /// <summary>
        /// SetItemとGetItemのテスト。
        /// </summary>
        [TestMethod]
        public void TestSetGetItem()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            TVITEMEX item = new TVITEMEX();
            foreach (TVIF element in (TVIF[])Enum.GetValues(typeof(TVIF)))
            {
                item.mask |= element;
            }
            foreach (TVIS element in (TVIS[])Enum.GetValues(typeof(TVIS)))
            {
                item.stateMask |= element;
            }

            //アイテムのすべての情報が正常に取得できることを確認。
            item.hItem = tree.Nodes[0];
            tree.GetItem(item);
            Assert.AreEqual("0", item.pszText);
            Assert.AreEqual(0, item.iImage);
            Assert.AreEqual(4, item.iSelectedImage);
            Assert.AreEqual(1, item.cChildren);
            Assert.AreEqual(new IntPtr(100), item.lParam);
            Assert.AreEqual((TVIS)0, (item.state & TVIS.EXPANDED));
            tree.EmulateExpand(item.hItem, true);
            tree.GetItem(item);
            Assert.AreEqual(TVIS.EXPANDED, (item.state & TVIS.EXPANDED));
            item.hItem = tree.FindNode(0, 0); //iImageは上記では0確認であるため、念のため、値の入ったアイテムでもテスト。
            tree.GetItem(item);
            Assert.AreEqual(1, item.iImage);

            //全ての情報情報を設定できることを確認。
            item.hItem = tree.Nodes[0];
            tree.GetItem(item);//もとに戻す用
            TVITEMEX newItem = new TVITEMEX();
            newItem.hItem = item.hItem;
            newItem.mask = item.mask;
            newItem.pszText = "xxx";
            newItem.iImage = 1;
            newItem.iSelectedImage = 2;
            newItem.cChildren = 0;
            newItem.lParam = new IntPtr(200);
            newItem.stateMask = TVIS.SELECTED;
            newItem.state = TVIS.SELECTED;
            tree.EmulateChangeItem(newItem);

            TVITEMEX check = new TVITEMEX();
            check.hItem = item.hItem;
            check.mask = item.mask;
            check.stateMask = item.stateMask;
            tree.GetItem(check);
            Assert.AreEqual("xxx", check.pszText);
            Assert.AreEqual(1, check.iImage);
            Assert.AreEqual(2, check.iSelectedImage);
            Assert.AreEqual(0, check.cChildren);
            Assert.AreEqual(new IntPtr(200), check.lParam);
            Assert.AreEqual(TVIS.SELECTED, check.state & TVIS.SELECTED);
        }

        /// <summary>
        /// 編集テスト
        /// </summary>
        [TestMethod]
        public void TestGetItemTextOver256()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            StringBuilder overText = new StringBuilder();
            for (int i = 0; i < 259; i++)//ツリーの文字数最大がデフォルトで259。
            {
                overText.Append((i % 10).ToString());
            }
            tree.EmulateEdit(tree.Nodes[0], overText.ToString());
            Assert.AreEqual(overText.ToString(), tree.GetItemText(tree.Nodes[0]));
        }

        /// <summary>
        /// EnsureVisibleのテスト
        /// </summary>
        [TestMethod]
        public void TestEnsureVisible()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            tree.EmulateExpand(tree.Nodes[1], true);
            IntPtr node = tree.FindNode(1, 99);
            RECT native = new RECT();
            NativeMethods.GetWindowRect(tree.Handle, out native);

            Rectangle rect = new Rectangle(0, 0, native.Right - native.Left + 1, native.Bottom - native.Top + 1);
            Assert.IsFalse(rect.Contains(tree.GetItemRect(node, false)));
            Assert.IsTrue(tree.EnsureVisible(node));
            Assert.IsTrue(rect.Contains(tree.GetItemRect(node, false)));
        }

        /// <summary>
        /// 編集テスト
        /// </summary>
        [TestMethod]
        public void TestEdit()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            tree.EmulateEdit(tree.Nodes[0], "test");
            Assert.AreEqual("test", tree.GetItemText(tree.Nodes[0]));

            //非同期でも同様の効果があることの確認。
            Async a = new Async();
            tree.EmulateEdit(tree.Nodes[0], "test2", a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.AreEqual("test2", tree.GetItemText(tree.Nodes[0]));
        }

        /// <summary>
        /// 編集キャンセルテスト。
        /// </summary>
        [TestMethod]
        public void TestCancelEdit()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));

            //編集状態にする。
            NativeMethods.SetFocus(tree.Handle);
            int TVM_EDITLABEL = isUni ? TVM_EDITLABELW : TVM_EDITLABELA;
            tree.SendMessage(TVM_EDITLABEL, IntPtr.Zero, tree.Nodes[0]);
            new WindowControl(tree.App, tree.SendMessage(TVM_GETEDITCONTROL, IntPtr.Zero, IntPtr.Zero)).SetWindowText("test");

            //キャンセル。
            tree.EmulateCancelEdit();
            Assert.AreEqual("0", tree.GetItemText(tree.Nodes[0]));
        }

        /// <summary>
        /// EmulateEditイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateEditEvent()
        {
            //編集開始　TVN_BEGINLABELEDITが通知されること
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { tree.EmulateEdit(tree.Nodes[0], "xxx"); },
                //無視
                new CodeInfo[] {
                    new CodeInfo(0, NativeMethods.WM_COMMAND, EN_SETFOCUS),
                    new CodeInfo(0, NativeMethods.WM_COMMAND, EN_UPDATE),
                    new CodeInfo(1, NativeMethods.WM_COMMAND, EN_CHANGE),
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_SELCHANGING),
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGING),
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGED),
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_SELCHANGED)
                },
                //確認
                new CodeInfo[] {
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_BEGINLABELEDIT),
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ENDLABELEDIT)
                }));
        }

        /// <summary>
        /// EmulateExpandイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateExpandEvent()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));

            //経験から発見されたTreeの性質として、一度開いた後はExpnadメッセージで後の通知が発生しない。
            //そのため、別途通知を投げている。その確認。
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { tree.EmulateExpand(tree.Nodes[1], true); },
                new CodeInfo[] { new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_SELCHANGING),
                                 new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_SELCHANGED),
                                 new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGING),
                                 new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGED)},
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMEXPANDING),
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMEXPANDED)
                ));
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { tree.EmulateExpand(tree.Nodes[1], false); },
                new CodeInfo[] { new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_SELCHANGING),
                                 new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_SELCHANGED),
                                 new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGING),
                                 new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGED)},
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMEXPANDING),
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMEXPANDED)
                ));
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { tree.EmulateExpand(tree.Nodes[1], true); },
                new CodeInfo[] { new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_SELCHANGING),
                                 new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_SELCHANGED),
                                 new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGING),
                                 new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGED)},
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMEXPANDING),
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMEXPANDED)
                ));

            //詳細な通知内容の確認。
            NMTREEVIEW[] expectation = new NMTREEVIEW[2];
            expectation[0].action = expectation[1].action = 1;
            expectation[0].itemNew.cChildren = expectation[1].itemNew.cChildren = 0;
            expectation[0].itemNew.cchTextMax = expectation[1].itemNew.cchTextMax = 0;
            expectation[0].itemNew.hItem = expectation[1].itemNew.hItem = tree.Nodes[1];
            expectation[0].itemNew.iImage = expectation[1].itemNew.iImage = 3;
            expectation[0].itemNew.iSelectedImage = expectation[1].itemNew.iSelectedImage = 4;
            expectation[0].itemNew.lParam = expectation[1].itemNew.lParam = new IntPtr(101);
            expectation[0].itemNew.mask = expectation[1].itemNew.mask = (int)(TVIF.IMAGE | TVIF.PARAM | TVIF.STATE | TVIF.HANDLE | TVIF.SELECTEDIMAGE);
            expectation[0].itemNew.pszText = expectation[1].itemNew.pszText = IntPtr.Zero;
            expectation[0].itemNew.state = 0x1060;
            expectation[1].itemNew.state = 0x1040;
            expectation[0].itemNew.stateMask = expectation[1].itemNew.stateMask = 0xffff;
            Assert.IsTrue(EventChecker.CheckNotifyDetail(testDlg,
                delegate { tree.EmulateExpand(tree.Nodes[1], false); },
                expectation));
            
        }

        /// <summary>
        /// EmulateSelectItemイベントテスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateSelectItemEvent()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            tree.EmulateSelectItem(tree.Nodes[0]);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { tree.EmulateSelectItem(tree.Nodes[1]); },
                //無視
                new CodeInfo[] {
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGING),
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGED),
                },
                //確認
                new CodeInfo[] { 
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_SELCHANGING),
                    new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_SELCHANGED)
                }
            ));
        }

        /// <summary>
        /// イベント確認テスト
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeItemEvent()
        {
            if (!isUni || !OSUtility.Is7or8())
            {
                //設定によってはこのイベントは発生しない。
                return;
            }

            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));

            TVITEMEX itemOri = new TVITEMEX();
            itemOri.hItem = tree.Nodes[0];
            tree.GetItem(itemOri);
            TVITEMEX newItem = new TVITEMEX();
            newItem.hItem = itemOri.hItem;
            newItem.mask = itemOri.mask = TVIF.STATE;
            newItem.stateMask = TVIS.SELECTED;
            newItem.state = (TVIS)0;

            //一度明示的に選択をOFFにする
            tree.EmulateChangeItem(newItem);

            //変更
            newItem.state = TVIS.SELECTED;
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
            delegate { tree.EmulateChangeItem(newItem); },
                new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGING),
                new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGED)
            ));
        }

        /// <summary>
        /// イベント確認テスト
        /// </summary>
        [TestMethod]
        public void TestEmulateCheckEvent()
        {
            if (!isUni || !OSUtility.Is7or8())
            {
                //設定によってはこのイベントは発生しない。
                return;
            }

            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            tree.EmulateCheck(tree.Nodes[0], false);
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
            delegate { tree.EmulateCheck(tree.Nodes[0], true); },
                new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGING),
                new CodeInfo(1041, NativeMethods.WM_NOTIFY, TVN_ITEMCHANGED)
            ));
            tree.EmulateCheck(tree.Nodes[0], false);
        }

        /// <summary>
        /// TestEmulateChangeItemEventイベント非同期実行テスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeItemEventAsync()
        {
            if (!isUni || !OSUtility.Is7or8())
            {
                //設定によってはこのイベントは発生しない。
                return;
            }

            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1042));
            TVITEMEX newItem = new TVITEMEX();
            newItem.hItem = tree.Nodes[0];
            newItem.mask = TVIF.STATE;
            newItem.stateMask = TVIS.SELECTED;
            newItem.state = TVIS.SELECTED;

            Async async = new Async();
            tree.EmulateChangeItem(newItem, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateCheckイベント非同期実行テスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateCheckEventAsync()
        {
            if (!isUni || !OSUtility.Is7or8())
            {
                //設定によってはこのイベントは発生しない。
                return;
            }

            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1042));
            Async async = new Async();
            tree.EmulateCheck(tree.Nodes[0], true, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateEditイベント非同期実行テスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateEditEventAsync()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1042));
            Async async = new Async();
            tree.EmulateEdit(tree.Nodes[0], "ttt", async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateExpandイベント非同期実行テスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateExpandEventAsync()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1042));
            Async async = new Async();
            tree.EmulateExpand(tree.Nodes[0], true, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateSelectItemイベント非同期実行テスト。
        /// </summary>
        [TestMethod]
        public void TestEmulateSelectItemEventAsync()
        {
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1042));
            Async async = new Async();
            tree.EmulateSelectItem(tree.FindNode(0, 0), async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// ユニコードのテスト
        /// </summary>
        [TestMethod]
        public void TestUniCode()
        {
            if (!isUni)
            {
                return;
            }
            NativeTree tree = new NativeTree(testDlg.IdentifyFromDialogId(1041));
            tree.EmulateEdit(tree.Nodes[0], "𩸽");
            Assert.AreEqual("𩸽", tree.GetItemText(tree.Nodes[0]));
        }
    }
}
