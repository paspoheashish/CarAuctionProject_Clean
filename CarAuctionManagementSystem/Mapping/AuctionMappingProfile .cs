using AutoMapper;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.DTOs.Auctions;

namespace CarAuctionManagementSystem.Mapping
{
    public class AuctionMappingProfile : Profile
    {
        public AuctionMappingProfile()
        {
            // Domain → Response
            CreateMap<Auction, AuctionResponse>()
                .ForMember(dest => dest.StartedAt, opt => opt.MapFrom(src => src.StartedAt.ToString("dd MMM yyyy HH:mm:ss")));

            // Request → Domain
            CreateMap<StartAuctionRequest, Auction>()
                .ConstructUsing(src => new Auction(src.VehicleId, 0)); // Starting bid comes from vehicle service
        }
    }
}
