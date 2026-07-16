using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarAuctionManagementSystem.DTOs
{
    public class PlaceBidRequestDto
    {
        [Required]
        public string AuctionId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [JsonIgnore]
        public string? UserId { get; set; } // or from auth context
    }
}
