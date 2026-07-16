using CarAuctionManagementSystem.Application.Interfaces;
using CarAuctionManagementSystem.Application.Services;
using CarAuctionManagementSystem.DTOs;
using CarAuctionManagementSystem.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionManagementSystem.Controllers
{
    /// <summary>
    /// Provides HTTP endpoints for auction-related operations.
    /// Delegates business logic to the application service layer.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionController"/> class.
        /// </summary>
        /// <param name="auctionService">Service responsible for auction operations.</param>
        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        /// <summary>
        /// Retrieves all auctions (active + inactive).
        /// </summary>
        /// <returns>Returns a list of all auctions.</returns>
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<AuctionResponseDto>> GetAuctions()
        {
            var auctions = _auctionService.GetAuctions(isActive: false);

            var response = auctions
                .Select(AuctionMapper.MapToDto)
                .ToList();

            return Ok(response);
        }

        /// <summary>
        /// Retrieves only active auctions.
        /// </summary>
        /// <returns>Returns a list of active auctions.</returns>
        [HttpGet("allactive")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<AuctionResponseDto>> GetActiveAuctions()
        {
            var auctions = _auctionService.GetAuctions(isActive: true);

            var response = auctions
                .Select(AuctionMapper.MapToDto)
                .ToList();

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific auction by vehicle ID.
        /// </summary>
        /// <param name="vehicleId">The ID of the vehicle associated with the auction.</param>
        /// <returns>Returns the auction details.</returns>
        [HttpGet("{vehicleId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<AuctionResponseDto> GetAuction(string vehicleId)
        {
            var auction = _auctionService.GetAuction(vehicleId);
            var response = AuctionMapper.MapToDto(auction);

            return Ok(response);
        }

        /// <summary>
        /// Starts an auction for a given vehicle.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID for which the auction should start.</param>
        /// <returns>Returns confirmation message.</returns>
        [HttpPost("start")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult StartAuction([FromBody] string vehicleId)
        {
            var ownerUserId = Request.Headers.TryGetValue("X-User-Id", out var userId)
                ? userId.ToString()
                : string.Empty;

            if(string.IsNullOrEmpty(ownerUserId))
            {
                return BadRequest("Missing X-User-Id header.");
            };

            _auctionService.StartAuction(vehicleId, ownerUserId);

            return Ok($"Auction started for VehicleId {vehicleId}");
        }

        /// <summary>
        /// Closes an auction for a given vehicle.
        /// </summary>
        /// <param name="vehicleId">Vehicle ID for which the auction should close.</param>
        /// <returns>Returns confirmation message.</returns>
        [HttpPost("close")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult CloseAuction([FromBody] string vehicleId)
        {
            var ownerUserId = Request.Headers.TryGetValue("X-User-Id", out var userId)
                ? userId.ToString()
                : string.Empty;

            _auctionService.CloseAuction(vehicleId, ownerUserId);

            return Ok($"Auction closed for VehicleId {vehicleId}");
        }
    }
}
