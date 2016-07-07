using System;
using System.Collections.Generic;
using System.Linq;
using Applicita.DataAccess;
using Applicita.Entities;
using Applicita.Entities.Common;
using Applicita.Services.Devices;
using Moq;
using NUnit.Framework;

namespace Applicita.Services.Tests
{
    [TestFixture]
    public class ServicesTest
    {
        private Mock<IRepository<Device>> _mockDeviceRepository;
        private IDeviceService _deviceService;

        [SetUp]
        public void Initialize()
        {
            _mockDeviceRepository = new Mock<IRepository<Device>>();
            _deviceService = new DeviceService(_mockDeviceRepository.Object);
        }


        [Test]
        public void StartDevices_WhenWhereAreNoDevices()
        {
            //Arrange
            _mockDeviceRepository.Setup(g => g.GetAll()).Returns(new List<Device>());
            const string expectedError = "There are no devices to start";

            //Act
            ServiceResult result = _deviceService.StartDevices();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded);
            Assert.IsNotNull(result.Errors);
            Assert.AreEqual(result.Errors.First(), expectedError);
        }

        [Test]
        public void StartDevices_WhenRepositoryReturnsNull()
        {
            //Arrange
            _mockDeviceRepository.Setup(g => g.GetAll()).Returns((List<Device>) null);
            const string expectedError = "There are no devices to start";

            //Act
            ServiceResult result = _deviceService.StartDevices();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Succeeded);
            Assert.IsNotNull(result.Errors);
            Assert.AreEqual(result.Errors.First(), expectedError);
        }
    }
}
