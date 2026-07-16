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
    /// In-memory implementation of <see cref="IAuctionRepository"/> for auction persistence.
    /// </summary>
    public sealed class AuctionRepository : IAuctionRepository
    {
        private readonly List<Auction> _auctions = InMemoryDatabase.Auctions;

        /// <summary>
        /// Retrieves all auctions from the datastore.
        /// </summary>
        /// <returns>A list of all <see cref="Auction"/> entities.</returns>
        public Task<List<Auction>> GetAllAsync()
        {
            return Task.FromResult(_auctions.ToList());
        }

        /// <summary>
        /// Retrieves all auctions that are currently active.
        /// </summary>
        /// <returns>A list of active <see cref="Auction"/> entities.</returns>
        public Task<List<Auction>> GetAllActiveAsync()
        {
            return Task.FromResult(_auctions.Where(x => x.Status == true).ToList());
        }

        /// <summary>
        /// Retrieves an auction by its identifier.
        /// </summary>
        /// <param name="id">The auction identifier.</param>
        /// <returns>The <see cref="Auction"/> if found; otherwise <c>null</c>.</returns>
        public Task<Auction?> GetByIdAsync(int id)
        {
            return Task.FromResult(_auctions.FirstOrDefault(x => x.Id == id));
        }

        /// <summary>
        /// Retrieves an auction associated with the specified vehicle identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The <see cref="Auction"/> if found; otherwise <c>null</c>.</returns>
        public Task<Auction?> GetByVehicleIdAsync(int vehicleId)
        {
            var auction = _auctions.FirstOrDefault(x => x.VehicleId == vehicleId);
            return Task.FromResult(auction);
        }

        /// <summary>
        /// Adds a new auction or updates an existing one in the datastore.
        /// </summary>
        /// <param name="auction">The <see cref="Auction"/> to add or update.</param>
        public Task AddOrUpdateAsync(Auction auction)
        {
            var existingAuction = _auctions.FirstOrDefault(x => x.Id == auction.Id);
            if (existingAuction != null)
            {
                _auctions[_auctions.IndexOf(existingAuction)] = auction;
            }
            else
            {
                _auctions.Add(auction);
            }
            return Task.CompletedTask;
        }
    }
}
