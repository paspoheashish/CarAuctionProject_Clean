using CarAuctionManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CarAuctionManagementSystem.DTOs.Vehicles
{
    public record SearchVehiclesRequest : IValidatableObject
    {
        public string? Type { get; set; }
        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public int? Year { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Type != null && Type.ToLower() != "hatchback" && Type.ToLower() != "sedan" && Type.ToLower() != "suv" && Type.ToLower() != "truck")
            {
                yield return new ValidationResult($"Invalid vehicle type: {Type}. Allowed types are: Hatchback, Sedan, SUV, Truck.", new[] { nameof(Type) });
            }

        }
    }

}
