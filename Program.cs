using System;

namespace Prototype.Fez.BootloaderUtil
{
    internal class Program
    {
        private static int Main()
        {
            var controller = new FezBootloaderController();
            try
            {
                controller.Open();
                Console.WriteLine("Loader version is {0}", controller.GetLoaderVersion());
                controller.LoadFirmware(
                    @"C:\Program Files (x86)\GHI Electronics\GHI NETMF v4.1 SDK\USBizi\Firmware\USBizi_CLR.GHI");
                Console.WriteLine("All done.");
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