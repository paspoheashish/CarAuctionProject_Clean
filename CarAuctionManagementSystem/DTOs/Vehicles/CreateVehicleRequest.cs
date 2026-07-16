using CarAuctionManagementSystem.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CarAuctionManagementSystem.DTOs.Vehicles
{
    public record CreateVehicleRequest : IValidatableObject
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Type { get; init; }
        [Required]
        public string Manufacturer { get; init; }
        [Required]
        public string Model { get; init; }
        [Required]
        public int Year { get; init; }
        [Required]
        public decimal StartingBid { get; init; }
        [JsonIgnore]
        public string? OwnerUserId { get; set; }
        public int? NumberOfDoors { get; init; }
        public int? NumberOfSeats { get; init; }
        public decimal? LoadCapacityKg { get; init; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if (Id <= 0)
                yield return new ValidationResult("Id must be  0.", new[] { nameof(Id) });

            if (Type.ToLower() != "hatchback" && Type.ToLower() != "sedan" && Type.ToLower() != "suv" && Type.ToLower() != "truck")
            {
                yield return new ValidationResult($"Invalid vehicle type: {Type}. Allowed types are: Hatchback, Sedan, SUV, Truck.", new[] { nameof(Type) });
            }

            if (Type.ToLower() == "hatchback" || Type == "sedan")
            {
                if (NumberOfDoors == null || NumberOfDoors == 0)
                    yield return new ValidationResult("NumberOfDoors is required.", new[] { nameof(NumberOfDoors) });

                if (NumberOfDoors < 2 || NumberOfDoors > 5)
                    yield return new ValidationResult("NumberOfDoors must be between 2 and 5.", new[] { nameof(NumberOfDoors) });
            }

            if (Type.ToLower() == "suv")
            {
                if (NumberOfSeats == null || NumberOfSeats == 0)
                    yield return new ValidationResult("NumberOfSeats is required.", new[] { nameof(NumberOfSeats) });

                if (NumberOfSeats < 2 || NumberOfSeats > 8)
                    yield return new ValidationResult("NumberOfSeats must be between 2 and 8.", new[] { nameof(NumberOfSeats) });
            }

            if (Type.ToLower() == "truck")
            {
                if (LoadCapacityKg == null || LoadCapacityKg == 0)
                    yield return new ValidationResult("LoadCapacityKg is required.", new[] { nameof(LoadCapacityKg) });

                if (LoadCapacityKg <= 0)
                    yield return new ValidationResult("LoadCapacityKg must be greater than 0.", new[] { nameof(LoadCapacityKg) });
            }

            if (StartingBid <= 0)
                yield return new ValidationResult("StartingBid must be greater than 0.", new[] { nameof(StartingBid) });

            if (Year < 1900 || Year > DateTime.UtcNow.Year + 1)
                yield return new ValidationResult("Year is invalid.", new[] { nameof(Year) });
        }
    }
}