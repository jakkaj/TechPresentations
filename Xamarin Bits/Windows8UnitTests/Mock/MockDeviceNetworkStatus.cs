using System;
using XamlingCore.Portable.Contract.Network;
using XamlingCore.Portable.Model.Network;

namespace Windows8UnitTests.Mock
{
    public class WinMockDeviceNetworkStatus : IDeviceNetworkStatus
    {
        public bool HardcodeNetworkStatus { get; set; }

        public event EventHandler NetworkChanged;

        public WinMockDeviceNetworkStatus()
        {
            HardcodeNetworkStatus = true;
        }
        public bool QuickNetworkCheck()
        {
            return HardcodeNetworkStatus;
        }

        public XNetworkType NetworkCheck()
        {
            throw new NotImplementedException();
        }
    }
}
