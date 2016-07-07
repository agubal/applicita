using System;

namespace Applicita.Entities.Events
{
    /// <summary>
    /// Custom event args to pass temperature
    /// </summary>
    public class DeviceArgs : EventArgs
    {
        public DeviceArgs(int temperature)
        {
            Temperature = temperature;
        }
        public int Temperature { get; set; }
    }
}
