using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.DTOs;

namespace CarAuctionManagementSystem.Mappers
{
    public static class BidMapper
    {
        public static BidResponseDto ToDto(Bid bid) =>
            new()
            {
                BidId = bid.Id,
                AuctionId = bid.AuctionId,
                BidderUserId = bid.BidderUserId,
                Amount = bid.Amount,
                PlacedAt = bid.PlacedAt.ToString("dd MMM yyyy HH:mm:ss")
            };
    }
}
