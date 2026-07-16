using CarAuctionManagementSystem.Application.Interfaces;
using CarAuctionManagementSystem.Application.Services;
using CarAuctionManagementSystem.DTOs;
using CarAuctionManagementSystem.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionManagementSystem.Controllers
{
    /// <summary>
    /// Provides HTTP endpoints for bid-related operations.
    /// Delegates business logic to the application service layer.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BidController"/> class.
        /// </summary>
        /// <param name="bidService">Service responsible for bid operations.</param>
        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        /// <summary>
        /// Places a bid on an auction.
        /// </summary>
        /// <param name="request">DTO containing bid placement details.</param>
        /// <returns>Returns 200 OK when the bid is successfully placed.</returns>
        [HttpPost("place")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult PlaceBid([FromBody] PlaceBidRequestDto request)
        {
            // Extract user ID from gateway header
            request.UserId = Request.Headers.TryGetValue("X-User-Id", out var userId)
                ? userId.ToString()
                : string.Empty;

            _bidService.PlaceBid(request.AuctionId, request.UserId, request.Amount);

            return Ok("Bid placed successfully.");
        }

        /// <summary>
        /// Retrieves all bids for a given auction.
        /// </summary>
        /// <param name="auctionId">The ID of the auction.</param>
        /// <returns>Returns a list of bids associated with the auction.</returns>
        [HttpGet("auction/{auctionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<BidResponseDto>> GetBids(string auctionId)
        {
            var bids = _bidService.GetBids(auctionId)
                                  .Select(BidMapper.ToDto)
                                  .ToList();

            return Ok(bids);
        }

        /// <summary>
        /// Retrieves the highest bid for a given auction.
        /// </summary>
        /// <param name="auctionId">The ID of the auction.</param>
        /// <returns>Returns the highest bid if available; otherwise 404 Not Found.</returns>
        [HttpGet("highest/{auctionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<BidResponseDto> GetHighestBid(string auctionId)
        {
            var highestBid = _bidService.GetHighestBid(auctionId);
            return Ok(BidMapper.ToDto(highestBid));
        }
    }
}
