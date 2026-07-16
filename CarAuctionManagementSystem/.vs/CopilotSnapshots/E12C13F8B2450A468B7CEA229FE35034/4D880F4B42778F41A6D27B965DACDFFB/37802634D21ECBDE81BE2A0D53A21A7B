using CarAuctionManagementSystem.Application.Interfaces.Repositories;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Infrastructure.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Infrastructure.Repositories
{
    /// <summary>
    /// In-memory implementation of <see cref="IBidRepository"/> for bid persistence.
    /// </summary>
    public sealed class BidRepository : IBidRepository
    {
        private readonly List<Auction> _auctions = InMemoryDatabase.Auctions;

        /// <summary>
        /// Adds a new bid to the auction's bid list and updates the current highest bid.
        /// </summary>
        /// <param name="bid">The <see cref="Bid"/> to add.</param>
        public Task AddAsync(Bid bid)
        {
            var existingAuction = _auctions.First(x => x.Id == bid.AuctionId);
            existingAuction.Bids.Add(bid);
            existingAuction.CurrentHighestBid = bid.Amount;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Retrieves bids associated with a specific auction.
        /// </summary>
        /// <param name="auctionId">The auction identifier.</param>
        /// <returns>A collection of <see cref="Bid"/> entities for the auction.</returns>
        public Task<IEnumerable<Bid>> GetByAuctionIdAsync(int auctionId)
        {
            var existingAuction = _auctions.First(x => x.Id == auctionId);
            return Task.FromResult(existingAuction.Bids.AsEnumerable());
        }
    }
}
