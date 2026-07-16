using CarAuctionManagementSystem.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Infrastructure.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IUnitOfWork"/> that aggregates repository instances.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Repository for auction operations.
        /// </summary>
        public IAuctionRepository Auctions { get; }

        /// <summary>
        /// Repository for vehicle operations.
        /// </summary>
        public IVehicleRepository Vehicles { get; }

        /// <summary>
        /// Repository for bid operations.
        /// </summary>
        public IBidRepository Bids { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
        /// </summary>
        /// <param name="auctionRepository">The auction repository.</param>
        /// <param name="vehicleRepository">The vehicle repository.</param>
        /// <param name="bidRepository">The bid repository.</param>
        public UnitOfWork(
            IAuctionRepository auctionRepository,
            IVehicleRepository vehicleRepository,
            IBidRepository bidRepository)
        {
            Auctions = auctionRepository;
            Vehicles = vehicleRepository;
            Bids = bidRepository;
        }

        /// <summary>
        /// Commits pending changes to the underlying datastore.
        /// </summary>
        /// <returns>The number of affected records.</returns>
        public async Task<int> CommitAsync()
        {
            return await Task.FromResult(0); // or DbContext.SaveChangesAsync()
        }
    }

}
