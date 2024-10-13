using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    // Enumeration for different drive types of vehicles
    public enum DriveType
    {
        FWD = 0, // Front-wheel drive
        RWD = 1, // Rear-wheel drive
        [Display(Name = "4WD")] _4WD = 2, // Four-wheel drive, with display name for UI
        AWD = 3 // All-wheel drive
    }

    // Enumeration for available colors of vehicles
    public enum Colour
    {
        Red = 0, // Red color option
        White = 1, // White color option
        Black = 2, // Black color option
        Grey = 3, // Grey color option
        Blue = 4, // Blue color option
        Cyan = 5, // Cyan color option
        Yellow = 6, // Yellow color option
        Orange = 7, // Orange color option
        Other = 8 // Option for other colors
    }

    // Class representing a vehicle entity
    public class Vehicle
    {
        // Primary key for the Vehicle entity
        [Key] // Marks this property as the primary key in the database
        [Display(Name = "Vehicle ID")] // Specifies the display label for the field in the UI
        public int VehicleId { get; set; }

        // Foreign key for Vehicle Brand, required field
        [Required] // Ensures this field is mandatory
        [Display(Name = "Vehicle Brand")] // Specifies the display label for the vehicle brand field
        public int BrandId { get; set; }
        public virtual VehicleBrand? VehicleBrand { get; set; } // Navigation property for related VehicleBrand

        // Foreign key for Vehicle Model, required field
        [Required] // Ensures this field is mandatory
        [Display(Name = "Vehicle Model")] // Specifies the display label for the vehicle model field
        public int ModelId { get; set; }
        public virtual VehicleModel? VehicleModel { get; set; } // Navigation property for related VehicleModel

        // Vehicle Identification Number (VIN), required with specific validation rules
        [Required(ErrorMessage = "Please enter the Vehicle Identification Number (VIN).")] // Ensures this field is mandatory with a custom error message
        [Display(Name = "VIN")] // Specifies the display label for the VIN field
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "The VIN must be exactly 17 characters long and can only contain capital letters " +
            "(A-H, J-N, P-R, S-Z) and digits (0-9), excluding I, O, and Q.")] // Validates the VIN format
        public string VIN { get; set; }

        // Vehicle registration number, required with specific validation rules
        [Required(ErrorMessage = "Please enter the vehicle registration number.")] // Ensures this field is mandatory with a custom error message
        [Display(Name = "Registration")] // Specifies the display label for the registration field
        [RegularExpression(@"^([A-Z]{2}\d{2}[A-Z]{2}\d{4}|[A-Z]{3}\d{3}|[A-Z]{2}\d{4}|[A-Z]{5}|[A-Z]{3}\d{3}[A-Z]?|[A-Z]{2}\d{4})$", ErrorMessage = "The registration number must be in a valid format (e.g., MH12AB1234, ABC123, AB1234, ABCDE, ABC123, or ABC123D).")] // Validates the registration number format
        public string Registration { get; set; }

        // Colour of the vehicle, required field
        [Required] // Ensures this field is mandatory
        [Display(Name = "Colour")] // Specifies the display label for the color field
        public Colour? Colour { get; set; } // Nullable to allow for non-selection

        // Drive type of the vehicle, required field
        [Required] // Ensures this field is mandatory
        [Display(Name = "Drive Type")] // Specifies the display label for the drive type field
        public DriveType? DriveType { get; set; } // Nullable to allow for non-selection

        // Odometer reading, required with specific validation rules
        [Required(ErrorMessage = "Please enter the odometer reading.")] // Ensures this field is mandatory with a custom error message
        [Display(Name = "Odometer")] // Specifies the display label for the odometer field
        [DisplayFormat(DataFormatString = "{0:N0}")] // Formats the display to show no decimal places with thousand separators
        [Range(0, 1000000, ErrorMessage = "The odometer reading must be between 0 and 1,000,000.")] // Validates that the odometer is within a specific range
        public int Odometer { get; set; }

        // Foreign key for Customer, required field
        [Required] // Ensures this field is mandatory
        [Display(Name = "Customer")] // Specifies the display label for the customer field
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; } // Navigation property for related Customer

        // Navigation properties for related entities
        public virtual ICollection<Appointment>? Appointments { get; set; } // Collection of appointments related to this vehicle
        public virtual ICollection<Fault>? Faults { get; set; } // Collection of faults related to this vehicle
    }
}
