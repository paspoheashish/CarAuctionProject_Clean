using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CarAuctionManagementSystem.DTOs
{
    public class VehicleResponseDto
    {        
        public string Type { get; set; }
        public string Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal StartingBid { get; set; }
        public string? OwnerUserId { get; set; }       
    }
}
