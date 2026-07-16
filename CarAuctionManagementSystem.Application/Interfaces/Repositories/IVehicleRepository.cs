using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.Interfaces.Repositories
{
    /// <summary>
    /// Repository contract for vehicle persistence and queries.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a new vehicle to the datastore.
        /// </summary>
        /// <param name="vehicle">The <see cref="Vehicle"/> to add.</param>
        Task AddAsync(Vehicle vehicle);

        /// <summary>
        /// Retrieves a vehicle by its identifier.
        /// </summary>
        /// <param name="id">The vehicle identifier.</param>
        /// <returns>The <see cref="Vehicle"/> if found; otherwise <c>null</c>.</returns>
        Task<Vehicle?> GetByIdAsync(int id);

        /// <summary>
        /// Searches for vehicles using the provided filtering parameters.
        /// </summary>
        /// <param name="type">Optional vehicle type to filter by.</param>
        /// <param name="manufacturer">Optional manufacturer name to filter by.</param>
        /// <param name="model">Optional model name to filter by.</param>
        /// <param name="year">Optional production year to filter by.</param>
        /// <returns>A list of matching <see cref="Vehicle"/> entities.</returns>
        Task<List<Vehicle>> SearchAsync(VehicleType? type, string? manufacturer, string? model, int? year);
    }
}
