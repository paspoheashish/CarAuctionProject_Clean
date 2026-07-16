namespace CarAuctionManagementSystem.DTOs
{
    public class AuctionResponseDto
    {
        public string AuctionId { get; set; }
        public string VehicleId { get; set; }

        // Vehicle Info
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Type { get; set; }

        // Auction Info
        public bool IsActive { get; set; }
        public decimal StartingBid { get; set; }
        public decimal? CurrentBid { get; set; }
        public int TotalBids { get; set; }

        public string StartedAt { get; set; }
        public string? ClosedAt { get; set; }

        // Optional: Winner Info
        public string? WinnerUserId { get; set; }
        public decimal? WinningBid { get; set; }
    }
}
