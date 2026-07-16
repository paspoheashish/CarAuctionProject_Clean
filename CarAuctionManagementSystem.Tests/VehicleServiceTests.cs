using CarAuctionManagementSystem.Application.Interfaces.Repositories;
using CarAuctionManagementSystem.Application.Services;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.Tests
{
    public class VehicleServiceTests
    {
        private Mock<IUnitOfWork> _uowMock;
        private Mock<IVehicleRepository> _vehicleRepoMock;
        private VehicleService _service;

        [SetUp]
        public void Setup()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _vehicleRepoMock = new Mock<IVehicleRepository>();

            _uowMock.Setup(u => u.Vehicles).Returns(_vehicleRepoMock.Object);

            _service = new VehicleService(_uowMock.Object);
        }

        [Test]
        public void AddVehicleAsync_ShouldThrow_WhenVehicleAlreadyExists()
        {
            var vehicle = new Vehicle { Id = 10 };

            _vehicleRepoMock.Setup(r => r.GetByIdAsync(10))
                            .ReturnsAsync(vehicle);

            Assert.ThrowsAsync<ApplicationException>(() =>
                _service.AddVehicleAsync(vehicle));
        }

        [Test]
        public async Task AddVehicleAsync_ShouldAddVehicle_WhenNotExists()
        {
            var vehicle = new Vehicle { Id = 10 };

            _vehicleRepoMock.Setup(r => r.GetByIdAsync(10))
                            .ReturnsAsync((Vehicle)null);

            _vehicleRepoMock.Setup(r => r.AddAsync(vehicle))
                            .Returns(Task.CompletedTask);

            _uowMock.Setup(u => u.CommitAsync())
                    .ReturnsAsync(1);

            await _service.AddVehicleAsync(vehicle);

            _vehicleRepoMock.Verify(r => r.AddAsync(vehicle), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Test]
        public async Task SearchAsync_ShouldReturnMatchingVehicles()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle { Id = 1, Manufacturer = "Toyota", Model = "Corolla", Year = 2020 }
            };

            _vehicleRepoMock.Setup(r => r.SearchAsync(
                    VehicleType.Hatchback, "Toyota", "Corolla", 2020))
                .ReturnsAsync(vehicles);

            var result = await _service.SearchAsync(
                VehicleType.Hatchback, "Toyota", "Corolla", 2020);

            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public async Task SearchAsync_ShouldReturnEmptyList_WhenNoMatches()
        {
            _vehicleRepoMock.Setup(r => r.SearchAsync(
                    null, null, null, null))
                .ReturnsAsync(new List<Vehicle>());

            var result = await _service.SearchAsync(null, null, null, null);

            Assert.IsEmpty(result);
        }

    }
}
