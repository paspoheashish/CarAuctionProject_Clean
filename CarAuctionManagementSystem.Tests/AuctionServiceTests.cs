using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CarAuctionManagementSystem.Application.Services;
using CarAuctionManagementSystem.Application.Interfaces.Repositories;
using CarAuctionManagementSystem.Domain.Entities;

namespace CarAuctionManagementSystem.Tests
{
    public class AuctionServiceTests
    {
        private Mock<IUnitOfWork> _uowMock;
        private Mock<IAuctionRepository> _auctionRepoMock;
        private Mock<IVehicleRepository> _vehicleRepoMock;
        private AuctionService _service;

        [SetUp]
        public void Setup()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _auctionRepoMock = new Mock<IAuctionRepository>();
            _vehicleRepoMock = new Mock<IVehicleRepository>();

            _uowMock.Setup(u => u.Auctions).Returns(_auctionRepoMock.Object);
            _uowMock.Setup(u => u.Vehicles).Returns(_vehicleRepoMock.Object);

            _service = new AuctionService(_uowMock.Object);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAuctions()
        {
            var auctions = new List<Auction> { new Auction(1, 100) };

            _auctionRepoMock.Setup(r => r.GetAllAsync())
                            .ReturnsAsync(auctions);

            var result = await _service.GetAllAsync();

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void GetAllAsync_ShouldThrow_WhenNull()
        {
            _auctionRepoMock.Setup(r => r.GetAllAsync())
                            .ReturnsAsync((List<Auction>)null);

            Assert.ThrowsAsync<ApplicationException>(() => _service.GetAllAsync());
        }

        [Test]
        public void StartAuctionAsync_ShouldThrow_WhenVehicleNotFound()
        {
            _vehicleRepoMock.Setup(r => r.GetByIdAsync(10))
                            .ReturnsAsync((Vehicle)null);

            Assert.ThrowsAsync<ApplicationException>(() =>
                _service.StartAuctionAsync(10, "owner1"));
        }

        [Test]
        public void StartAuctionAsync_ShouldThrow_WhenAuctionAlreadyActive()
        {
            //var vehicle = new Vehicle { Id = 10, StartingBid = 100, OwnerUserId = "owner1" };
            var vehicle = new Vehicle();
            vehicle.Id = 10;
            vehicle.StartingBid = 100;
            vehicle.OwnerUserId = "owner1";
            var auction = new Auction(10, 100) { Status = true };

            _vehicleRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(vehicle);
            _auctionRepoMock.Setup(r => r.GetByVehicleIdAsync(10)).ReturnsAsync(auction);

            Assert.ThrowsAsync<ApplicationException>(() =>
                _service.StartAuctionAsync(10, "owner1"));
        }

        [Test]
        public void StartAuctionAsync_ShouldThrow_WhenOwnerMismatch()
        {
            var vehicle = new Vehicle { Id = 10, StartingBid = 100, OwnerUserId = "owner1" };
            var auction = new Auction(10, 100) { Status = false, OwnerUserId = "owner1" };

            _vehicleRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(vehicle);
            _auctionRepoMock.Setup(r => r.GetByVehicleIdAsync(10)).ReturnsAsync(auction);

            Assert.ThrowsAsync<ApplicationException>(() =>
                _service.StartAuctionAsync(10, "differentOwner"));
        }

        [Test]
        public async Task StartAuctionAsync_ShouldStartAuctionSuccessfully()
        {
            var vehicle = new Vehicle { Id = 10, StartingBid = 100, OwnerUserId = "owner1" };

            _vehicleRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(vehicle);
            _auctionRepoMock.Setup(r => r.GetByVehicleIdAsync(10)).ReturnsAsync((Auction)null);

            _auctionRepoMock.Setup(r => r.AddOrUpdateAsync(It.IsAny<Auction>()))
                            .Returns(Task.CompletedTask);

            _uowMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            await _service.StartAuctionAsync(10, "owner1");

            _auctionRepoMock.Verify(r => r.AddOrUpdateAsync(It.IsAny<Auction>()), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Test]
        public void CloseAuctionAsync_ShouldThrow_WhenAuctionNotFound()
        {
            _auctionRepoMock.Setup(r => r.GetByIdAsync(5))
                            .ReturnsAsync((Auction)null);

            Assert.ThrowsAsync<ApplicationException>(() =>
                _service.CloseAuctionAsync(5, "owner1"));
        }

        [Test]
        public void CloseAuctionAsync_ShouldThrow_WhenAuctionNotActive()
        {
            var auction = new Auction(10, 100) { Status = false };

            _auctionRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(auction);

            Assert.ThrowsAsync<ApplicationException>(() =>
                _service.CloseAuctionAsync(10, "owner1"));
        }

        [Test]
        public void CloseAuctionAsync_ShouldThrow_WhenOwnerMismatch()
        {
            var auction = new Auction(10, 100)
            {
                Status = true,
                OwnerUserId = "owner1"
            };

            _auctionRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(auction);

            Assert.ThrowsAsync<ApplicationException>(() =>
                _service.CloseAuctionAsync(10, "differentOwner"));
        }

        [Test]
        public async Task CloseAuctionAsync_ShouldCloseAuctionSuccessfully()
        {
            var auction = new Auction(10, 100)
            {
                Status = true,
                OwnerUserId = "owner1",
                Bids = new List<Bid>
                {
                    new Bid { Amount = 200, BidderUserId = "userA" },
                    new Bid { Amount = 300, BidderUserId = "userB" }
                }
            };

            _auctionRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(auction);
            _auctionRepoMock.Setup(r => r.AddOrUpdateAsync(It.IsAny<Auction>()))
                            .Returns(Task.CompletedTask);

            _uowMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            await _service.CloseAuctionAsync(10, "owner1");

            Assert.False(auction.Status);
            Assert.AreEqual("userB", auction.WinnerUserId);
            Assert.AreEqual(300, auction.WinningBid);

            _auctionRepoMock.Verify(r => r.AddOrUpdateAsync(It.IsAny<Auction>()), Times.Once);
            _uowMock.Verify(u => u.CommitAsync(), Times.Once);
        }

    }
}
