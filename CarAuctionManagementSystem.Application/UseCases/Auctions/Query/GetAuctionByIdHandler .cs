using CarAuctionManagementSystem.Application.Interfaces.Repositories;
using CarAuctionManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.UseCases.Auctions.Query
{
    /// <summary>
    /// Handles retrieval of a single auction by its id.
    /// </summary>
    public class GetAuctionByIdHandler
        : IRequestHandler<GetAuctionByIdQuery, Auction>
    {
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuctionByIdHandler"/> class.
        /// </summary>
        /// <param name="uow">The unit of work for repository access.</param>
        public GetAuctionByIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Handles the request to retrieve an auction by id.
        /// </summary>
        /// <param name="request">The get-auction-by-id request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The <see cref="Auction"/> matching the id.</returns>
        public async Task<Auction> Handle(GetAuctionByIdQuery request, CancellationToken cancellationToken)
        {
            var auction = await _uow.Auctions.GetByIdAsync(request.AuctionId);

            if (auction == null)
                throw new ApplicationException("Auction not found.");

            return auction;
        }
    }

}
