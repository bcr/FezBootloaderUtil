using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Management;
using System.IO;

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
            LoadFirmware = (Byte)'X',
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

        internal void LoadFirmware(string filename)
        {
            // Load the file into a memory block

            byte[] data = ReadFileIntoByteArray(filename);

            // Set up an XMODEM object

            var xmodem = new XModem.XModem(fezPort);
            var bytesSent = 0;
            xmodem.PacketSent += (sender, args) => { bytesSent += 1024;  System.Console.Write("{0}% sent\r", Math.Min(bytesSent, data.Length) * 100 / data.Length ); };

            // Tell the FEZ to get ready for some firmware

            SendCommand(FezCommand.LoadFirmware);
            fezPort.ReadLine(); // Eat "Start File Transfer" chit chat

            // Transfer the block

            var result = xmodem.XmodemTransmit(data, data.Length, true);

            // Throw an exception if anything freaked

            if (result < data.Length)
            {
                throw new Exception("Failed to transmit file " + result);
            }

            System.Console.WriteLine();
            while (true)
            {
                var line = fezPort.ReadLine();
                System.Console.WriteLine(line);
            }
        }

        private byte[] ReadFileIntoByteArray(string filename)
        {
            MemoryStream outputStream;

            using (var inputStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                outputStream = new MemoryStream((int) inputStream.Length);
                inputStream.CopyTo(outputStream);
            }

            return outputStream.ToArray();
        }
    }
}
