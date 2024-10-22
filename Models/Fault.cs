using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    // Fault class representing a record of faults in a vehicle
    public class Fault
    {
        // Primary key for the Fault entity
        [Key] // Marks this property as the primary key in the database
        [Display(Name = "Fault ID")] // Specifies the display label for the field in the UI
        public int FaultId { get; set; }

        // Name of the faulty part - required with validation rules
        [Required(ErrorMessage = "Please enter Faulty Part")] // Ensures this field is mandatory
        [MinLength(3, ErrorMessage = "The Fault Name must be at least 3 characters long.")] // Ensures the fault name is at least 3 characters long
        [MaxLength(35, ErrorMessage = "The Fault Name cannot exceed 35 characters.")] // Limits the length of the fault name to 35 characters
        [RegularExpression(@"^([A-Z][a-z]*)(\s[A-Z][a-z]*)*$", ErrorMessage = "Each word must start with an uppercase letter, followed by lowercase letters.")]
        // This regex ensures each word starts with a capital letter followed by lowercase
        [Display(Name = "Fault Name")] // Display label for the fault name in the UI
        public string FaultName { get; set; }

        // Foreign key for Vehicle - required
        [Required] // Ensures this field is mandatory
        [Display(Name = "Vehicle")] // Specifies the display label for the vehicle field in the UI
        public int VehicleId { get; set; }

        // Navigational property linking the Fault to the associated Vehicle entity
        public virtual Vehicle? Vehicle { get; set; } // Defines the relationship between the Fault and Vehicle

        // Foreign key for Customer - required
        [Required] // Ensures this field is mandatory
        [Display(Name = "Customer")] // Specifies the display label for the customer field in the UI
        public int CustomerId { get; set; }

        // Navigational property linking the Fault to the associated Customer entity
        public virtual Customer? Customer { get; set; } // Defines the relationship between the Fault and Customer

        // Collection of fault parts associated with the fault (optional)
        public virtual ICollection<FaultPart>? FaultParts { get; set; } // Represents the related fault parts associated with this fault
    }
}
