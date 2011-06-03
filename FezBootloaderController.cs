using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Management;

namespace Prototype.Fez.BootloaderUtil
{
    class FezBootloaderController
    {
        private SerialPort fezPort;

        internal void Open()
        {
            fezPort = OpenFezBootloaderSerialPort();
            fezPort.Open();
            fezPort.NewLine = "\r\n";
        }

        // http://www.codeproject.com/Messages/3692563/Get-correct-driver-name-from-Device-management-ass.aspx
        // http://geekswithblogs.net/PsychoCoder/archive/2008/01/25/using_wmi_in_csharp.aspx

        private SerialPort OpenFezBootloaderSerialPort()
        {
            var searcher = new ManagementObjectSearcher("select DeviceID,MaxBaudRate from Win32_SerialPort where Description = \"GHI Boot Loader Interface\"");
            foreach (var obj in searcher.Get())
            {
                return new SerialPort((string) obj["DeviceID"], (int) (uint) obj["MaxBaudRate"]);
            }
            throw new Exception("Unable to find FEZ device. Is it in bootloader mode?");
        }

        enum FezCommand : byte
        {
            None = 0,
            GetLoaderVersion = (Byte) 'V',
        }

        internal string GetLoaderVersion()
        {
            SendCommand(FezCommand.GetLoaderVersion);
            var response = fezPort.ReadLine();
            fezPort.ReadLine(); // Eat trailing BL\r\n
            return response;
        }

        private void SendCommand(FezCommand fezCommand)
        {
            fezPort.Write(new byte[] { (byte)fezCommand }, 0, 1);
        }
    }
}
