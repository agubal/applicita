using System;
using System.Threading.Tasks;
using Applicita.Communication;
using Applicita.Entities;
using Orleans;

namespace Applicita.Agents
{
    /// <summary>
    /// Device processor. Emulates device's work
    /// </summary>
    public class Processor : Grain, IProcessor
    {
        /// <summary>
        /// Device to work on
        /// </summary>
        private Device Device { get; set; }

        private readonly Random _random;
        private IDisposable _timer;

        public Processor()
        {
            _random = new Random();
        }

        /// <summary>
        /// Start device work
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public Task StartDevice(Device device)
        {
            Device = device;
            _timer = RegisterTimer(HandleDevice, Device, TimeSpan.Zero, TimeSpan.FromSeconds(2));
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stop device work
        /// </summary>
        /// <returns></returns>
        public Task StopDevice()
        {
            _timer?.Dispose();
            return Task.CompletedTask;
        }      

        /// <summary>
        /// Get current device temperature
        /// </summary>
        /// <returns></returns>
        public Task<int> GetCurrentTemperature()
        {
            return Task.FromResult(Device.Temperature);
        }

        private Task HandleDevice(object obj)
        {
            Device.Temperature += _random.Next(0, 6);
            return Task.CompletedTask;
        }
    }
}
