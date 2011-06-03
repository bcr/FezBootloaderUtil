using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.IO.Ports;

//Availability 2
//Binary True
//Capabilities
//CapabilityDescriptions
//Caption GHI Boot Loader Interface (COM5)
//ConfigManagerErrorCode 0
//ConfigManagerUserConfig False
//CreationClassName Win32_SerialPort
//Description GHI Boot Loader Interface
//DeviceID COM5
//ErrorCleared
//ErrorDescription
//InstallDate
//LastErrorCode
//MaxBaudRate 115200
//MaximumInputBufferSize 0
//MaximumOutputBufferSize 0
//MaxNumberControlled
//Name GHI Boot Loader Interface (COM5)
//OSAutoDiscovered True
//PNPDeviceID USB\VID_1B9F&PID_0104\5&35EDA8E7
//PowerManagementCapabilities System.UInt16[]
//PowerManagementSupported False
//ProtocolSupported
//ProviderType Modem Device
//SettableBaudRate True
//SettableDataBits True
//SettableFlowControl True
//SettableParity True
//SettableParityCheck True
//SettableRLSD True
//SettableStopBits True
//Status OK
//StatusInfo 3
//Supports16BitMode False
//SupportsDTRDSR True
//SupportsElapsedTimeouts True
//SupportsIntTimeouts True
//SupportsParityCheck True
//SupportsRLSD True
//SupportsRTSCTS False
//SupportsSpecialCharacters False
//SupportsXOnXOff False
//SupportsXOnXOffSet False
//SystemCreationClassName Win32_ComputerSystem
//SystemName BLAKKAKE
//TimeOfLastReset

namespace Prototype.Fez.BootloaderUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new FezBootloaderController();
            controller.Open();
            System.Console.WriteLine("Version is {0} so there", controller.GetLoaderVersion());
            System.Console.WriteLine("All done.");
        }
    }
}
