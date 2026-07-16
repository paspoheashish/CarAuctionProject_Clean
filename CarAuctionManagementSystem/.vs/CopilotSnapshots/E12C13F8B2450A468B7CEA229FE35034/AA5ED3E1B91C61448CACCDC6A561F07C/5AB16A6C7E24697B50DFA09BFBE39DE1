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
    /// Handles retrieval of all active auctions.
    /// </summary>
    public class GetAllActiveAuctionsHandler
        : IRequestHandler<GetAllActiveAuctionsQuery, List<Auction>>
    {
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllActiveAuctionsHandler"/> class.
        /// </summary>
        /// <param name="uow">The unit of work for repository access.</param>
        public GetAllActiveAuctionsHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Handles the request to retrieve all active auctions.
        /// </summary>
        /// <param name="request">The request message (ignored).</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of active <see cref="Auction"/> entities.</returns>
        public async Task<List<Auction>> Handle(GetAllActiveAuctionsQuery request, CancellationToken cancellationToken)
        {
            var auctions = await _uow.Auctions.GetAllActiveAsync();

            if (auctions == null || !auctions.Any())
                throw new ApplicationException("No active auction is available at the moment.");

            return auctions;
        }
    }

}
