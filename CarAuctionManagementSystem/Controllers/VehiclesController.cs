using CarAuctionManagementSystem.Application.Interfaces;
using CarAuctionManagementSystem.Application.Mappers;
using CarAuctionManagementSystem.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CarAuctionManagementSystem.Controllers
{
    /// <summary>
    /// Provides HTTP endpoints for vehicle-related operations.
    /// Delegates business logic to the application service layer.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        /// <param name="vehicleService">Service responsible for vehicle operations.</param>
        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        /// <summary>
        /// Creates a new vehicle record.
        /// </summary>
        /// <param name="dto">Vehicle data transfer object containing creation details.</param>
        /// <returns>Returns 200 OK when the vehicle is successfully created.</returns>
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateVehicle([FromBody] VehicleRequestDto dto)
        {
            // Extract owner user ID from gateway header
            if (Request.Headers.TryGetValue("X-User-Id", out var userId))
            {
                dto.OwnerUserId = userId.ToString();
            }
            else
            {
                throw new ApplicationException("X-User-Id is missing in the request headers.");
            }

            var vehicle = VehicleMapper.ToDomain(dto);

            _vehicleService.AddVehicle(vehicle);

            return Ok("Vehicle added successfully.");
        }

        /// <summary>
        /// Searches for vehicles using optional filter criteria.
        /// </summary>
        /// <param name="type">Optional vehicle type filter.</param>
        /// <param name="manufacturer">Optional manufacturer filter.</param>
        /// <param name="model">Optional model filter.</param>
        /// <param name="year">Optional year filter.</param>
        /// <returns>Returns a list of vehicles matching the provided criteria.</returns>
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult SearchVehicles(
            [FromQuery] string? type,
            [FromQuery] string? manufacturer,
            [FromQuery] string? model,
            [FromQuery] int? year)
        {
            var response = _vehicleService.Search(type, manufacturer, model, year).Select(VehicleMapper.MapToDto)
                .ToList(); 

            return Ok(response);
        }
    }
}
