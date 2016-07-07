using System;

namespace Applicita.Entities
{
    /// <summary>
    /// Represents device
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Device Id
        /// </summary>
        public Guid DeviceId { get; set; }

        /// <summary>
        /// Device name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Current device Temperature
        /// </summary>
        public int Temperature
        {
            get { return _temperature; }
            set
            {
                _temperature = value;
                if (_temperature >= MaxTemperature)
                {
                    OnDeviceIsTooHot?.Invoke(this, new EventArgs());
                }
            }
        }

        /// <summary>
        /// Maximum device Temperature
        /// </summary>
        public int MaxTemperature { get; set; }

        /// <summary>
        /// Event is being fired when Device is to hot
        /// </summary>
        public event EventHandler<EventArgs> OnDeviceIsTooHot;

        public Device(Guid deviceId, string name, int temperature, int maxTemperature)
        {
            DeviceId = deviceId;
            Name = name;
            _temperature = temperature;
            MaxTemperature = maxTemperature;
        }

        private int _temperature;
    }
}
