using System;
using Codeer.Friendly.Windows.Grasp;
using Codeer.Friendly.Windows.NativeStandardControls.Inside;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
#if ENG
    /// <summary>
    /// Provides operations on windows of WindowClass type SysIPAddress32.
    /// </summary>
#else
    /// <summary>
    /// WindowClassがSysIPAddress32のウィンドウに対応した操作を提供します。
    /// </summary>
#endif
    public class NativeIPAddress : NativeWindow
    {
        internal const int IPM_SETADDRESS = (NativeCommonDefine.WM_USER + 101);
        internal const int IPM_GETADDRESS = (NativeCommonDefine.WM_USER + 102);
        internal const int IPN_FIRST = -860;
        internal const int IPN_FIELDCHANGED = IPN_FIRST - 0;
        
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="src">WindowControl with the target window. </param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="src">元となるウィンドウコントロールです。</param>
#endif
        public NativeIPAddress(WindowControl src)
            : base(src)
        {
            Initializer.Initialize(App);
        }
        
#if ENG
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="app">Application manipulation object.</param>
        /// <param name="windowHandle">Window handle.</param>
#else
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        /// <param name="app">アプリケーション操作クラス。</param>
        /// <param name="windowHandle">ウィンドウハンドル。</param>
#endif
        public NativeIPAddress(WindowsAppFriend app, IntPtr windowHandle)
            : base(app, windowHandle)
        {
            Initializer.Initialize(App);
        }
        
#if ENG
        /// <summary>
        /// Returns the entered IP address.
        /// </summary>
        /// <param name="field0">Field 0 octet.</param>
        /// <param name="field1">Field 1 octet.</param>
        /// <param name="field2">Field 2 octet.</param>
        /// <param name="field3">Field 3 octet.</param>
#else
        /// <summary>
        /// IPアドレスを取得します。
        /// </summary>
        /// <param name="field0">フィールド0。</param>
        /// <param name="field1">フィールド1。</param>
        /// <param name="field2">フィールド2。</param>
        /// <param name="field3">フィールド3。</param>
#endif
        public void GetAddress(ref byte field0, ref byte field1, ref byte field2, ref byte field3)
        {
            AppVar inTarget0 = App.Dim((byte)0);
            AppVar inTarget1 = App.Dim((byte)0);
            AppVar inTarget2 = App.Dim((byte)0);
            AppVar inTarget3 = App.Dim((byte)0);
            App[GetType(), "GetAddressInTarget"](Handle, inTarget0, inTarget1, inTarget2, inTarget3);
            field0 = (byte)inTarget0.Core;
            field1 = (byte)inTarget1.Core;
            field2 = (byte)inTarget2.Core;
            field3 = (byte)inTarget3.Core;
        }
        
#if ENG
        /// <summary>
        /// Changes the entered IP address.
        /// Causes an IPN_FIELDCHANGED notification.
        /// </summary>
        /// <param name="field0">Field 0 octet.</param>
        /// <param name="field1">Field 1 octet.</param>
        /// <param name="field2">Field 2 octet.</param>
        /// <param name="field3">Field 3 octet.</param>
#else
        /// <summary>
        /// IPアドレスを変更します。
        /// IPN_FIELDCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="field0">フィールド0。</param>
        /// <param name="field1">フィールド1。</param>
        /// <param name="field2">フィールド2。</param>
        /// <param name="field3">フィールド3。</param>
#endif
        public void EmulateChangeAddress(byte field0, byte field1, byte field2, byte field3)
        {
            App[GetType(), "EmulateChangeAddressInTarget"](Handle, field0, field1, field2, field3);
        }
        
#if ENG
        /// <summary>
        /// Changes the entered IP address.
        /// Causes an IPN_FIELDCHANGED notification.
        /// Executes asynchronously. 
        /// </summary>
        /// <param name="field0">Field 0 octet.</param>
        /// <param name="field1">Field 1 octet.</param>
        /// <param name="field2">Field 2 octet.</param>
        /// <param name="field3">Field 3 octet.</param>
        /// <param name="async"> Asynchronous execution object.</param>
#else
        /// <summary>
        /// IPアドレスを変更します。
        /// IPN_FIELDCHANGEDの通知が発生します。
        /// 非同期で実行します。
        /// </summary>
        /// <param name="field0">フィールド0。</param>
        /// <param name="field1">フィールド1。</param>
        /// <param name="field2">フィールド2。</param>
        /// <param name="field3">フィールド3。</param>
        /// <param name="async">非同期実行オブジェクト。</param>
#endif
        public void EmulateChangeAddress(byte field0, byte field1, byte field2, byte field3, Async async)
        {
            App[GetType(), "EmulateChangeAddressInTarget", async](Handle, field0, field1, field2, field3);
        }

        /// <summary>
        /// IPアドレスを変更します。
        /// IPN_FIELDCHANGEDの通知が発生します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="field0">フィールド0。</param>
        /// <param name="field1">フィールド1。</param>
        /// <param name="field2">フィールド2。</param>
        /// <param name="field3">フィールド3。</param>
        private static void EmulateChangeAddressInTarget(IntPtr handle, byte field0, byte field1, byte field2, byte field3)
        {
            NativeMethods.SetFocus(handle);
            byte[] newFields = new byte[] { field0, field1, field2, field3 };
            for (int i = 0; i < newFields.Length; i++)
            {
                //変更前のアドレスを取得。
                int address = 0;
                NativeMethods.SendMessage(handle, IPM_GETADDRESS, IntPtr.Zero, ref address);
                byte[] fields = new byte[] { (byte)((address & 0xff000000) >> 24), (byte)((address & 0x00ff0000) >> 16), 
                                        (byte)((address & 0x0000ff00) >> 8), (byte)(address & 0x000000ff)};

                //一つフィールドを変更
                fields[i] = newFields[i];

                //変更。
                NativeMethods.SendMessage(handle, IPM_SETADDRESS, IntPtr.Zero, new IntPtr((fields[0] << 24) + (fields[1] << 16) + (fields[2] << 8) + fields[3]));

                //WM_NOTIFYでの通知
                NMIPADDRESS ipAdress = new NMIPADDRESS();
                EmulateUtility.InitNotify(handle, IPN_FIELDCHANGED, ref ipAdress.hdr);
                ipAdress.iField = i;
                ipAdress.iValue = newFields[i];
                NativeMethods.SendMessage(NativeMethods.GetParent(handle), NativeCommonDefine.WM_NOTIFY, ipAdress.hdr.idFrom, ref ipAdress);
            }
        }

        /// <summary>
        /// IPアドレスを取得します。
        /// </summary>
        /// <param name="handle">ウィンドウハンドル。</param>
        /// <param name="field0">フィールド0。</param>
        /// <param name="field1">フィールド1。</param>
        /// <param name="field2">フィールド2。</param>
        /// <param name="field3">フィールド3。</param>
        internal static void GetAddressInTarget(IntPtr handle, ref byte field0, ref byte field1, ref byte field2, ref byte field3)
        {
            int address = 0;
            NativeMethods.SendMessage(handle, NativeIPAddress.IPM_GETADDRESS, IntPtr.Zero, ref address);
            field0 = (byte)((address & 0xff000000) >> 24);
            field1 = (byte)((address & 0x00ff0000) >> 16);
            field2 = (byte)((address & 0x0000ff00) >> 8);
            field3 = (byte)(address & 0x000000ff);
        }
    }
}
