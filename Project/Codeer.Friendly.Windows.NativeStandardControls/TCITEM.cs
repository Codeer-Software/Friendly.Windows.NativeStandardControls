using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's TCITEM.
    /// In order to ensure the optimal size within the library when obtaining strings, does not use cchTextMax. 
    /// </summary>
#else
    /// <summary>
    /// MSDNのTCITEMを参照お願いします。
    /// 文字列取得の場合は最適なサイズをライブラリ側で確保するため、cchTextMaxは使用しません。
    /// </summary>
#endif
    [Serializable]
    public class TCITEM
    {
        internal TCITEM_CORE _core = new TCITEM_CORE();
        internal string _pszText = string.Empty;

        public TCIF mask { get { return (TCIF)_core.mask; } set { _core.mask = (int)value; } }
        public TCIS dwState { get { return (TCIS)_core.dwState; } set { _core.dwState = (int)value; } }
        public TCIS dwStateMask { get { return (TCIS)_core.dwStateMask; } set { _core.dwStateMask = (int)value; } }
        public string pszText { get { return _pszText; } set { _pszText = value; } }
        public int iImage { get { return _core.iImage; } set { _core.iImage = value; } }
        public IntPtr lParam { get { return _core.lParam; } set { _core.lParam = value; } }
    }
    
#if ENG
    /// <summary>
    /// Please refer to MSDN's TCITEM.
    /// </summary>
#else
    /// <summary>
    /// MSDNのTCITEMを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct TCITEM_CORE
    {
        public int mask;
        public int dwState;
        public int dwStateMask;
        public IntPtr pszText;
        public int cchTextMax;
        public int iImage;
        public IntPtr lParam;
    }
}
