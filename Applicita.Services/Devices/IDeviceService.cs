using System;
using Applicita.Entities.Common;
using Applicita.Entities.Events;

namespace Applicita.Services.Devices
{
    public interface IDeviceService
    {
        /// <summary>
        /// Event is being fired once per second with new Avarage temperature calculated
        /// </summary>
        event EventHandler<DeviceArgs> OnAvarageTempIsAvailable;

        /// <summary>
        /// Is being fired when of devices is getting too hot
        /// </summary>
        event EventHandler<EventArgs> OnDeviceIsTooHot;

        /// <summary>
        /// Starto work on devices
        /// </summary>
        /// <returns></returns>
        ServiceResult StartDevices();

        /// <summary>
        /// Stop all devices
        /// </summary>
        /// <returns></returns>
        ServiceResult StopDevices();
    }
}
