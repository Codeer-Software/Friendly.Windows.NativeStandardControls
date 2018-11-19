using Codeer.Friendly.Windows.Grasp;
using System;

namespace Codeer.Friendly.Windows.NativeStandardControls
{
    public class NativeCommand
    {
        WindowControl _receiveWindow;
        public int Id { get; }

        public NativeCommand(WindowControl receiveWindow, int id)
        {
            _receiveWindow = receiveWindow;
            Id = id;
        }

        public void EmulateClick() => _receiveWindow.SendMessage(0x111, new IntPtr(Id), IntPtr.Zero);

        public void EmulateClick(Async async) => _receiveWindow.SendMessage(0x111, new IntPtr(Id), IntPtr.Zero, async);
    }
}
