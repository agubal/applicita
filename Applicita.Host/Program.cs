using System;
using Applicita.Entities;
using Applicita.Entities.Common;
using Applicita.Entities.Events;
using Applicita.Services.Devices;

namespace Applicita.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            //Start and initialize Orlean host:
            AppDomain hostDomain = Startup.Start();

            //Start Devices:
            IDeviceService deviceService = new DeviceService();
            deviceService.OnAvarageTempIsAvailable += ShowAvarageTemperature;
            deviceService.OnDeviceIsTooHot += ShowHotDevice;
            ServiceResult serviceResult = deviceService.StartDevices();
            if(!serviceResult.Succeeded) Console.WriteLine(string.Join(",", serviceResult.Errors));

            //Finish aplication:
            Console.WriteLine("Press Enter to stop...");
            Console.ReadLine();
            hostDomain.DoCallBack(Startup.ShutdownSilo);
        }

        /// <summary>
        /// Handlet to show avarage temperature
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ShowAvarageTemperature(object sender, DeviceArgs e)
        {
            Console.WriteLine($"Avarage temperature: {e.Temperature}");
        }

        /// <summary>
        /// Handlet to show Warning from device
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ShowHotDevice(object sender, EventArgs e)
        {
            var device = sender as Device;
            if(device == null) return;
            Console.WriteLine($"WARNING: Device {device.Name} is too hot! It's current temperature is {device.Temperature}");
        }
        
    }
}
