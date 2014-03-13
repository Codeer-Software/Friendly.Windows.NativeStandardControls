using System;
using System.Collections.Generic;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;
using Codeer.TestAssistant.GeneratorToolKit;

namespace Codeer.Friendly.Windows.NativeStandardControls.Generator
{
    /// <summary>
    /// WindowClassがListBoxの操作をトレースしてコード生成。
    /// </summary>
    public class NativeListBoxGenerator : NativeGeneratorBase
    {
        int[] _lastSelected;
        int _lastCurrentSelected;
        bool _isSingleSelect;

        /// <summary>
        /// アタッチ
        /// </summary>
        protected override void Attach()
        {
            base.Attach();
            _lastCurrentSelected = NativeListBox.GetCurrentSelectedIndexInTarget(WindowHandle);
            _lastSelected = NativeListBox.GetSelectedIndicesInTarget(WindowHandle);
            _isSingleSelect = (((int)NativeMethods.GetWindowLongPtr(WindowHandle, NativeCommonDefine.GWL_STYLE) & NativeListBox.LVS_SINGLESEL) != 0);
        }

        /// <summary>
        /// 解析。
        /// </summary>
        /// <param name="message">メッセージ。</param>
        /// <param name="wparam">wparam。</param>
        /// <param name="lparam">lparam。</param>
        internal override void AnalyzeMessage(int message, IntPtr wparam, IntPtr lparam)
        {
            if (HasFocus() &&
                message == NativeCommonDefine.WM_COMMAND &&
                ControlId == (wparam.ToInt32() & 0xFFFF) &&
                ((int)(wparam.ToInt32() >> 16) & 0xFFFF) == NativeListBox.LBN_SELCHANGE)
            {
                OnSelected();
            }
        }

        /// <summary>
        /// 選択変更時にコードを生成する。
        /// </summary>
        private void OnSelected()
        {
            int[] currentSelected = NativeListBox.GetSelectedIndicesInTarget(WindowHandle);
            int currentIndex = NativeListBox.GetCurrentSelectedIndexInTarget(WindowHandle);
            if (_isSingleSelect)
            {
                AddSentence(new TokenName(), ".EmulateChangeCurrentSelectedIndex(" + currentIndex, new TokenAsync(CommaType.Before), ");");
            }
            else
            {
                List<int> newList = new List<int>(currentSelected);
                foreach (int index in _lastSelected)
                {
                    if (newList.IndexOf(index) == -1)
                    {
                        AddSentence(new TokenName(), ".EmulateChangeSelect(" + index + ", false", new TokenAsync(CommaType.Before), ");");
                    }
                }
                List<int> oldList = new List<int>(_lastSelected);
                foreach (int index in currentSelected)
                {
                    if (oldList.IndexOf(index) == -1)
                    {
                        AddSentence(new TokenName(), ".EmulateChangeSelect(" + index + ", true", new TokenAsync(CommaType.Before), ");");
                    }
                }
                if (_lastCurrentSelected != currentIndex)
                {
                    AddSentence(new TokenName(), ".EmulateChangeCurrentSelectedIndex(" + currentIndex, new TokenAsync(CommaType.Before), ");");
                }
            }
            _lastCurrentSelected = currentIndex;
            _lastSelected = currentSelected;
        }
    }
}
