namespace Codeer.Friendly.Windows.NativeStandardControls
{
    /// <summary>
    /// 共通定義。
    /// 詳細な情報はMSDNの同名の定義を参照お願いします。
    /// </summary>
    static class NativeCommonDefine
    {
        internal const int GWL_STYLE = -16;
        internal const int IMAGE_BITMAP = 0;
        internal const int IMAGE_ICON = 1;
        internal const int IMAGE_CURSOR = 2;
        internal const int WM_CUT = 0x0300;
        internal const int WM_COPY = 0x0301;
        internal const int WM_PASTE = 0x0302;
        internal const int WM_CLEAR = 0x0303;
        internal const int WM_UNDO = 0x0304;
        internal const int EC_LEFTMARGIN = 0x0001;
        internal const int EC_RIGHTMARGIN = 0x0002;
        internal const int WM_USER = 0x0400;
        internal const int CCM_FIRST = 0x2000;
        internal const int CCM_SETCOLORSCHEME = (CCM_FIRST + 2);
        internal const int CCM_GETCOLORSCHEME = (CCM_FIRST + 3);
        internal const int CCM_GETDROPTARGET = (CCM_FIRST + 4);
        internal const int CCM_SETUNICODEFORMAT = (CCM_FIRST + 5);
        internal const int CCM_GETUNICODEFORMAT = (CCM_FIRST + 6);
        internal const int CCM_SETWINDOWTHEME = (CCM_FIRST + 0xb);
        internal const int WM_NOTIFY = 0x004E;
        internal const int WM_COMMAND = 0x0111;
        internal const int WM_VSCROLL = 0x0115;
        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_CHAR = 0x0102;
        internal const int WM_SYSKEYDOWN = 0x0104;
        internal const int WM_SYSKEYUP = 0x0105;
        internal const int WM_SYSCHAR = 0x0106;
        internal const int NM_CLICK = -2;
    }
}
