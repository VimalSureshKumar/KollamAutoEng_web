using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    // Part class representing an auto part entity
    public class Part
    {
        // Primary key for the Part entity
        [Key] // Marks this property as the primary key in the database
        [Display(Name = "Part ID")] // Specifies the display label for the field in the UI
        public int PartId { get; set; }

        // Reference code for the part - required and must follow a specific pattern
        [Display(Name = "Reference")] // Specifies the display label for the reference field in the UI
        [Required(ErrorMessage = "Please enter a reference.")] // Ensures this field is mandatory
        [RegularExpression(@"^[A-Z]{4}-\d{5}$", ErrorMessage = "The reference must be in the format ABCD-12345.")] // Enforces the reference format: 4 uppercase letters followed by a hyphen and 5 digits
        public string Reference { get; set; }

        // Name of the part - required with a maximum length of 50 characters
        [Required(ErrorMessage = "Please enter Part Name")] // Ensures the part name is mandatory
        [MaxLength(50, ErrorMessage = "The Part Name cannot exceed 50 characters.")] // Limits the part name length to 50 characters
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Only letters and spaces are allowed.")] // Restricts input to letters and spaces only
        [Display(Name = "Part Name")] // Specifies the display label for the part name in the UI
        public string PartName { get; set; }

        // Cost of the part - required, validated as a currency, and within a specific range
        [Required(ErrorMessage = "Please enter Part Cost")] // Ensures that part cost is mandatory
        [DataType(DataType.Currency)] // Specifies that this field is currency type
        [RegularExpression("^(0|[1-9][0-9]*)(\\.[0-9]+)?$", ErrorMessage = "Please enter a valid positive number.")] // Ensures the input is a positive number with optional decimal values
        [Range(0.99, 50000, ErrorMessage = "Please enter a value between 0 and 100,000.")] // Enforces a valid range for the part cost
        [Display(Name = "Part Cost")] // Specifies the display label for the part cost in the UI
        public decimal Cost { get; set; }

        // Collection of FaultParts associated with this part (optional)
        public virtual ICollection<FaultPart>? FaultParts { get; set; } // Represents the relationship between the Part and FaultPart entities
    }
}
