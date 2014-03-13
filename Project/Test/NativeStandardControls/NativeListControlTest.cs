using System;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using NativeStandardControls.TestLib;
using System.Runtime.InteropServices;

namespace NativeStandardControls
{
    /// <summary>
    /// リストコントロールテストクラス。
    /// </summary>
    [TestClass]
    public class NativeListControlTest
    {
        const int LVM_FIRST = 0x1000;
        const int LVM_SETVIEW = (LVM_FIRST + 142);
        const int LVN_FIRST = -100;
        const int LVN_ITEMCHANGING = (LVN_FIRST - 0);
        const int LVN_ITEMCHANGED = (LVN_FIRST - 1);
        const int LVN_BEGINLABELEDITA = (LVN_FIRST - 5);
        const int LVN_BEGINLABELEDITW = (LVN_FIRST - 75);
        const int LVN_ENDLABELEDITA = (LVN_FIRST - 6);
        const int LVN_ENDLABELEDITW = (LVN_FIRST - 76);
        const int EN_SETFOCUS = 0x0100;
        const int EN_CHANGE = 0x0300;
        const int EN_UPDATE = 0x0400;
        const int GWL_STYLE = -16;
        const int LVS_ICON = 0x0000;
        const int LVS_REPORT = 0x0001;
        const int LVS_SMALLICON = 0x0002;
        const int LVS_LIST = 0x0003;
        const int LVM_SETCOLUMNA = (LVM_FIRST + 26);
        const int LVM_SETCOLUMNW = (LVM_FIRST + 96);
        const int LVM_EDITLABELA = (LVM_FIRST + 23);
        const int LVM_EDITLABELW = (LVM_FIRST + 118);
        const int LVM_GETEDITCONTROL = (LVM_FIRST + 24);

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
            NativeButton buttonTest = new NativeButton(main.IdentifyFromDialogId(1042));
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
                NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
                Assert.AreEqual(100, list.ItemCount);
            }
            //ハンドルから作成。
            {
                NativeListControl list = new NativeListControl(app, testDlg.IdentifyFromDialogId(1037).Handle);
                Assert.AreEqual(100, list.ItemCount);
            }
        }

        /// <summary>
        /// ItemCountのテスト
        /// </summary>
        [TestMethod]
        public void TestItemCount()
        {
             NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
             Assert.AreEqual(100, list.ItemCount);
        }

        /// <summary>
        /// TopIndexとEnsureVisibleのテスト
        /// </summary>
        [TestMethod]
        public void TestTopIndexAndEnsureVisible()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            list.EnsureVisible(list.ItemCount - 1, false);
            Assert.AreEqual(((isUni && OSUtility.Is7or8()) ? 90 : 87), list.TopIndex);
        }

        /// <summary>
        /// Viewのテスト
        /// </summary>
        [TestMethod]
        public void TestView()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            Assert.AreEqual(View.Details, list.View);
        }

        /// <summary>
        /// EmulateSelectとSelectedIndicesのテスト
        /// </summary>
        [TestMethod]
        public void TestSelect()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            list.EmulateSelect(1, true);
            list.EmulateSelect(5, true);
            AssertEx.AreEqual(list.SelectedIndices, new int[] { 1, 5 });

            //非同期でも同様の効果があることを確認。
            Async a = new Async();
            list.EmulateSelect(5, false, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            AssertEx.AreEqual(list.SelectedIndices, new int[] { 1 });
        }

        /// <summary>
        /// EmulateChangeStateとGetItemStateのテスト
        /// </summary>
        [TestMethod]
        public void TestState()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            list.EmulateChangeItemState(3, LVIS.FOCUSED, LVIS.FOCUSED);
            Assert.AreEqual(list.GetItemState(3, LVIS.FOCUSED), LVIS.FOCUSED);

            //非同期でも同様の効果があることを確認。
            Async a = new Async();
            list.EmulateChangeItemState(5, LVIS.FOCUSED, LVIS.FOCUSED, a);
            a.WaitForCompletion();
            Assert.AreEqual(list.GetItemState(5, LVIS.FOCUSED), LVIS.FOCUSED);
        }

        /// <summary>
        /// GetItemDataのテスト
        /// </summary>
        [TestMethod]
        public void TestItemData()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            Assert.AreEqual(new IntPtr(10), list.GetItemData(10));
        }

        /// <summary>
        /// GetItemTextのテスト
        /// </summary>
        [TestMethod]
        public void TestItemText()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            Assert.AreEqual("10-1", list.GetItemText(10, 1));
        }

        /// <summary>
        /// GetItemRectとGetSubItemRectのテスト
        /// </summary>
        [TestMethod]
        public void TestItemRect()
        {
            if (!OSUtility.Is7or8())
            {
                //矩形は環境によって変わるので7のみ。しかし、7なら常に同じ矩形とも限らない。
                //このテストデータが使えるOSの設定は限られる。
                return;
            }

            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            list.EnsureVisible(list.ItemCount - 1, false);
            if (isUni)
            {
                //GetItemRect
                Assert.AreEqual(new Rectangle(4, 177, 97, 18), list.GetItemRect(list.ItemCount - 1, LVIR.LABEL));

                //GetSubItemRect
                Assert.AreEqual(new Rectangle(100, 177, 102, 18), list.GetSubItemRect(list.ItemCount - 1, 1, LVIR.LABEL));
            }
            else
            {
                //GetItemRect
                Assert.AreEqual(new Rectangle(2, 185, 99, 15), list.GetItemRect(list.ItemCount - 1, LVIR.LABEL));

                //GetSubItemRect
                Assert.AreEqual(new Rectangle(100, 185, 102, 15), list.GetSubItemRect(list.ItemCount - 1, 1, LVIR.LABEL));
            }
        }

        /// <summary>
        /// EmulateEditのテスト
        /// </summary>
        [TestMethod]
        public void TestEdit()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            list.EmulateEdit(list.ItemCount - 1, "xxx");
            Assert.AreEqual("xxx", list.GetItemText(list.ItemCount - 1, 0));

            //非同期でも同様の効果があることを確認。
            Async a = new Async();
            list.EmulateEdit(list.ItemCount - 1, "yyy", a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            Assert.AreEqual("yyy", list.GetItemText(list.ItemCount - 1, 0));
        }

        /// <summary>
        /// 編集キャンセルテスト。
        /// </summary>
        [TestMethod]
        public void TestCancelEdit()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));

            //編集状態にする。
            list.SetFocus();
            int LVM_EDITLABEL = isUni ? LVM_EDITLABELW : LVM_EDITLABELA;
            list.SetFocus();
            list.SendMessage(LVM_EDITLABEL, new IntPtr(0), IntPtr.Zero);
            new WindowControl(list.App, list.SendMessage(LVM_GETEDITCONTROL, IntPtr.Zero, IntPtr.Zero)).SetWindowText("test");

            //キャンセル。
            list.EmulateCancelEdit();
            Assert.AreEqual("0-0", list.GetItemText(0, 0));
        }

        /// <summary>
        /// GetColumnのテスト
        /// </summary>
        [TestMethod]
        public void TestGetColumn()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            LVCOLUMN_CORE setData = new LVCOLUMN_CORE();
            LVCOLUMN column = new LVCOLUMN();
            foreach (LVCF element in (LVCF[])Enum.GetValues(typeof(LVCF)))
            {
                setData.mask |= (int)element;
                column.mask |= element;
            }

            //検証データを設定
            setData.fmt = 0x4802;
            setData.cx = 30;
            setData.iSubItem = 1;
            setData.iImage = 2;
            setData.iOrder = 1;
            setData.cxMin = 20;
            setData.cxDefault = 25;
            setData.cxIdeal = 30;
            SetLVCOLUMN(list, setData, "abc");

            //取得
            list.GetColumn(1, column);
            
            //比較
            Assert.AreEqual(setData.fmt, column.fmt);
            Assert.AreEqual(setData.cx, column.cx);
            Assert.AreEqual("abc", column.pszText); 
            Assert.AreEqual(setData.iSubItem, column.iSubItem);
            Assert.AreEqual(setData.iImage, column.iImage);
            Assert.AreEqual(setData.iOrder, column.iOrder);
            if (isUni && OSUtility.Is7or8())
            {
                Assert.AreEqual(setData.cxMin, column.cxMin);
                Assert.AreEqual(setData.cxDefault, column.cxDefault);
                Assert.AreEqual(setData.cxIdeal, column.cxIdeal);
            }
        }
  
        /// <summary>
        /// GetItemのテスト
        /// </summary>
        [TestMethod]
        public void TestSetGetItem()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));

            LVITEM setData = new LVITEM();
            LVITEM getData = new LVITEM();
            foreach (LVIF element in (LVIF[])Enum.GetValues(typeof(LVIF)))
            {
                setData.mask |= element;
                getData.mask |= element;
            }
            foreach (LVIS element in (LVIS[])Enum.GetValues(typeof(LVIS)))
            {
                setData.stateMask |= element;
                getData.stateMask |= element;
            }

            //アイテムの変更
            setData.iItem = 2;
            setData.iSubItem = 0;
            setData.state = LVIS.CUT;
            setData.pszText = "abc";
            setData.iImage = 2;
            setData.lParam = new IntPtr(3);
            list.EmulateChangeItem(setData);

            //変更結果の確認
            getData.iItem = 2;
            getData.iSubItem = 0;
            list.GetItem(getData);
            Assert.AreEqual(setData.iItem, getData.iItem);
            Assert.AreEqual(setData.iSubItem, getData.iSubItem);
            Assert.AreEqual(setData.state, getData.state);
            Assert.AreEqual(setData.pszText, getData.pszText);
            Assert.AreEqual(setData.iImage, getData.iImage);
            Assert.AreEqual(setData.lParam, getData.lParam);

            //非同期でも同様の効果があることを確認。
            Async a = new Async();
            setData.pszText = "efg";
            list.EmulateChangeItem(setData, a);
            while (!a.IsCompleted)
            {
                Thread.Sleep(10);
            }
            list.GetItem(getData);
            Assert.AreEqual(setData.pszText, getData.pszText);
        }

        /// <summary>
        /// EmulateEditイベントテスト
        /// </summary>
        [TestMethod]
        public void TestEmulateEditEvent()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));

            //同期実行。LVN_BEGINLABELEDIT,LVN_ENDLABELEDITが発生していることを確認。
            int LVN_BEGINLABELEDIT = isUni ? LVN_BEGINLABELEDITW : LVN_BEGINLABELEDITA;
            int LVN_ENDLABELEDIT = isUni ? LVN_ENDLABELEDITW : LVN_ENDLABELEDITA;
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { list.EmulateEdit(3, "###"); },
                //無視
                new CodeInfo[]
                {
                    new CodeInfo(0, NativeMethods.WM_COMMAND, EN_SETFOCUS),
                    new CodeInfo(0, NativeMethods.WM_COMMAND, EN_UPDATE),
                    new CodeInfo(0, NativeMethods.WM_COMMAND, EN_CHANGE),
                    new CodeInfo(1, NativeMethods.WM_COMMAND, EN_SETFOCUS),
                    new CodeInfo(1, NativeMethods.WM_COMMAND, EN_UPDATE),
                    new CodeInfo(1, NativeMethods.WM_COMMAND, EN_CHANGE),
                    new CodeInfo(1037, NativeMethods.WM_NOTIFY, LVN_ITEMCHANGING),
                    new CodeInfo(1037, NativeMethods.WM_NOTIFY, LVN_ITEMCHANGED)
                },
                //確認
                new CodeInfo[]
                {
                    new CodeInfo(1037, NativeMethods.WM_NOTIFY, LVN_BEGINLABELEDIT),
                    new CodeInfo(1037, NativeMethods.WM_NOTIFY, LVN_ENDLABELEDIT)
                }
                ));
        }

        /// <summary>
        /// EmulateSelectイベントテスト
        /// </summary>
        [TestMethod]
        public void TestEmulateSelectEvent()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { list.EmulateSelect(2, true); },
                new CodeInfo(1037, NativeMethods.WM_NOTIFY, LVN_ITEMCHANGING),
                new CodeInfo(1037, NativeMethods.WM_NOTIFY, LVN_ITEMCHANGED)
            ));
        }

        /// <summary>
        /// EmulateChangeStateイベントテスト
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeStateEvent()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate { list.EmulateChangeItemState(2, LVIS.FOCUSED, LVIS.FOCUSED); },
                new CodeInfo(1037, NativeMethods.WM_NOTIFY, LVN_ITEMCHANGING),
                new CodeInfo(1037, NativeMethods.WM_NOTIFY, LVN_ITEMCHANGED)
            ));
        }

        /// <summary>
        /// EmulateChangeItemイベントテスト
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeItemEvent()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            Assert.IsTrue(EventChecker.IsSameTestEvent(testDlg,
                delegate
                {
                    LVITEM item = new LVITEM();
                    item.iItem = 1;
                    item.mask = LVIF.TEXT;
                    item.pszText = "zzz";
                    list.EmulateChangeItem(item); 
                },
                new CodeInfo(1037, NativeMethods.WM_NOTIFY, LVN_ITEMCHANGING),
                new CodeInfo(1037, NativeMethods.WM_NOTIFY, LVN_ITEMCHANGED)
            ));
        }

        /// <summary>
        /// EmulateEdit非同期実行イベントテスト
        /// </summary>
        [TestMethod]
        public void TestEmulateEditEventAsync()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1038));
            Async async = new Async();
            list.EmulateEdit(3, "###", async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateChangeItem非同期実行イベントテスト
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeItemEventAsync()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1038));
            LVITEM item = new LVITEM();
            item.iItem = 1;
            item.mask = LVIF.TEXT;
            item.pszText = "zzz";
            Async async = new Async();
            list.EmulateChangeItem(item, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateSelect非同期実行イベントテスト
        /// </summary>
        [TestMethod]
        public void TestEmulateSelectEventAsync()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1038));
            Async async = new Async();
            list.EmulateSelect(2, true, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// EmulateChangeState非同期実行イベントテスト
        /// </summary>
        [TestMethod]
        public void TestEmulateChangeStateAsync()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1038));
            Async async = new Async();
            list.EmulateChangeItemState(2, LVIS.FOCUSED, LVIS.FOCUSED, async);
            Assert.IsTrue(0 < MessageBoxUtility.CloseAll(testDlg, async));
        }

        /// <summary>
        /// FindItemのテスト。
        /// </summary>
        [TestMethod]
        public void TestFindItem()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            Assert.AreEqual(2, list.FindItem(0, 0, "2-0"));
            Assert.AreEqual(2, list.FindItem(2, 0, "2-0"));
            Assert.AreEqual(-1, list.FindItem(3, 0, "2-0"));
        }

        /// <summary>
        /// 編集テスト
        /// </summary>
        [TestMethod]
        public void TestGetItemTextOver256()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            StringBuilder overText = new StringBuilder();
            for (int i = 0; i < 259; i++)//ツリーの文字数最大がデフォルトで259。
            {
                overText.Append((i % 10).ToString());
            }
            list.EmulateEdit(0, overText.ToString());
            Assert.AreEqual(overText.ToString(), list.GetItemText(0, 0));
        }

        /// <summary>
        /// 編集テスト
        /// </summary>
        [TestMethod]
        public void TestGetColumnOver256()
        {
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            StringBuilder overText = new StringBuilder();
            for (int i = 0; i < 259; i++)//ツリーの文字数最大がデフォルトで259。
            {
                overText.Append((i % 10).ToString());
            }

            //設定
            LVCOLUMN_CORE setData = new LVCOLUMN_CORE();
            setData.mask = (int)LVCF.TEXT;
            setData.iSubItem = 0;
            SetLVCOLUMN(list, setData, overText.ToString());

            //取得
            LVCOLUMN getData = new LVCOLUMN();
            getData.mask = LVCF.TEXT;
            list.GetColumn(0, getData);
            Assert.AreEqual(overText.ToString(), getData.pszText);
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
            NativeListControl list = new NativeListControl(testDlg.IdentifyFromDialogId(1037));
            list.EmulateEdit(0, "𩸽");
            Assert.AreEqual("𩸽", list.GetItemText(0, 0));
        }

        /// <summary>
        /// カラム設定
        /// </summary>
        /// <param name="list">リストコントロール</param>
        /// <param name="col">カラムデータ</param>
        /// <param name="text">テキスト</param>
        private void SetLVCOLUMN(NativeListControl list, LVCOLUMN_CORE col, string text)
        {
            if (text != null)
            {
                col.pszText = isUni ? (IntPtr)app[typeof(Marshal), "StringToCoTaskMemUni"](text).Core :
                                          (IntPtr)app[typeof(Marshal), "StringToCoTaskMemAnsi"](text).Core;
            }
            int LVM_SETCOLUMN = isUni ? LVM_SETCOLUMNW : LVM_SETCOLUMNA;
            app["Codeer.Friendly.Windows.NativeStandardControls.Inside.NativeMethods.SendMessage"](list.Handle, LVM_SETCOLUMN, new IntPtr(col.iSubItem), col);
            if (text != null)
            {
                app[typeof(Marshal), "FreeCoTaskMem"](col.pszText);
            }
        }
    }
}
