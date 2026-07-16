using AutoMapper;
using CarAuctionManagementSystem.Application.UseCases.Bids;
using CarAuctionManagementSystem.Application.UseCases.Bids.Command;
using CarAuctionManagementSystem.Application.UseCases.Bids.Query;
using CarAuctionManagementSystem.DTOs.Bids;
using CarAuctionManagementSystem.Mapping;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionManagementSystem.Controllers
{
    /// <summary>
    /// Controller for managing bid-related endpoints.
    /// </summary>
    [ApiController]
    [Route("api/bids")]
    public class BidsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BidsController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR mediator.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public BidsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Places a bid on an auction.
        /// </summary>
        /// <param name="request">The bid placement request.</param>
        /// <returns>An <see cref="IActionResult"/> containing the created bid id.</returns>
        [HttpPost("place")]
        public async Task<IActionResult> PlaceBid([FromBody] PlaceBidRequest request)
        {
            var command = new PlaceBidCommand(
                request.AuctionId,
                request.BidderUserId,
                request.Amount
            );

            var bidId = await _mediator.Send(command);

            return Ok(new { bidId });
        }

        /// <summary>
        /// Retrieves bids for a specific auction.
        /// </summary>
        /// <param name="auctionId">The auction identifier.</param>
        /// <returns>An <see cref="IActionResult"/> containing a collection of bids.</returns>
        [HttpGet("{auctionId}")]
        public async Task<IActionResult> GetBids(int auctionId)
        {
            var query = new GetBidsQuery(auctionId);

            var bids = await _mediator.Send(query);

            var response = bids.Select(_mapper.Map<BidResponse>);

            return Ok(response);
        }
    }
}
