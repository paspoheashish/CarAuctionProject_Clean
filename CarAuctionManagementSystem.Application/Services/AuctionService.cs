using CarAuctionManagementSystem.Application.Interfaces.Repositories;
using CarAuctionManagementSystem.Application.Interfaces.Services;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.Services
{
    /// <summary>
    /// Provides auction-related business logic and coordinates repository operations.
    /// </summary>
    public class AuctionService : IAuctionService
    {
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionService"/> class.
        /// </summary>
        /// <param name="uow">The unit of work for repository access.</param>
        public AuctionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Retrieves all auctions.
        /// </summary>
        /// <returns>A list of all <see cref="Auction"/> entities.</returns>
        public async Task<List<Auction>> GetAllAsync()
        {
            var auctions = await _uow.Auctions.GetAllAsync();
            if (auctions == null)
            {
                throw new ApplicationException("No auction record exists.");
            }
            return auctions;
        }

        /// <summary>
        /// Starts a new auction for the specified vehicle on behalf of the owner.
        /// </summary>
        /// <param name="vehicleId">The identifier of the vehicle to auction.</param>
        /// <param name="ownerUserId">The user id of the owner starting the auction.</param>
        /// <returns>A task that completes when the auction has been started.</returns>
        /// <exception cref="ApplicationException">Thrown when the vehicle is not found or auction cannot be started.</exception>
        public async Task StartAuctionAsync(int vehicleId, string ownerUserId)
        {
            var vehicle = await _uow.Vehicles.GetByIdAsync(vehicleId);
            if (vehicle == null)
            {
                throw new ApplicationException("Vehicle not found.");
            }

            var auction = await _uow.Auctions.GetByVehicleIdAsync(vehicleId);

            if (auction != null && auction.Status == true)
            {
                throw new ApplicationException("Auction already active.");
            }

            if (auction != null && auction.OwnerUserId != ownerUserId)
            {
                throw new ApplicationException("You are not the owner of this auction.");
            }

            auction = new Auction(vehicleId, vehicle.StartingBid);
            auction.Status = true;
            auction.StartedAt = DateTime.UtcNow;
            auction.OwnerUserId = vehicle.OwnerUserId;
            auction.CurrentHighestBid = vehicle.StartingBid;
            await _uow.Auctions.AddOrUpdateAsync(auction);
            await _uow.CommitAsync();
        }

        /// <summary>
        /// Closes an existing auction.
        /// </summary>
        /// <param name="auctionId">The identifier of the auction to close.</param>
        /// <param name="ownerUserId">The user id of the owner requesting the close.</param>
        /// <returns>A task that completes when the auction has been closed.</returns>
        /// <exception cref="ApplicationException">Thrown when the auction is not found, not active, or requested by a non-owner.</exception>
        public async Task CloseAuctionAsync(int auctionId, string ownerUserId)
        {
            var auction = await _uow.Auctions.GetByIdAsync(auctionId);
            if (auction == null)
            {
                throw new ApplicationException("Auction not found.");
            }

            if (auction.Status != true)
            {
                throw new ApplicationException("Auction must be active to close.");
            }

            if (auction.OwnerUserId != ownerUserId)
            {
                throw new ApplicationException("You are not the owner of this auction.");
            }

            auction.Status = false;
            auction.ClosedAt = DateTime.UtcNow;
            auction.WinnerUserId = auction.Bids?.OrderByDescending(b => b.Amount).FirstOrDefault()?.BidderUserId;
            auction.WinningBid = auction.Bids?.OrderByDescending(b => b.Amount).FirstOrDefault()?.Amount;
            auction.CurrentHighestBid = auction.WinningBid ?? auction.CurrentHighestBid;
            await _uow.Auctions.AddOrUpdateAsync(auction);
            await _uow.CommitAsync();
        }

        #region commented out code
        //public async Task<List<Auction>> GetAllActiveAsync()
        //{
        //    var auctions = await _auctionRepo.GetAllActiveAsync();
        //    if (auctions == null)
        //    {
        //        throw new ApplicationException("No active auction is available at the moment.");
        //    }
        //    return auctions;
        //}

        //public async Task<Auction> GetAuctionByIdAsync(int auctionId)
        //{
        //    var auction = await _auctionRepo.GetByIdAsync(auctionId);
        //    if (auction == null)
        //    {
        //        throw new ApplicationException("Auction not found.");
        //    }
        //    return auction;
        //}

        //public async Task<Auction> GetAuctionByVehicleIdAsync(int vehicleId)
        //{
        //    var vehicle = await _vehicleRepo.GetByIdAsync(vehicleId);
        //    if (vehicle == null)
        //    {
        //        throw new ApplicationException("Vehicle not found.");
        //    }

        //    var auction = await _auctionRepo.GetByVehicleIdAsync(vehicleId);
        //    if (auction == null)
        //    {
        //        throw new ApplicationException("Auction not found.");
        //    }
        //    return auction;
        //}

        #endregion
    }
}
