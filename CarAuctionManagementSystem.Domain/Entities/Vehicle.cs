using CarAuctionManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuctionManagementSystem.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public VehicleType Type { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal StartingBid { get; set; }
        public string OwnerUserId { get; set; }
        public DateTime CreatedOn { get; set; }

        public Vehicle() { }

        protected Vehicle(int id, VehicleType type, string manufacturer, string model, int year, decimal startingBid, string ownerUserId)
        {
            Id = id;
            Type = type;
            Manufacturer = manufacturer;
            Model = model;
            Year = year;
            StartingBid = startingBid;
            OwnerUserId = ownerUserId;
            CreatedOn = DateTime.UtcNow;
        }
    }

}
