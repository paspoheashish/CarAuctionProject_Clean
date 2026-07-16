using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Domain.Entities
{
    public class Hatchback : Vehicle
    {
        public int NumberOfDoors { get; set; }

        Hatchback() : base() { }
        public Hatchback(int id, string manufacturer, string model, int year, decimal startingBid, string ownerUserId, int numberOfDoors)
            : base(id, Enums.VehicleType.Hatchback, manufacturer, model, year, startingBid, ownerUserId)
        {
            NumberOfDoors = numberOfDoors;
        }
    }
}
