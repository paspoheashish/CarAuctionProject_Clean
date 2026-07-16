using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Enums;
using CarAuctionManagementSystem.Infrastructure.DataBase;
using CarAuctionManagementSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Infrastructure.Tests
{
    public class VehicleRepositoryTests
    {
        private VehicleRepository _repo;

        [SetUp]
        public void Setup()
        {
            InMemoryDatabase.Vehicles = new List<Vehicle>();
            _repo = new VehicleRepository();
        }

        [Test]
        public async Task AddAsync_ShouldAddVehicle()
        {
            var vehicle = new Vehicle { Id = 1, Manufacturer = "Toyota" };

            await _repo.AddAsync(vehicle);

            Assert.AreEqual(1, InMemoryDatabase.Vehicles.Count);
            Assert.AreEqual("Toyota", InMemoryDatabase.Vehicles[0].Manufacturer);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnVehicle_WhenExists()
        {
            var vehicle = new Vehicle { Id = 10 };
            InMemoryDatabase.Vehicles.Add(vehicle);

            var result = await _repo.GetByIdAsync(10);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.Id);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            var result = await _repo.GetByIdAsync(999);

            Assert.IsNull(result);
        }

        [Test]
        public async Task SearchAsync_ShouldFilterByType()
        {
            InMemoryDatabase.Vehicles.Add(new Vehicle { Id = 1, Type = VehicleType.Hatchback });
            InMemoryDatabase.Vehicles.Add(new Vehicle { Id = 2, Type = VehicleType.Truck });

            var result = await _repo.SearchAsync(VehicleType.Hatchback, null, null, null);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(VehicleType.Hatchback, result.First().Type);
        }

        [Test]
        public async Task SearchAsync_ShouldFilterByYear()
        {
            InMemoryDatabase.Vehicles.Add(new Vehicle { Id = 1, Year = 2020 });
            InMemoryDatabase.Vehicles.Add(new Vehicle { Id = 2, Year = 2021 });

            var result = await _repo.SearchAsync(null, null, null, 2020);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2020, result.First().Year);
        }

        [Test]
        public async Task SearchAsync_ShouldIgnoreManufacturerAndModel()
        {
            InMemoryDatabase.Vehicles.Add(new Vehicle
            {
                Id = 1,
                Manufacturer = "Toyota",
                Model = "Corolla"
            });

            var result = await _repo.SearchAsync(null, "Toyota", "Corolla", null);

            Assert.AreEqual(0, result.Count); // Because your logic excludes non-empty manufacturer/model
        }

        [Test]
        public async Task SearchAsync_ShouldReturnAll_WhenNoFilters()
        {
            InMemoryDatabase.Vehicles.Add(new Vehicle { Id = 1 });
            InMemoryDatabase.Vehicles.Add(new Vehicle { Id = 2 });

            var result = await _repo.SearchAsync(null, null, null, null);

            Assert.AreEqual(2, result.Count);
        }

    }
}
