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
        [MinLength(3, ErrorMessage = "The Brand Name must be at least 3 characters long.")] // Ensures the brand name is at least 3 characters long
        [MaxLength(25, ErrorMessage = "The Brand Name cannot exceed 25 characters.")] // Limits the length of the brand name to 25 characters
        [RegularExpression(@"^([A-Z][a-z]*)(\s[A-Z][a-z]*)*$", ErrorMessage = "Each word must start with an uppercase letter, followed by lowercase letters.")]
        // This regex ensures each word starts with a capital letter followed by lowercase
        [Required(ErrorMessage = "Please enter the brand name.")] // Ensures this field is mandatory with a custom error message
        [Display(Name = "Brand Name")] // Specifies the display label for the BrandName field in the UI
        public string BrandName { get; set; }

        // Navigation property to related Vehicles
        public virtual ICollection<Vehicle>? Vehicles { get; set; } // Collection of vehicles associated with this brand

        // Navigation property to related VehicleModels
        public virtual ICollection<VehicleModel>? VehicleModels { get; set; } // Collection of vehicle models associated with this brand
    }
}
