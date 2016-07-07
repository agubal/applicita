using System.Threading.Tasks;
using Applicita.Entities;
using Orleans;

namespace Applicita.Communication
{
    /// <summary>
    /// Communication interface for Processor
    /// </summary>
    public interface IProcessor : IGrainWithIntegerKey
    {
        /// <summary>
        /// Start device
        /// </summary>
        /// <param name="device">Device to work on</param>
        /// <returns></returns>
        Task StartDevice(Device device);

        /// <summary>
        /// Stop device
        /// </summary>
        /// <returns></returns>
        Task StopDevice();

        /// <summary>
        /// Get current device temperature
        /// Get current device temperature
        /// </summary>
        /// <returns></returns>
        Task<int> GetCurrentTemperature();
    }
}
