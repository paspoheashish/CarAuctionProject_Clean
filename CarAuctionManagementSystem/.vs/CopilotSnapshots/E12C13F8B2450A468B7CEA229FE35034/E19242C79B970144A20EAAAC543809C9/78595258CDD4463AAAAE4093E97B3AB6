using AutoMapper;
using CarAuctionManagementSystem.Application.Interfaces.Services;
using CarAuctionManagementSystem.Application.Services;
using CarAuctionManagementSystem.Application.UseCases.Auctions.Query;
using CarAuctionManagementSystem.Application.UseCases.Bids.Query;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.DTOs.Auctions;
using CarAuctionManagementSystem.DTOs.Vehicles;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionManagementSystem.Controllers
{
    /// <summary>
    /// Controller for managing auctions endpoints.
    /// </summary>
    [ApiController]
    [Route("api/auctions")]
    public class AuctionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuctionService _service;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuctionsController"/> class.
        /// </summary>
        /// <param name="service">The auction service.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        /// <param name="mediator">The MediatR mediator.</param>
        public AuctionsController(IAuctionService service, IMapper mapper, IMediator mediator)
        {
            _service = service;
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all auctions.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing a list of auctions.</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAuctions()
        {
            var auctions = await _service.GetAllAsync();

            var response = _mapper.Map<List<AuctionResponse>>(auctions);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves all active auctions.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing a list of active auctions.</returns>
        [HttpGet("allactive")]
        public async Task<IActionResult> GetActiveAuctions()
        {
            var query = new GetAllActiveAuctionsQuery();
            var auctions = await _mediator.Send(query);
            var response = _mapper.Map<List<AuctionResponse>>(auctions);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific auction by its identifier.
        /// </summary>
        /// <param name="auctionId">The auction identifier.</param>
        /// <returns>An <see cref="IActionResult"/> containing the auction details.</returns>
        [HttpGet("{auctionId}")]
        public async Task<IActionResult> GetAuction(int auctionId)
        {
            var query = new GetAuctionByIdQuery(auctionId);
            var auction = await _mediator.Send(query);
            var response = _mapper.Map<AuctionResponse>(auction);
            return Ok(response);
        }

        /// <summary>
        /// Retrieves an auction by the associated vehicle identifier.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>An <see cref="IActionResult"/> containing the auction for the vehicle.</returns>
        [HttpGet("vehicle/{vehicleId}")]
        public async Task<IActionResult> GetAuctionByVehicleId(int vehicleId)
        {
            var query = new GetAuctionByVehicleIdQuery(vehicleId);
            var auction = await _mediator.Send(query);
            var response = _mapper.Map<AuctionResponse>(auction);
            return Ok(response);
        }

        /// <summary>
        /// Starts a new auction for the specified vehicle.
        /// </summary>
        /// <param name="startAuctionRequest">Request object containing the vehicle id and other start details.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost("start")]
        public async Task<IActionResult> Start(StartAuctionRequest startAuctionRequest)
        {
            var ownerUserId = Request.Headers.TryGetValue("X-User-Id", out var userId) ? userId.ToString() : string.Empty;
            ownerUserId = "owner1";

            if (string.IsNullOrEmpty(ownerUserId))
            {
                return BadRequest("Missing X-User-Id header.");
            }

            await _service.StartAuctionAsync(startAuctionRequest.VehicleId, ownerUserId);
            return Ok();
        }

        /// <summary>
        /// Closes an existing auction.
        /// </summary>
        /// <param name="closeAuctionRequest">Request object containing the auction id to close.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation.</returns>
        [HttpPost("close")]
        public async Task<IActionResult> Close(CloseAuctionRequest closeAuctionRequest)
        {
            var ownerUserId = Request.Headers.TryGetValue("X-User-Id", out var userId) ? userId.ToString() : string.Empty;
            ownerUserId = "owner1";

            if (string.IsNullOrEmpty(ownerUserId))
            {
                return BadRequest("Missing X-User-Id header.");
            }

            await _service.CloseAuctionAsync(closeAuctionRequest.AuctionId, ownerUserId);
            return Ok();
        }
    }
}
