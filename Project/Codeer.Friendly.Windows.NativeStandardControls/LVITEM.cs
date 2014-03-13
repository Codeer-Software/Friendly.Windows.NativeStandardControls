using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's LVITEM.
    /// In order to ensure the optimal size within the library when obtaining strings, does not use cchTextMax. 
    /// </summary>
#else
    /// <summary>
    /// MSDNのLVITEMを参照お願いします。
    /// 文字列取得の場合は最適なサイズをライブラリ側で確保するため、cchTextMaxは使用しません。
    /// cColumnsはpuColumns、piColFmtのサイズで表現されるため、必要ありません。
    /// puColumns、piColFmtのサイズは同一である必要があります。
    /// </summary>
#endif
    [Serializable]
    public class LVITEM
    {
        internal LVITEM_CORE _core = new LVITEM_CORE();
        string _pszText = string.Empty;

        public LVIF mask { get { return (LVIF)_core.mask; } set { _core.mask = (int)value; } }
        public int iItem { get { return _core.iItem; } set { _core.iItem = value; } }
        public int iSubItem { get { return _core.iSubItem; } set { _core.iSubItem = value; } }
        public LVIS state { get { return (LVIS)_core.state; } set { _core.state = (int)value; } }
        public LVIS stateMask { get { return (LVIS)_core.stateMask; } set { _core.stateMask = (int)value; } }
        public string pszText { get { return _pszText; } set { _pszText = value; } }
        public int iImage { get { return _core.iImage; } set { _core.iImage = value; } }
        public IntPtr lParam { get { return _core.lParam; } set { _core.lParam = value; } }
    }
    
#if ENG
    /// <summary>
    /// Please refer to MSDN's LVITEM.
    /// </summary>
#else
    /// <summary>
    /// MSDNのLVITEMを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct LVITEM_CORE
    {
        public int mask;
        public int iItem;
        public int iSubItem;
        public int state;
        public int stateMask;
        public IntPtr pszText;
        public int cchTextMax;
        public int iImage;
        public IntPtr lParam;
        //maskフィールドにより、安全とは思うが、念のためバッファのサイズを最新に合わせておく。
        int iIndent;
        int iGroupId;
        int cColumns;
        IntPtr puColumns;
        IntPtr piColFmt;
        int iGroup;
    }
}
