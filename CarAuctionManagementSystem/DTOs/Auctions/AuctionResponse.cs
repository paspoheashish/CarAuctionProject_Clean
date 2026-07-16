using CarAuctionManagementSystem.Domain.Enums;

namespace CarAuctionManagementSystem.DTOs.Auctions
{
    public record AuctionResponse
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public bool Status { get; set; }
        public decimal StartingBid { get; set; }
        public decimal CurrentHighestBid { get; set; }
        public string? OwnerUserId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string? WinnerUserId { get; set; }
        public decimal? WinningBid { get; set; }
    }
}
