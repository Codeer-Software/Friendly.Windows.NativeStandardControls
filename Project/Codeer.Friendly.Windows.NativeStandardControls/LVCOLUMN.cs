using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's LVCOLUMN.
    /// In order to obtain the optimal size within the library when obtaining strings, does not use cchTextMax. 
    /// </summary>
#else
    /// <summary>
    /// MSDNのLVCOLUMNを参照お願いします。
    /// 文字列取得の場合は最適なサイズをライブラリ側で確保するため、cchTextMaxは使用しません。
    /// </summary>
#endif
    [Serializable]
    public class LVCOLUMN
    {
        internal LVCOLUMN_CORE _core = new LVCOLUMN_CORE();
        internal string _pszText = string.Empty;

        public LVCF mask { get { return (LVCF)_core.mask; } set { _core.mask = (int)value; } }
        public int fmt { get { return _core.fmt; } set { _core.fmt = value; } }
        public int cx { get { return _core.cx; } set { _core.cx = value; } }
        public string pszText { get { return _pszText; } set { _pszText = value; } }
        public int iSubItem { get { return _core.iSubItem; } set { _core.iSubItem = value; } }
        public int iImage { get { return _core.iImage; } set { _core.iImage = value; } }
        public int iOrder { get { return _core.iOrder; } set { _core.iOrder = value; } }
        public int cxMin { get { return _core.cxMin; } set { _core.cxMin = value; } }
        public int cxDefault { get { return _core.cxDefault; } set { _core.cxDefault = value; } }
        public int cxIdeal { get { return _core.cxIdeal; } set { _core.cxIdeal = value; } }
    }

#if ENG
    /// <summary>
    /// Please refer to MSDN's LVCOLUMN.
    /// </summary>
#else
    /// <summary>
    /// MSDNのLVCOLUMNを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct LVCOLUMN_CORE
    {
        public int mask;
        public int fmt;
        public int cx;
        public IntPtr pszText;
        public int cchTextMax;
        public int iSubItem;
        public int iImage;
        public int iOrder;
        public int cxMin;
        public int cxDefault;
        public int cxIdeal;
    }
}
