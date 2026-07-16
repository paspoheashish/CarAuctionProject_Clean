using AutoMapper;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.Domain.Enums;
using CarAuctionManagementSystem.DTOs.Vehicles;

namespace CarAuctionManagementSystem.Mapping
{

    public class VehicleMappingProfile : Profile
    {
        public VehicleMappingProfile()
        {
            // Domain → Response
            CreateMap<Vehicle, VehicleResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(x => x.GetType().Name))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => x.CreatedOn.ToString("dd MMM yyyy HH:mm:ss")));
            // Request → Domain
            CreateMap<CreateVehicleRequest, Vehicle>()
            .ConvertUsing<CreateVehicleRequestToVehicleConverter>();
        }
    }

    public class CreateVehicleRequestToVehicleConverter : ITypeConverter<CreateVehicleRequest, Vehicle>
    {
        public Vehicle Convert(CreateVehicleRequest x, Vehicle dest, ResolutionContext context)
        {
            return x.Type switch
            {
                "hatchback" => new Hatchback(
                    x.Id,
                    x.Manufacturer,
                    x.Model,
                    x.Year,
                    x.StartingBid,
                    x.OwnerUserId,
                    x.NumberOfDoors!.Value),

                "sedan" => new Sedan(
                    x.Id,
                    x.Manufacturer,
                    x.Model,
                    x.Year,
                    x.StartingBid,
                    x.OwnerUserId,
                    x.NumberOfDoors!.Value),

                "suv" => new SUV(
                    x.Id,
                    x.Manufacturer,
                    x.Model,
                    x.Year,
                    x.StartingBid,
                    x.OwnerUserId,
                    x.NumberOfSeats!.Value),

                "truck" => new Truck(
                    x.Id,
                    x.Manufacturer,
                    x.Model,
                    x.Year,
                    x.StartingBid,
                    x.OwnerUserId,
                    x.LoadCapacityKg!.Value),

                _ => throw new ArgumentOutOfRangeException(nameof(x.Type))
            };
        }
    }

}


