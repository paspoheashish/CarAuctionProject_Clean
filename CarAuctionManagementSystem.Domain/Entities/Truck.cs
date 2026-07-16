using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Domain.Entities
{
    public class Truck : Vehicle
    {
        public decimal LoadCapacityKg { get; }

        public Truck() : base() { }
        public Truck(int id, string manufacturer, string model, int year, decimal startingBid, string ownerUserId, decimal loadCapacityKg)
            : base(id, Enums.VehicleType.Truck, manufacturer, model, year, startingBid, ownerUserId)
        {
            LoadCapacityKg = loadCapacityKg;
        }
    }

}
