using System;
using System.Collections.Generic;
using Applicita.Entities;

namespace Applicita.DataAccess
{
    /// <summary>
    /// Emulation of database
    /// </summary>
    internal static class Database
    {
        public static List<Device> Devices = new List<Device>
        {
            new Device(Guid.NewGuid(), "Oskar", 0, 15),
            new Device(Guid.NewGuid(), "Omar", 0, 15),
            new Device(Guid.NewGuid(), "Tom", 0, 15),
            new Device(Guid.NewGuid(), "Tim", 0, 15),
            new Device(Guid.NewGuid(), "Ben", 0, 15)
        };
    }
}
