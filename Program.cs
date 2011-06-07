using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.IO.Ports;

namespace Prototype.Fez.BootloaderUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            var controller = new FezBootloaderController();
            controller.Open();
            System.Console.WriteLine("Version is {0} so there", controller.GetLoaderVersion());
            controller.LoadFirmware(@"C:\Program Files (x86)\GHI Electronics\GHI NETMF v4.1 SDK\USBizi\Firmware\USBizi_CLR.GHI");
            System.Console.WriteLine("All done.");
        }
    }
}
