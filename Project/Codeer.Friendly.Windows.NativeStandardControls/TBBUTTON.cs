using System;
using System.Runtime.InteropServices;

namespace Codeer.Friendly.Windows.NativeStandardControls
{

    [StructLayout(LayoutKind.Sequential)]
    public struct TBBUTTON
    {
        public int iBitmap;
        public int idCommand;
        [StructLayout(LayoutKind.Explicit)]
        private struct TBBUTTON_U
        {
            [FieldOffset(0)] public byte fsState;
            [FieldOffset(1)] public byte fsStyle;
            [FieldOffset(0)] private IntPtr bReserved;
        }
        private TBBUTTON_U union;
        public byte fsState { get { return union.fsState; } set { union.fsState = value; } }
        public byte fsStyle { get { return union.fsStyle; } set { union.fsStyle = value; } }
        public IntPtr dwData;
        public IntPtr iString;
    }
}
