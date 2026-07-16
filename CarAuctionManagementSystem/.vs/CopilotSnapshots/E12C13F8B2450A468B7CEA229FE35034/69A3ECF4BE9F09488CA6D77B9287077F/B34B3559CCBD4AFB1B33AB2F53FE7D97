using CarAuctionManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.Interfaces.Repositories
{
    /// <summary>
    /// Repository contract for accessing and modifying auctions.
    /// </summary>
    public interface IAuctionRepository
    {
        /// <summary>
        /// Retrieves all auctions.
        /// </summary>
        /// <returns>A list of all <see cref="Auction"/> entities.</returns>
        Task<List<Auction>> GetAllAsync();

        /// <summary>
        /// Retrieves all auctions that are currently active.
        /// </summary>
        /// <returns>A list of active <see cref="Auction"/> entities.</returns>
        Task<List<Auction>> GetAllActiveAsync();

        /// <summary>
        /// Retrieves an auction by its identifier.
        /// </summary>
        /// <param name="id">The auction identifier.</param>
        /// <returns>The <see cref="Auction"/> if found; otherwise <c>null</c>.</returns>
        Task<Auction?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves an auction associated with the specified vehicle identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The <see cref="Auction"/> if found; otherwise <c>null</c>.</returns>
        Task<Auction?> GetByVehicleIdAsync(int vehicleId);

        /// <summary>
        /// Adds a new auction or updates an existing one.
        /// </summary>
        /// <param name="auction">The auction to add or update.</param>
        Task AddOrUpdateAsync(Auction auction);
    }
}
