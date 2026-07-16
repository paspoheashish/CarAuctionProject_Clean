using CarAuctionManagementSystem.Application.Interfaces.Services;
using CarAuctionManagementSystem.Application.Services;
using CarAuctionManagementSystem.Application.UseCases.Bids.Command;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Application.DependencyInjection
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IAuctionService, AuctionService>();
            services.AddMediatR(cfg =>
                         cfg.RegisterServicesFromAssemblyContaining<PlaceBidHandler>()
                     );

            return services;
        }
    }
}
