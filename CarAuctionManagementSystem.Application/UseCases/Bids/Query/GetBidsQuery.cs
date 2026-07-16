using CarAuctionManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.UseCases.Bids.Query
{
    public record GetBidsQuery(int AuctionId) : IRequest<IEnumerable<Bid>>;
}
