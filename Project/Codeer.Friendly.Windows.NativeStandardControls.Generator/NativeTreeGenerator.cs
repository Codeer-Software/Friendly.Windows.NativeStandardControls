using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがSysTreeView32の操作をトレースしてコード生成。
    /// </summary>
    [CaptureCodeGenerator("Codeer.Friendly.Windows.NativeStandardControls.NativeTree")]
    public class NativeTreeGenerator : NativeGeneratorBase
    {
        /// <summary>
        /// 解析。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
        internal override void AnalyzeMessage(int message, IntPtr wparam, IntPtr lparam)
        {
            if (message != NativeCommonDefine.WM_NOTIFY)
            {
                return;
            }
            NMHDR data = (NMHDR)Marshal.PtrToStructure
                    (lparam, typeof(NMHDR));
            if (data.idFrom.ToInt32() != ControlId)
            {
                return;
            }

            IntPtr hItem = IntPtr.Zero;
            string path = string.Empty;
            switch (data.code)
            {
                case NativeCommonDefine.NM_CLICK:
                    {
                        hItem = GetCurrentMousePosItem();
                        path = GetNodePathCode(hItem);
                        if (!string.IsNullOrEmpty(path))
                        {
                            GenerateEmulateCheck(hItem, path);
                        }
                    }
                    break;
                case NativeTree.TVN_KEYDOWN:
                    if (GetCurrentItemAndPath(ref hItem, ref path))
                    {
                        GenerateEmulateCheck(hItem, path);
                    }
                    break;
                case NativeTree.TVN_SELCHANGEDA:
                case NativeTree.TVN_SELCHANGEDW:
                    BeginInvoke(delegate
                    {
                        if (HasFocus() && GetCurrentItemAndPath(ref hItem, ref path))
                        {
                            AddSentence(new TokenName(), ".EmulateSelectItem(" + path, new TokenAsync(CommaType.Before), ");");
                        }
                    });
                    break;
                case NativeTree.TVN_ITEMEXPANDEDA:
                case NativeTree.TVN_ITEMEXPANDEDW:
                    {
                        NMTREEVIEW info = (NMTREEVIEW)Marshal.PtrToStructure(lparam, typeof(NMTREEVIEW));
                        BeginInvoke(delegate
                        {
                            if (HasFocus())
                            {
                                path = GetNodePathCode(info.itemNew.hItem);
                                if (!string.IsNullOrEmpty(path))
                                {
                                    AddSentence(new TokenName(), path + ".EmulateExpand(" +
                                        NativeTree.IsExpandedInTarget(WindowHandle, info.itemNew.hItem).ToString().ToLower(), new TokenAsync(CommaType.Before), ");");
                                }
                            }
                        });
                    }
                    break;
                case NativeTree.TVN_ENDLABELEDITA:
                case NativeTree.TVN_ENDLABELEDITW:
                    {
                        bool isUni = NativeMethods.IsWindowUnicode(WindowHandle);
                        NMTVDISPINFO info = (NMTVDISPINFO)Marshal.PtrToStructure(lparam, typeof(NMTVDISPINFO));
                        path = GetNodePathCode(info.item.hItem);
                        if (!string.IsNullOrEmpty(path))
                        {
                            string text = isUni ? Marshal.PtrToStringUni(info.item.pszText) :
                                     Marshal.PtrToStringAnsi(info.item.pszText);
                            text = NativeEditGenerator.AdjustText(text);
                            AddSentence(new TokenName(), path + ".EmulateEdit(" + text, new TokenAsync(CommaType.Before), ");");
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// EmulateCheckのコード生成
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <param name="path">アイテムまでのパス。</param>
        private void GenerateEmulateCheck(IntPtr hItem, string path)
        {
            bool isChecked = NativeTree.IsCheckedInTarget(WindowHandle, hItem);
            BeginInvoke(delegate
            {
                bool isCurChecked = NativeTree.IsCheckedInTarget(WindowHandle, hItem);
                if (isChecked != isCurChecked)
                {
                    AddSentence(new TokenName(), path + ".EmulateCheck(" + isCurChecked.ToString().ToLower(), new TokenAsync(CommaType.Before), ");");
                }
            });
        }

        /// <summary>
        /// 現在のマウス選択位置のアイテムを取得。
        /// </summary>
        /// <returns>現在のマウス選択位置のアイテム。</returns>
        private IntPtr GetCurrentMousePosItem()
        {
            TVHITTESTINFO info = new TVHITTESTINFO();
            info.pt.X = Control.MousePosition.X;
            info.pt.Y = Control.MousePosition.Y;
            NativeMethods.ScreenToClient(WindowHandle, ref info.pt);
            return NativeMethods.SendMessage(WindowHandle, NativeTree.TVM_HITTEST, IntPtr.Zero, ref info);
        }

        /// <summary>
        /// 現在の選択アイテムとそこまでパスを取得。
        /// </summary>
        /// <param name="hItem">アイテム。</param>
        /// <param name="path">パス。</param>
        /// <returns>成否。</returns>
        private bool GetCurrentItemAndPath(ref IntPtr hItem, ref string path)
        {
            hItem = NativeMethods.SendMessage(WindowHandle, NativeTree.TVM_GETNEXTITEM, new IntPtr(NativeTree.TVGN_CARET), IntPtr.Zero);
            path = GetNodePathCode(hItem);
            return !string.IsNullOrEmpty(path);
        }

        /// <summary>
        /// 指定のノードまでのパスコードを取得。
        /// </summary>
        /// <param name="hItem">アイテムハンドル。</param>
        /// <returns>指定のパスコード。</returns>
        private string GetNodePathCode(IntPtr hItem)
        {
            string path = string.Empty;
            while (hItem != IntPtr.Zero)
            {
                string text = NativeTree.GetItemTextInTarget(WindowHandle, hItem);
                if (!string.IsNullOrEmpty(path))
                {
                    path = ", " + path;
                }
                path = NativeEditGenerator.AdjustText(text) + path;
                hItem = NativeMethods.SendMessage(WindowHandle, NativeTree.TVM_GETNEXTITEM, new IntPtr(NativeTree.TVGN_PARENT), hItem);
            }
            return string.IsNullOrEmpty(path) ? string.Empty : Name + ".FindNodeObj(" + path + ")";
        }
    }
}
