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
    /// Handles retrieval of an auction by its associated vehicle id.
    /// </summary>
    public class GetAuctionByVehicleIdHandler
        : IRequestHandler<GetAuctionByVehicleIdQuery, Auction>
    {
        private readonly IUnitOfWork _uow;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetAuctionByVehicleIdHandler"/> class.
        /// </summary>
        /// <param name="uow">The unit of work for repository access.</param>
        public GetAuctionByVehicleIdHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Handles the request to retrieve an auction by vehicle id.
        /// </summary>
        /// <param name="request">The get-auction-by-vehicle request.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The <see cref="Auction"/> for the vehicle.</returns>
        public async Task<Auction> Handle(GetAuctionByVehicleIdQuery request, CancellationToken cancellationToken)
        {
            var vehicle = await _uow.Vehicles.GetByIdAsync(request.VehicleId);

            if (vehicle == null)
                throw new ApplicationException("Vehicle not found.");

            var auction = await _uow.Auctions.GetByVehicleIdAsync(request.VehicleId);

            if (auction == null)
                throw new ApplicationException("Auction not found.");

            return auction;
        }
    }

}
