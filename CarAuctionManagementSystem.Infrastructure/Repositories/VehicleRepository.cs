using CarAuctionManagementSystem.Application.Interfaces.Repositories;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Enums;
using CarAuctionManagementSystem.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Infrastructure.Repositories
{
    /// <summary>
    /// In-memory implementation of <see cref="IVehicleRepository"/> for vehicle persistence and queries.
    /// </summary>
    public sealed class VehicleRepository : IVehicleRepository
    {
        private readonly List<Vehicle> _vehicles = InMemoryDatabase.Vehicles;

        /// <summary>
        /// Adds a new vehicle to the datastore.
        /// </summary>
        /// <param name="vehicle">The <see cref="Vehicle"/> to add.</param>
        public Task AddAsync(Vehicle vehicle)
        {
            _vehicles.Add(vehicle);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Retrieves a vehicle by its identifier.
        /// </summary>
        /// <param name="id">The vehicle identifier.</param>
        /// <returns>The <see cref="Vehicle"/> if found; otherwise <c>null</c>.</returns>
        public Task<Vehicle?> GetByIdAsync(int id)
        {
            var vehicle = _vehicles.FirstOrDefault(v => v.Id == id);
            return Task.FromResult(vehicle);
        }            

        /// <summary>
        /// Searches for vehicles using the provided filtering parameters.
        /// </summary>
        /// <param name="type">Optional vehicle type to filter by.</param>
        /// <param name="manufacturer">Optional manufacturer name to filter by.</param>
        /// <param name="model">Optional model name to filter by.</param>
        /// <param name="year">Optional production year to filter by.</param>
        /// <returns>A list of matching <see cref="Vehicle"/> entities.</returns>
        public Task<List<Vehicle>> SearchAsync(
            VehicleType? type, string? manufacturer, string? model, int? year)
        {
            var result = _vehicles.Where(x =>
                (!type.HasValue || x.Type == type.Value) &&
                (string.IsNullOrWhiteSpace(manufacturer)) &&
                (string.IsNullOrWhiteSpace(model)) &&
                (!year.HasValue || x.Year == year.Value)).ToList();

            return Task.FromResult(result);
        }
    }

}
