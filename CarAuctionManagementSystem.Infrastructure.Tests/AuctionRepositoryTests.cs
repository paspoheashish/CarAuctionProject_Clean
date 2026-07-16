using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Infrastructure.DataBase;
using CarAuctionManagementSystem.Infrastructure.Repositories;

namespace CarAuctionManagementSystem.Infrastructure.Tests
{
    public class AuctionRepositoryTests
    {
        private AuctionRepository _repo;

        [SetUp]
        public void Setup()
        {
            // Reset in-memory DB before each test
            InMemoryDatabase.Auctions = new List<Auction>();
            _repo = new AuctionRepository();
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllAuctions()
        {
            InMemoryDatabase.Auctions.Add(new Auction(1, 100));
            InMemoryDatabase.Auctions.Add(new Auction(2, 200));

            var result = await _repo.GetAllAsync();

            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetAllActiveAsync_ShouldReturnOnlyActiveAuctions()
        {
            InMemoryDatabase.Auctions.Add(new Auction(1, 100) { Status = true });
            InMemoryDatabase.Auctions.Add(new Auction(2, 200) { Status = false });

            var result = await _repo.GetAllActiveAsync();

            Assert.AreEqual(1, result.Count);
            Assert.IsTrue(result[0].Status);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnAuction_WhenExists()
        {
            var auction = new Auction(1, 100);
            InMemoryDatabase.Auctions.Add(auction);

            var result = await _repo.GetByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WhenNotFound()
        {
            var result = await _repo.GetByIdAsync(99);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetByVehicleIdAsync_ShouldReturnAuction_WhenExists()
        {
            var auction = new Auction(10, 100) { VehicleId = 10 };
            InMemoryDatabase.Auctions.Add(auction);

            var result = await _repo.GetByVehicleIdAsync(10);

            Assert.IsNotNull(result);
            Assert.AreEqual(10, result.VehicleId);
        }

        [Test]
        public async Task GetByVehicleIdAsync_ShouldReturnNull_WhenNotFound()
        {
            var result = await _repo.GetByVehicleIdAsync(999);

            Assert.IsNull(result);
        }

        [Test]
        public async Task AddOrUpdateAsync_ShouldAddNewAuction_WhenNotExists()
        {
            var auction = new Auction(1, 100);

            await _repo.AddOrUpdateAsync(auction);

            Assert.AreEqual(1, InMemoryDatabase.Auctions.Count);
            Assert.AreEqual(1, InMemoryDatabase.Auctions[0].Id);
        }


    }
}