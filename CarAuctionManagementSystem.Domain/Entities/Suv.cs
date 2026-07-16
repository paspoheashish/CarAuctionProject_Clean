using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Domain.Entities
{
    public class SUV : Vehicle
    {
        public int NumberOfSeats { get; }
        public SUV() : base() { }
        public SUV(int id, string manufacturer, string model, int year, decimal startingBid, string ownerUserId, int numberOfSeats)
            : base(id, Enums.VehicleType.SUV, manufacturer, model, year, startingBid, ownerUserId)
        {
            NumberOfSeats = numberOfSeats;
        }
    }

}
