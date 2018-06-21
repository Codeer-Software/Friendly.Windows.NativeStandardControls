using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Codeer.Friendly.Windows;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls;
using NativeStandardControls.TestLib;
using Codeer.Friendly.Windows.KeyMouse;
using System.Drawing;
using System;
using System.Runtime.InteropServices;
using Codeer.Friendly.Dynamic;
using System.Windows.Forms;

namespace NativeStandardControls
{
    [TestClass]
    public class NativeMenuTest
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
            Process.GetProcessById(app.ProcessId).Kill();
        }

        [TestMethod]
        public void TestGetMenuItems()
        {
            var items = NativeMenuItem.GetMenuItems(main);

        //    var xxx = NativeMenuItem.GetMenuItems(main.Handle, (IntPtr)main.App["Codeer.Friendly.Windows.NativeStandardControls.Inside.NativeMethods.GetMenu"](main.Handle).Core);

            Assert.AreEqual(items.Length, 2);
            Assert.AreEqual(items[0].Text, "A0");
            Assert.AreEqual(items[1].Text, "B0");

            var b0 = NativeMenuItem.GetMenuItem(main, "B0");
            b0.Click();

            var popupItems = NativeMenuItem.GetPopupMenuItems(app);
            Assert.AreEqual(popupItems.Length, 3);
            Assert.AreEqual(popupItems[0].Text, "B0-0");
            Assert.AreEqual(popupItems[1].Text, "B0-1");
            Assert.AreEqual(popupItems[2].Text, "B0-2");


            var window = WindowControl.FromZTop(app);
            var x = window.SendMessage(0x01E1, IntPtr.Zero, IntPtr.Zero);
        //    NativeMenuItem.GetMenuItems(window.Handle, x);

        }

        [TestMethod]
        public void TestClickMenu()
        {
            var b0 = NativeMenuItem.GetMenuItem(main, "B0");
            b0.Click();

            var b01 = NativeMenuItem.GetPopupMenuItem(app, "B0-1");
            b01.Click();

            var b011 = NativeMenuItem.GetPopupMenuItem(app, "B0-1-1");
            b011.Click();

            var b0112 = NativeMenuItem.GetPopupMenuItem(app, "B0-1-1-2");
            Assert.IsFalse(b0112.Enabled);

            var b0111 = NativeMenuItem.GetPopupMenuItem(app, "B0-1-1-1𩸽");
            Assert.IsTrue(b0111.Enabled);
            const int ID_B0_B7 = 32778;
            Assert.AreEqual(b0111.Id, ID_B0_B7);
            Assert.AreEqual(b0111.Text, "B0-1-1-1𩸽");
            b0111.Click();

            var msg = new NativeMessageBox(app.FromZTop());
            Assert.AreEqual(msg.Message, "AAA");
            msg.Window.Close();
        }

        [TestMethod]
        public void TesClickMenuIndex()
        {
            var b0 = NativeMenuItem.GetMenuItem(main, 1);
            b0.Click();

            var b01 = NativeMenuItem.GetPopupMenuItem(app, 1);
            b01.Click();

            var b011 = NativeMenuItem.GetPopupMenuItem(app, 1);
            b011.Click();

            var b0112 = NativeMenuItem.GetPopupMenuItem(app, 2);
            Assert.IsFalse(b0112.Enabled);

            var b0111 = NativeMenuItem.GetPopupMenuItem(app, 1);
            Assert.IsTrue(b0111.Enabled);
            b0111.Click();

            var msg = new NativeMessageBox(app.FromZTop());
            Assert.AreEqual(msg.Message, "AAA");
            msg.Window.Close();
        }
    }
}
