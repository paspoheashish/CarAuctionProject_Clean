using CarAuctionManagementSystem.Domain.Enums;

namespace CarAuctionManagementSystem.DTOs.Vehicles
{
    public record VehicleResponse
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal StartingBid { get; set; }
        public string OwnerUserId { get; set; }
        public string CreatedOn { get; set; }
    }
}
