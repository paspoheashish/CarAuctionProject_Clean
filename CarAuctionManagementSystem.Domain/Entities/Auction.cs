using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAuctionManagementSystem.Domain.Enums;

namespace CarAuctionManagementSystem.Domain.Entities
{
    public static class AuctionIdGenerator
    {
        private static int LastAuctionId { get; set; } = 0;
        public static int GetNextAuctionId()
        {
            LastAuctionId++;
            return LastAuctionId;
        }
    }
    public class Auction
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
        public List<Bid> Bids { get; set; }

        public Auction() { }
        
        public Auction(int vehicleId, decimal startingBid)
        {
            Id = AuctionIdGenerator.GetNextAuctionId();
            VehicleId = vehicleId;
            Status = false;
            StartingBid = startingBid;
            CurrentHighestBid = startingBid;
            Bids = new List<Bid>();
        }
    }
}
