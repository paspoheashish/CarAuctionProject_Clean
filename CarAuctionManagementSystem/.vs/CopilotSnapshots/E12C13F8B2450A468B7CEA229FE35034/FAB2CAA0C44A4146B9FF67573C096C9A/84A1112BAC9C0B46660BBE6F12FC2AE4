using CarAuctionManagementSystem.Application.Interfaces.Repositories;
using CarAuctionManagementSystem.Application.Interfaces.Services;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.Services
{
    /// <summary>
    /// Provides vehicle-related business logic and coordinates repository operations.
    /// </summary>
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleService"/> class.
        /// </summary>
        /// <param name="uow">The unit of work for repository access.</param>
        public VehicleService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Adds a new vehicle to the datastore.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A task that completes when the vehicle has been added.</returns>
        /// <exception cref="ApplicationException">Thrown if a vehicle with the same id already exists.</exception>
        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            if (await _uow.Vehicles.GetByIdAsync(vehicle.Id) != null)
            {
                throw new ApplicationException($"Vehicle ID {vehicle.Id} already exists.");
            }
            await _uow.Vehicles.AddAsync(vehicle);
            await _uow.CommitAsync();
        }

        /// <summary>
        /// Searches for vehicles using the provided parameters.
        /// </summary>
        /// <param name="type">Optional vehicle type filter.</param>
        /// <param name="manufacturer">Optional manufacturer filter.</param>
        /// <param name="model">Optional model filter.</param>
        /// <param name="year">Optional production year filter.</param>
        /// <returns>A collection of matching <see cref="Vehicle"/> entities.</returns>
        public async Task<IEnumerable<Vehicle>> SearchAsync(
            VehicleType? type, string? manufacturer, string? model, int? year)
        {
            return await _uow.Vehicles.SearchAsync(type, manufacturer, model, year);
        }

    }
}
