using CarAuctionManagementSystem.Application.DependencyInjection;
using CarAuctionManagementSystem.Mapping;

namespace CarAuctionManagementSystem.DependencyInjection
{
    public static class PresentationServiceRegistration
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddAutoMapper(cfg => { }, typeof(VehicleMappingProfile));
            return services;
        }
    }
}
