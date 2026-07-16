using ItemService.Application.DTOs;
using ItemService.Application.Interfaces;
using ItemService.Domain.Entities;
using ItemService.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces.Repositories;

namespace ItemService.Application.Services
{
    public class VechicleService : IVechicleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVehicleRepository _vehicleRepository;

        public VechicleService(IUnitOfWork unitOfWork, IVehicleRepository vehicleRepository)
        {
            _unitOfWork = unitOfWork;
            _vehicleRepository = vehicleRepository;
        }
        public async Task<Vehicle> AddVehicleAsync(AddVehicleDto dto)
        {
            // Check duplicate ID
            if (await _vehicleRepository.GetAsync(dto.Id) != null)
                throw new Exception("Vehicle ID already exists.");

            Vehicle? vehicle = null;

            if (dto.Type == "Hatchback")
            {
                vehicle = new Hatchback
                {
                    Id = dto.Id,
                    Manufacturer = dto.Manufacturer,
                    Model = dto.Model,
                    Year = dto.Year,
                    StartingBid = dto.StartingBid,
                    NumberOfDoors = dto.NumberOfDoors ?? 4
                };
            }
            else if (dto.Type == "Sedan")
            {
                vehicle = new Sedan
                {
                    Id = dto.Id,
                    Manufacturer = dto.Manufacturer,
                    Model = dto.Model,
                    Year = dto.Year,
                    StartingBid = dto.StartingBid,
                    NumberOfDoors = dto.NumberOfDoors ?? 4
                };
            }
            else if (dto.Type == "SUV")
            {
                vehicle = new SUV
                {
                    Id = dto.Id,
                    Manufacturer = dto.Manufacturer,
                    Model = dto.Model,
                    Year = dto.Year,
                    StartingBid = dto.StartingBid,
                    NumberOfSeats = dto.NumberOfSeats ?? 5
                };
            }
            else if (dto.Type == "Truck")
            {
                vehicle = new Truck
                {
                    Id = dto.Id,
                    Manufacturer = dto.Manufacturer,
                    Model = dto.Model,
                    Year = dto.Year,
                    StartingBid = dto.StartingBid,
                    LoadCapacity = dto.LoadCapacity ?? 1000
                };
            }
            else
            {
                throw new Exception("Invalid vehicle type");
            }
            
            await _vehicleRepository.AddVehicleAsync(vehicle);
            await _unitOfWork.SaveAsync();

            return vehicle;
        }

        public async Task<IEnumerable<Vehicle>> SearchAsync(string? type, string? manufacturer, string? model, int? year)
        {
            return await _vehicleRepository.SearchAsync(type, manufacturer, model, year);
        }

        public async Task<Vehicle?> GetAsync(long id)
        {
            var vehicle = await _vehicleRepository.GetAsync(id);

            return vehicle;
        }
    }
}
