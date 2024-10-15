using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KollamAutoEng_web.Models
{
    // Class representing a vehicle model entity
    public class VehicleModel
    {
        // Primary key for the VehicleModel entity
        [Key] // Marks this property as the primary key in the database
        [Display(Name = "Model ID")] // Specifies the display label for the ModelId field in the UI
        public int ModelId { get; set; }

        // Model name of the vehicle, required with validation rules
        [Required(ErrorMessage = "Please enter the model name.")] // Ensures this field is mandatory with a custom error message
        [Display(Name = "Model Name")] // Specifies the display label for the ModelName field in the UI
        [MaxLength(25, ErrorMessage = "The model name cannot exceed 25 characters.")] // Limits the maximum length of the model name
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "The model name can only contain letters, numbers, and spaces.")] // Validates that the model name contains only letters, numbers, and spaces
        public string ModelName { get; set; }

        // Foreign key reference to the VehicleBrand entity
        [Required] // Indicates that this field is mandatory
        [Display(Name = "Vehicle Brand")] // Specifies the display label for the BrandId field in the UI
        public int BrandId { get; set; } // Foreign key property linking to the VehicleBrand

        // Navigation property to the related VehicleBrand
        public virtual VehicleBrand? VehicleBrand { get; set; } // Reference to the associated VehicleBrand entity

        // Navigation property to related Vehicles
        public virtual ICollection<Vehicle>? Vehicles { get; set; } // Collection of vehicles associated with this model
    }
}