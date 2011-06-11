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
        static int Main(string[] args)
        {
            var controller = new FezBootloaderController();
            try
            {
                controller.Open();
                System.Console.WriteLine("Loader version is {0}", controller.GetLoaderVersion());
                controller.LoadFirmware(@"C:\Program Files (x86)\GHI Electronics\GHI NETMF v4.1 SDK\USBizi\Firmware\USBizi_CLR.GHI");
                System.Console.WriteLine("All done.");
            }
            catch (FezBootloaderException e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }

            return 0;
        }
    }
}
