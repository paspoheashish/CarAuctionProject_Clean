using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.Interfaces.Services
{
    /// <summary>
    /// Service contract for vehicle-related operations.
    /// </summary>
    public interface IVehicleService
    {
        /// <summary>
        /// Adds a new vehicle for the current user.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        Task AddVehicleAsync(Vehicle vehicle);

        /// <summary>
        /// Searches for vehicles using the provided parameters.
        /// </summary>
        /// <param name="type">Optional vehicle type filter.</param>
        /// <param name="manufacturer">Optional manufacturer filter.</param>
        /// <param name="model">Optional model filter.</param>
        /// <param name="year">Optional production year filter.</param>
        /// <returns>A collection of matching <see cref="Vehicle"/> entities.</returns>
        Task<IEnumerable<Vehicle>> SearchAsync(
            VehicleType? type,
            string? manufacturer,
            string? model,
            int? year);
    }
}
