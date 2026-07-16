using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Domain.Entities
{
    public static class BidIdGenerator
    {
        private static int LastBidId { get; set; } = 0;
        public static int GetNextBidId()
        {
            LastBidId++;
            return LastBidId;
        }
    }
    public class Bid
    {
        public int Id { get; set; }
        public int AuctionId { get; set; }
        public string BidderUserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }

        public Bid() { }
        public Bid(int auctionId, string bidderUserId, decimal amount)
        {
            Id = BidIdGenerator.GetNextBidId();
            AuctionId = auctionId;
            BidderUserId = bidderUserId;
            Amount = amount;
            Timestamp = DateTime.UtcNow;
        }
    }

}
