using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    // Class representing a vehicle brand entity
    public class VehicleBrand
    {
        // Primary key for the VehicleBrand entity
        [Key] // Marks this property as the primary key in the database
        [Display(Name = "Brand ID")] // Specifies the display label for the BrandId field in the UI
        public int BrandId { get; set; }

        // Brand name of the vehicle, required with validation rules
        [Required(ErrorMessage = "Please enter the brand name.")] // Ensures this field is mandatory with a custom error message
        [Display(Name = "Brand Name")] // Specifies the display label for the BrandName field in the UI
        [MaxLength(25, ErrorMessage = "The brand name cannot exceed 25 characters.")] // Limits the maximum length of the brand name
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "The brand name can only contain letters and spaces.")] // Validates that the brand name contains only letters and spaces
        public string BrandName { get; set; }

        // Navigation property to related Vehicles
        public virtual ICollection<Vehicle>? Vehicles { get; set; } // Collection of vehicles associated with this brand

        // Navigation property to related VehicleModels
        public virtual ICollection<VehicleModel>? VehicleModels { get; set; } // Collection of vehicle models associated with this brand
    }
}
