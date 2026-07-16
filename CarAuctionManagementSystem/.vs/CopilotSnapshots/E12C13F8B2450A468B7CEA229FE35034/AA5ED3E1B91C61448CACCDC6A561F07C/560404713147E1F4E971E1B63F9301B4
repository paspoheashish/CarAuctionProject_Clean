using CarAuctionManagementSystem.Application.Interfaces.Repositories;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.UseCases.Bids.Command
{
    /// <summary>
    /// Handles placing a bid command and persists the bid.
    /// </summary>
    public class PlaceBidHandler : IRequestHandler<PlaceBidCommand, int>
    {
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaceBidHandler"/> class.
        /// </summary>
        /// <param name="uow">The unit of work for repository access.</param>
        public PlaceBidHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Handles the place-bid command by validating the auction and creating a new bid.
        /// </summary>
        /// <param name="request">The place-bid command.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The id of the created bid.</returns>
        public async Task<int> Handle(PlaceBidCommand request, CancellationToken cancellationToken)
        {
            var auction = await _uow.Auctions.GetByIdAsync(request.AuctionId);

            if (auction == null)
                throw new InvalidOperationException("Auction not found.");

            if (auction != null && auction.Status != true)
                throw new InvalidOperationException("Auction is not active.");

            if (auction != null && request.Amount <= auction.CurrentHighestBid)
                throw new InvalidOperationException("Bid must be higher.");

            var bid = new Bid(request.AuctionId, request.BidderUserId, request.Amount);

            await _uow.Bids.AddAsync(bid);
            return bid.Id;
        }
    }
}
