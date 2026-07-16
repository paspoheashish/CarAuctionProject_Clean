using CarAuctionManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.Interfaces.Repositories
{
    /// <summary>
    /// Repository contract for bid persistence operations.
    /// </summary>
    public interface IBidRepository
    {
        /// <summary>
        /// Adds a new bid to the datastore.
        /// </summary>
        /// <param name="bid">The <see cref="Bid"/> to add.</param>
        Task AddAsync(Bid bid);

        /// <summary>
        /// Retrieves bids associated with a specific auction.
        /// </summary>
        /// <param name="auctionId">The auction identifier.</param>
        /// <returns>A collection of <see cref="Bid"/> entities for the auction.</returns>
        Task<IEnumerable<Bid>> GetByAuctionIdAsync(int auctionId);
    }
}
