using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CarAuctionManagementSystem.DTOs
{
    public class VehicleRequestDto :  IValidatableObject
    {
        [Required(ErrorMessage ="Vehicle Type is required.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Vehicle Id is required.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Vehicle Manufacturer is required.")]
        public string Manufacturer { get; set; }

        [Required(ErrorMessage = "Vehicle Model is required.")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Vehicle Year is required.")]
        public int Year { get; set; }
        public decimal StartingBid { get; set; }

        [JsonIgnore]
        public string? OwnerUserId { get; set; }

        public int? NumberOfDoors { get; set; }
        public int? NumberOfSeats { get; set; }
        public int? LoadCapacity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (Year == 0)
            {
                yield return new ValidationResult("Year is required.", new[] { nameof(Year) });
            }

            if (Type.ToLower() != "hatchback" &&  Type.ToLower() != "sedan" && Type.ToLower() != "suv" && Type.ToLower() != "truck")
            {
                yield return new ValidationResult($"Invalid vehicle type: {Type}. Allowed types are: Hatchback, Sedan, SUV, Truck.", new[] { nameof(Type) });
            }

            if (Type.ToLower() == "hatchback" ||  Type.ToLower() == "sedan")
            {
                if (NumberOfDoors == null || NumberOfDoors == 0)
                    yield return new ValidationResult("NumberOfDoors is required.", new[] { nameof(NumberOfDoors) });                
            }

            if (Type.ToLower() == "suv")
            {
                if (NumberOfSeats == null || NumberOfSeats == 0)
                    yield return new ValidationResult("NumberOfSeats is required.", new[] { nameof(NumberOfSeats) });
            }

            if (Type.ToLower() == "truck")
            {
                if (LoadCapacity == null || LoadCapacity == 0)
                    yield return new ValidationResult("LoadCapacity is required.", new[] { nameof(LoadCapacity) });
            }
        }
    }
}
