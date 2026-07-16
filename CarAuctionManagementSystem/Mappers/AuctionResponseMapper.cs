using CarAuctionManagementSystem.Application.Responses;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.DTOs;

namespace CarAuctionManagementSystem.Mappers
{
    public static class AuctionMapper
    {
        public static AuctionResponseDto MapToDto(AuctionResponse auctionResponse) =>
            new()
            {
                AuctionId = auctionResponse.AuctionId,
                VehicleId = auctionResponse.VehicleId,
                Manufacturer = auctionResponse.Manufacturer,
                Model = auctionResponse.Model,
                Year = auctionResponse.Year,
                Type = auctionResponse.Type,
                IsActive = auctionResponse.IsActive,
                StartingBid = auctionResponse.StartingBid,
                CurrentBid = auctionResponse.CurrentBid,
                TotalBids = auctionResponse.TotalBids,
                StartedAt = auctionResponse.StartedAt,
                ClosedAt = auctionResponse.ClosedAt,
                WinnerUserId = auctionResponse.WinnerUserId,
                WinningBid = auctionResponse.WinningBid
            };

    }
}

