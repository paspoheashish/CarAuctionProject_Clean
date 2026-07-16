using System.ComponentModel.DataAnnotations;

namespace CarAuctionManagementSystem.DTOs.Bids
{
    public record PlaceBidRequest
    {
        [Required]
        public int AuctionId { get; set; }
        [Required]
        public string BidderUserId { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }

}
