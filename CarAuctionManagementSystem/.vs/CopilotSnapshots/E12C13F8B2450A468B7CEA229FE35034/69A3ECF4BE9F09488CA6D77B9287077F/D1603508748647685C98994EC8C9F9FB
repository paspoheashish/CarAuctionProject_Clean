using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.Interfaces.Repositories
{
    /// <summary>
    /// Unit of work that aggregates repository access and transaction commit.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Repository for auction operations.
        /// </summary>
        IAuctionRepository Auctions { get; }

        /// <summary>
        /// Repository for vehicle operations.
        /// </summary>
        IVehicleRepository Vehicles { get; }

        /// <summary>
        /// Repository for bid operations.
        /// </summary>
        IBidRepository Bids { get; }

        /// <summary>
        /// Commits pending changes to the underlying datastore.
        /// </summary>
        /// <returns>The number of affected records.</returns>
        Task<int> CommitAsync();
    }

}
