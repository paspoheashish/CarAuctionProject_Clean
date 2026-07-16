using CarAuctionManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Infrastructure.DataBase
{
    public static class InMemoryDatabase
    {
        public static List<Auction> Auctions { get; set; } = new List<Auction>();
        public static List<Vehicle> Vehicles { get; set; } = new List<Vehicle>
        {
            new Hatchback(1, "Toyota", "Yaris", 2018, 3500,"owner1", 4),
            new Hatchback(2, "Honda", "Fit", 2019, 3800,"owner1", 4),
            new Hatchback(3, "Ford", "Fiesta", 2017, 3200,"owner1", 4),
            new Hatchback(4, "Hyundai", "i20", 2020, 4000,"owner1", 4),
            new Hatchback(5, "Volkswagen", "Polo", 2016, 3000,"owner1", 4),

            new Sedan(6, "Toyota", "Camry", 2018, 7000,"owner2", 4),
            new Sedan(7, "Honda", "Accord", 2019, 7500,"owner2", 4),
            new Sedan(8, "Hyundai", "Elantra", 2020, 6800,"owner2", 4),
            new Sedan(9, "BMW", "3 Series", 2017, 12000,"owner2", 4),
            new Sedan(10, "Mercedes", "C-Class", 2016, 13000,"owner2", 4),

            new SUV(11, "Toyota", "RAV4", 2018, 11000,"owner3", 5),
            new SUV(12, "Honda", "CR-V", 2019, 11500,"owner3", 5),
            new SUV(13, "Ford", "Escape", 2020, 10500,"owner3", 5),
            new SUV(14, "Hyundai", "Tucson", 2017, 9500,"owner3", 5),
            new SUV(15, "Kia", "Sportage", 2016, 9000,"owner3", 5),

            new Truck(16, "Ford", "F-150", 2018, 15000,"owner4", 2000),
            new Truck(17, "Chevrolet", "Silverado", 2019, 16000,"owner4", 2200),
            new Truck(18, "Ram", "1500", 2020, 17000,"owner4", 2500),
            new Truck(19, "Toyota", "Tundra", 2017, 15500,"owner4", 2300),
            new Truck(20, "Nissan", "Titan", 2016, 14000,"owner4", 2100)
        };
    }
}
