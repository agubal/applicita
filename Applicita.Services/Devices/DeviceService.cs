using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Applicita.Communication;
using Applicita.DataAccess;
using Applicita.Entities;
using Applicita.Entities.Common;
using Applicita.Entities.Events;
using Orleans;

namespace Applicita.Services.Devices
{
    public class DeviceService : IDeviceService
    {
        private readonly IRepository<Device> _deviceRepository;
        private readonly Random _random;
        public event EventHandler<DeviceArgs> OnAvarageTempIsAvailable;
        public event EventHandler<EventArgs> OnDeviceIsTooHot;
        private Timer _timer;
        private const int AvarageTemperatureInterval = 1;
        private readonly List<IProcessor> _processors;

        public DeviceService() : this(new DeviceRepository())
        {          
        }

        public DeviceService(IRepository<Device> deviceRepository)
        {
            _deviceRepository = deviceRepository;
            _random = new Random();
            _processors = new List<IProcessor>();
        }

        public ServiceResult StartDevices()
        {
            //Get all devices from database:
            List<Device> devices = _deviceRepository.GetAll();
            if (devices == null || !devices.Any()) return new ServiceResult("There are no devices to start");

            try
            {
                for (int i = 0; i < devices.Count; i++)
                {
                    //Sunscribe on event when device is too hot:
                    if (OnDeviceIsTooHot != null) devices[i].OnDeviceIsTooHot += OnDeviceIsTooHot.Invoke;

                    //Get processor from Service Factory (Microsoft Orlean) and start it:
                    var processor = GrainClient.GrainFactory.GetGrain<IProcessor>(i);
                    processor.StartDevice(devices[i]);
                    _processors.Add(processor);
                }

                //Run timet to measure avarage temperature once per second:
                _timer = new Timer(CheckDevicesTemperature, _processors, TimeSpan.Zero, TimeSpan.FromSeconds(AvarageTemperatureInterval));
                return new ServiceResult();
            }
            catch (Exception ex)
            {
                return new ServiceResult($"Failed to start devices: {ex.Message}");
            }
        }

        public ServiceResult StopDevices()
        {
            try
            {    
                //Stop timer:            
                _timer.Change(Timeout.Infinite, Timeout.Infinite);

                //Stop devices:
                foreach (IProcessor processor in _processors)
                {
                    processor.StopDevice();
                }
                return new ServiceResult();
            }
            catch (Exception ex)
            {
                return new ServiceResult($"Failed to stop devices: {ex.Message}");
            }

        }

        private void CheckDevicesTemperature(object obj)
        {
            var processors = obj as List<IProcessor>;
            if (processors == null) return;

            //Select random devices to check:
            int[] deviceToCheck = SelectRandomDevicesToCheck(processors);

            //Measure total temperature:
            int totalTemperature = 0;
            for (int i = 0; i < deviceToCheck.Length; i++)
            {
                totalTemperature += processors[deviceToCheck[i]].GetCurrentTemperature().Result;
            }
            //Calculate and return avarage temperature:
            OnAvarageTempIsAvailable?.Invoke(this, new DeviceArgs(totalTemperature / deviceToCheck.Length));
        }

        private int[] SelectRandomDevicesToCheck(List<IProcessor> prcessors)
        {
            const int min = 0;
            int max = prcessors.Count - 1;
            return Enumerable
                .Repeat(0, 5)
                .Select(i => _random.Next(min, max))
                .ToArray();
        }
    }
}
