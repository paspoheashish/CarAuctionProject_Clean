using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Domain.Entities
{
    public class Sedan : Vehicle
    {
        public int NumberOfDoors { get; set; }

        public Sedan() : base() { }
        public Sedan(int id, string manufacturer, string model, int year, decimal startingBid, string ownerUserId, int numberOfDoors)
            : base(id, Enums.VehicleType.Sedan, manufacturer, model, year, startingBid, ownerUserId)
        {
            NumberOfDoors = numberOfDoors;
        }
    }

}
