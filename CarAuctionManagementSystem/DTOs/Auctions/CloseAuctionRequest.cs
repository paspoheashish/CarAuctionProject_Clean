using System.ComponentModel.DataAnnotations;

namespace CarAuctionManagementSystem.DTOs.Auctions
{
    public record CloseAuctionRequest
    {
        [Required]
        public int AuctionId { get; set; }
    }
}
