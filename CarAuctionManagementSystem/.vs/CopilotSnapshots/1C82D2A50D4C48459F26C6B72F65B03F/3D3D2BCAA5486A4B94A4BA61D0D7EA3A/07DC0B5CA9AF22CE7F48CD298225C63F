using AuctionService.Application.Interfaces;
using AuctionService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces.Repositories;

namespace AuctionService.Application.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItemServiceClient _itemClient;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidRepository _bidRepository;

        public AuctionService(IUnitOfWork unitOfWork, IItemServiceClient itemClient, IAuctionRepository auctionRepository, IBidRepository bidRepository )
        {
            _unitOfWork = unitOfWork;
            _itemClient = itemClient;
            _auctionRepository = auctionRepository;
            _bidRepository = bidRepository;
        }

        public async Task<Auction> StartAuctionAsync(long vehicleId)
        {
            if (!await _itemClient.VehicleExists(vehicleId))
                throw new Exception("Vehicle does not exist.");

            if (await _auctionRepository.GetActive(vehicleId, true) != null)
                throw new Exception("Auction already active for this vehicle.");

            var auction = new Auction
            {
                VehicleId = vehicleId,
                IsActive = true,
                CurrentBid = 0,
                StartedAt = DateTime.UtcNow
            };

            await _auctionRepository.Add(auction);
            await _unitOfWork.SaveAsync();
            return auction;
        }

        public async Task<Auction> CloseAuctionAsync(long auctionId)
        {
            var auction = await _auctionRepository.Get(auctionId);
            if (auction == null) throw new Exception("Auction not found.");
            if (!auction.IsActive) throw new Exception("Auction already closed.");

            auction.IsActive = false;
            auction.ClosedAt = DateTime.UtcNow;

            await _unitOfWork.SaveAsync();
            return auction;
        }

        public async Task<Auction?> PlaceBidAsync(long auctionId, decimal amount, long bidder)
        {
            var auction = await _auctionRepository.Get(auctionId);

            if (auction == null)
                throw new Exception("Auction not found.");

            if (!auction.IsActive)
                throw new Exception("Auction is not active.");

            if (amount <= auction.CurrentBid)
                throw new Exception("Bid must be higher than current bid.");

            auction.Bids = await _bidRepository.GetBidsForAuction(auctionId);
            var bid = new Bid
            {
                AuctionId = auctionId,
                Amount = amount,
                Bidder = bidder,
                Timestamp = DateTime.UtcNow
            };

            auction.CurrentBid = amount;
            auction.HighestBidder = bidder;
            auction.Bids.Add(bid);

            await _unitOfWork.SaveAsync();
            return await GetAuctionAsync(auctionId);
        }

        public async Task<Auction?> GetAuctionAsync(long id) {
            return await _auctionRepository.GetAuctionWithBids(id);
        }
    }

}
