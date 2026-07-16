using CarAuctionManagementSystem.Application.Interfaces.Repositories;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.UseCases.Bids.Query
{
    /// <summary>
    /// Handles retrieval of bids for a specific auction.
    /// </summary>
    public class GetBidsHandler : IRequestHandler<GetBidsQuery, IEnumerable<Bid>>
    {
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetBidsHandler"/> class.
        /// </summary>
        /// <param name="uow">The unit of work for repository access.</param>
        public GetBidsHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Handles the request to retrieve bids for an auction.
        /// </summary>
        /// <param name="request">The get-bids request containing the auction id.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A collection of <see cref="Bid"/> entities for the auction.</returns>
        public async Task<IEnumerable<Bid>> Handle(GetBidsQuery request, CancellationToken cancellationToken)
        {
            var auction = await _uow.Auctions.GetByIdAsync(request.AuctionId);

            if (auction == null)
            { 
                throw new ApplicationException("Auction not found."); 
            }
            return await _uow.Bids.GetByAuctionIdAsync(request.AuctionId);
        }
    }
}
