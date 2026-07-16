using CarAuctionManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.UseCases.Auctions.Query
{
    public record GetAuctionByIdQuery(int AuctionId) : IRequest<Auction>;

}
