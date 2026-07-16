using CarAuctionManagementSystem.Application.Interfaces.Repositories;
using CarAuctionManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IVehicleRepository, VehicleRepository>();
            services.AddSingleton<IAuctionRepository, AuctionRepository>();
            services.AddSingleton<IBidRepository, BidRepository>();
            services.AddSingleton<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
