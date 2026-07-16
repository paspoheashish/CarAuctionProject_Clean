using CarAuctionManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.Interfaces.Services
{
    /// <summary>
    /// Service contract for auction-related operations.
    /// </summary>
    public interface IAuctionService
    {
        /// <summary>
        /// Retrieves all auctions.
        /// </summary>
        /// <returns>A list of <see cref="Auction"/> entities.</returns>
        Task<List<Auction>> GetAllAsync();      

        /// <summary>
        /// Starts a new auction for the specified vehicle and owner.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier to start the auction for.</param>
        /// <param name="ownerUserId">The user id of the auction owner.</param>
        Task StartAuctionAsync(int vehicleId, string ownerUserId);

        /// <summary>
        /// Closes an existing auction.
        /// </summary>
        /// <param name="auctionId">The auction identifier to close.</param>
        /// <param name="ownerUserId">The user id of the auction owner requesting close.</param>
        Task CloseAuctionAsync(int auctionId, string ownerUserId);
    }
}
