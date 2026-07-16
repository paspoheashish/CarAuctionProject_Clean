using AutoMapper;
using CarAuctionManagementSystem.Application.Interfaces.Services;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Enums;
using CarAuctionManagementSystem.DTOs.Vehicles;
using CarAuctionManagementSystem.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionManagementSystem.Controllers
{
    /// <summary>
    /// Controller for managing vehicle-related endpoints.
    /// </summary>
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _service;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="service">The vehicle service.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public VehiclesController(IVehicleService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new vehicle for the current user.
        /// </summary>
        /// <param name="request">The vehicle creation request.</param>
        /// <returns>An <see cref="IActionResult"/> containing the new vehicle id.</returns>
        [HttpPost]
        public async Task<IActionResult> AddVehicle(CreateVehicleRequest request)
        {
            var ownerUserId = Request.Headers.TryGetValue("X-User-Id", out var userId) ? userId.ToString() : string.Empty;
            ownerUserId = "owner1";

            if (string.IsNullOrEmpty(ownerUserId))
            {
                return BadRequest("Missing X-User-Id header.");
            }

            request.OwnerUserId = ownerUserId;

            var vehicle = _mapper.Map<Vehicle>(request);

            await _service.AddVehicleAsync(vehicle);
            
            return Ok(new { vehicle.Id });
        }

        /// <summary>
        /// Searches for vehicles using the provided query parameters.
        /// </summary>
        /// <param name="request">The search request parameters.</param>
        /// <returns>An <see cref="IActionResult"/> containing matching vehicles.</returns>
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchVehiclesRequest request)
        {
            var vehicleType = Enum.TryParse<VehicleType>(request.Type, true, out var parsedType) ? parsedType : (VehicleType?)null;
            var vehicles = await _service.SearchAsync(vehicleType, request.Manufacturer, request.Model, request.Year);

            var result = _mapper.Map<List<VehicleResponse>>(vehicles);

            return Ok(result);
        }
    }
}
