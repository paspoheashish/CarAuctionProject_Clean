using AutoMapper;
using CarAuctionManagementSystem.Domain.Entities;
using CarAuctionManagementSystem.DTOs.Bids;

namespace CarAuctionManagementSystem.Mapping
{
    public class BidMappingProfile : Profile
    {
        public BidMappingProfile()
        {
            // Domain → Response
            CreateMap<Bid, BidResponse>()
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp.ToString("dd MMM yyyy HH:mm:ss"))); ;

            // Request → Domain
            CreateMap<PlaceBidRequest, Bid>()
                .ConstructUsing(src => new Bid(                  
                    src.AuctionId,
                    src.BidderUserId,
                    src.Amount));
        }
    }
}
