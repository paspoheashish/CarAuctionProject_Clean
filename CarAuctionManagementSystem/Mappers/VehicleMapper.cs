using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.Mappers
{
    public static class VehicleMapper
    {
        public static Vehicle ToDomain(VehicleRequestDto dto)
        {
            return dto.Type.ToLower() switch
            {
                "hatchback" => new Hatchback(
                    dto.Id,
                    dto.Manufacturer,
                    dto.Model,
                    dto.Year,
                    dto.StartingBid,
                    dto.OwnerUserId,
                    dto.NumberOfDoors!.Value),

                "sedan" => new Sedan(
                    dto.Id,
                    dto.Manufacturer,
                    dto.Model,
                    dto.Year,
                    dto.StartingBid,
                    dto.OwnerUserId,
                    dto.NumberOfDoors!.Value),

                "suv" => new SUV(
                    dto.Id,
                    dto.Manufacturer,
                    dto.Model,
                    dto.Year,
                    dto.StartingBid,
                    dto.OwnerUserId,
                    dto.NumberOfSeats!.Value),

                "truck" => new Truck(
                    dto.Id,
                    dto.Manufacturer,
                    dto.Model,
                    dto.Year,
                    dto.StartingBid,
                    dto.OwnerUserId,
                    dto.LoadCapacity!.Value),
                _ => throw new ArgumentException($"Unknown vehicle type: {dto.Type}"),
            };
        }

        public static VehicleResponseDto MapToDto(Vehicle vehicle)
        {
            return new VehicleResponseDto()
            {
                Type  = vehicle.GetType().Name,
                Id = vehicle.Id,
                Manufacturer = vehicle.Manufacturer,
                Model = vehicle.Model,
                Year = vehicle.Year,
                StartingBid = vehicle.StartingBid,
                OwnerUserId = vehicle.OwnerUserId,
            };
        }
    }

}
