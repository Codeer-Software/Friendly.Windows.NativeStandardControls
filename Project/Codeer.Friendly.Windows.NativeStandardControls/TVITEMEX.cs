using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Please refer to MSDN's TVITEM.
    /// In order to ensure the optimal size within the library when obtaining strings, does not use cchTextMax. 
    /// </summary>
#else
    /// <summary>
    /// MSDNのTVITEMを参照お願いします。
    /// 文字列取得の場合は最適なサイズをライブラリ側で確保するため、cchTextMaxは使用しません。
    /// </summary>
#endif
    [Serializable]
    public class TVITEMEX
    {
        internal TVITEMEX_CORE _core = new TVITEMEX_CORE();
        string _pszText = string.Empty;

        public TVIF mask { get { return (TVIF)_core.mask; } set { _core.mask = (int)value; } }
        public IntPtr hItem { get { return _core.hItem; } set { _core.hItem = value; } }
        public TVIS state { get { return (TVIS)_core.state; } set { _core.state = (int)value; } }
        public TVIS stateMask { get { return (TVIS)_core.stateMask; } set { _core.stateMask = (int)value; } }
        public string pszText { get { return _pszText; } set { _pszText = value; } }
        public int iImage { get { return _core.iImage; } set { _core.iImage = value; } }
        public int iSelectedImage { get { return _core.iSelectedImage; } set { _core.iSelectedImage = value; } }
        public int cChildren { get { return _core.cChildren; } set { _core.cChildren = value; } }
        public IntPtr lParam { get { return _core.lParam; } set { _core.lParam = value; } }
        public int iIntegral { get { return _core.iIntegral; } set { _core.iIntegral = value; } }
        public int uStateEx { get { return _core.uStateEx; } set { _core.uStateEx = value; } }
        public IntPtr hwnd { get { return _core.hwnd; } set { _core.hwnd = value; } }
        public int iExpandedImage { get { return _core.iExpandedImage; } set { _core.iExpandedImage = value; } }
        public int iReserved { get { return _core.iReserved; } set { _core.iReserved = value; } }
        public TVITEMEX_CORE Core { get { return _core; } }
    }
    
#if ENG
    /// <summary>
    /// Please refer to MSDN's TVITEM.
    /// </summary>
#else
    /// <summary>
    /// MSDNのTVITEMを参照お願いします。
    /// </summary>
#endif
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct TVITEMEX_CORE
    {
        public int mask;
        public IntPtr hItem;
        public int state;
        public int stateMask;
        public IntPtr pszText;
        public int cchTextMax;
        public int iImage;
        public int iSelectedImage;
        public int cChildren;
        public IntPtr lParam;
        public int iIntegral;
        public int uStateEx;
        public IntPtr hwnd;
        public int iExpandedImage;
        public int iReserved;
        
#if ENG
        /// <summary>
        /// Get TVITEM.
        /// </summary>
        /// <param name="item">TVITEM.</param>
#else
        /// <summary>
        /// TVITEMの部分を取得します。
        /// </summary>
        /// <param name="item">TVITEMの部分。</param>
#endif
        public void GetTVITEMEX(ref TVITEM item)
        {
            item.mask = mask;
            item.hItem = hItem;
            item.state = state;
            item.stateMask = stateMask;
            item.pszText = pszText;
            item.cchTextMax = cchTextMax;
            item.iImage = iImage;
            item.iSelectedImage = iSelectedImage;
            item.cChildren = cChildren;
            item.lParam = lParam;
        }
    }
}
