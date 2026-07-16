namespace CarAuctionManagementSystem.DTOs
{
    public class BidResponseDto
    {
        public string BidId { get; set; }
        public string AuctionId { get; set; }
        public string BidderUserId { get; set; }
        public decimal Amount { get; set; }
        public string? PlacedAt { get; set; }
    }
}
