using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.UseCases.Bids.Command
{
    public record PlaceBidCommand(int AuctionId, string BidderUserId, decimal Amount)
        : IRequest<int>;
}
