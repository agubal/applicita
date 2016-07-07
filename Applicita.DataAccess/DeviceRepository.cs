using System.Collections.Generic;
using Applicita.Entities;

namespace Applicita.DataAccess
{
    public class DeviceRepository : IRepository<Device>
    {
        public List<Device> GetAll()
        {
            return Database.Devices;
        }
    }
}
