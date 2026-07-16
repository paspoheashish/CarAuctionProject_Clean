using System.ComponentModel.DataAnnotations;

namespace CarAuctionManagementSystem.DTOs.Auctions
{
    public record StartAuctionRequest
    {
        [Required]
        public int VehicleId { get; set; }
    }
}
