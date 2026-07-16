using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Infrastructure.DataBase;
using CarAuctionManagementSystem.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Infrastructure.Tests
{
    public class BidRepositoryTests
    {
        private BidRepository _repo;

        [SetUp]
        public void Setup()
        {
            InMemoryDatabase.Auctions = new List<Auction>();
            _repo = new BidRepository();
        }

        [Test]
        public async Task AddAsync_ShouldAddBid_AndUpdateHighestBid()
        {
            var auction = new Auction(1, 100)
            {
                Bids = new List<Bid>(),
                CurrentHighestBid = 100
            };

            InMemoryDatabase.Auctions.Add(auction);

            var bid = new Bid
            {
                AuctionId = 1,
                Amount = 250,
                BidderUserId = "userA"
            };

            await _repo.AddAsync(bid);

            Assert.AreEqual(1, auction.Bids.Count);
            Assert.AreEqual(250, auction.CurrentHighestBid);
        }

        [Test]
        public async Task AddAsync_ShouldUpdateHighestBid_WhenMultipleBidsAdded()
        {
            var auction = new Auction(1, 100)
            {
                Bids = new List<Bid>(),
                CurrentHighestBid = 100
            };

            InMemoryDatabase.Auctions.Add(auction);

            await _repo.AddAsync(new Bid { AuctionId = 1, Amount = 150 });
            await _repo.AddAsync(new Bid { AuctionId = 1, Amount = 300 });

            Assert.AreEqual(2, auction.Bids.Count);
            Assert.AreEqual(300, auction.CurrentHighestBid);
        }

        [Test]
        public async Task GetByAuctionIdAsync_ShouldReturnAllBids()
        {
            var auction = new Auction(1, 100)
            {
                Bids = new List<Bid>
                {
                    new Bid { AuctionId = 1, Amount = 200 },
                    new Bid { AuctionId = 1, Amount = 300 }
                }
            };

            InMemoryDatabase.Auctions.Add(auction);

            var result = await _repo.GetByAuctionIdAsync(1);

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetByAuctionIdAsync_ShouldThrow_WhenAuctionNotFound()
        {
            Assert.ThrowsAsync<InvalidOperationException>(() =>
                _repo.GetByAuctionIdAsync(999));
        }

    }
}
