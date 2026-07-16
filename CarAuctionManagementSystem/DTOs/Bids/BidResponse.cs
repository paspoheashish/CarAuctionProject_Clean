namespace CarAuctionManagementSystem.DTOs.Bids
{
    public record BidResponse
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public string BidderUserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
