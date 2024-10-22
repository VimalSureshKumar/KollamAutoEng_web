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
        [Required(ErrorMessage = "Please enter a reference.")] // Ensures this field is mandatory
        [MinLength(10, ErrorMessage = "The reference must be exactly 10 characters long.")] // Ensures the reference is at least 10 characters (since the format is fixed)
        [MaxLength(10, ErrorMessage = "The reference must be exactly 10 characters long.")] // Ensures the reference is at most 10 characters
        [RegularExpression(@"^XYZA-\d{5}$", ErrorMessage = "The reference must be in the format XYZA-00001 to XYZA-99999.")]
        // Enforces the reference format: XYZA followed by a hyphen and exactly 5 digits
        [Display(Name = "Reference")] // Specifies the display label for the reference field in the UI
        public string Reference { get; set; }

        // Name of the part - required with a maximum length of 35 characters
        [Required(ErrorMessage = "Please enter Part Name")] // Ensures this field is mandatory
        [MinLength(3, ErrorMessage = "The Part Name must be at least 3 characters long.")] // Ensures the part name is at least 3 characters long
        [MaxLength(30, ErrorMessage = "The Part Name cannot exceed 30 characters.")] // Limits the part name to 30 characters
        [RegularExpression(@"^([A-Z][a-z]*)(\s[A-Z][a-z]*)*$", ErrorMessage = "Each word must start with an uppercase letter, followed by lowercase letters.")]
        // This regex ensures that each word starts with an uppercase letter, followed by lowercase letters, and allows multiple words separated by spaces
        [Display(Name = "Part Name")] // Specifies the display label for the part name in the UI
        public string PartName { get; set; }

        // Cost of the part - required, validated as a currency, and within a specific range
        [Required(ErrorMessage = "Please enter Part Cost")] // Ensures that part cost is mandatory
        [DataType(DataType.Currency)] // Specifies that the field should be treated as a currency value
        [RegularExpression(@"^(0|[1-9][0-9]{0,4}(,[0-9]{3})*)(\.[0-9]{1,2})?$", ErrorMessage = "Please enter a valid positive number (e.g., 50,000 or 50000).")]
        // This regex allows numbers with optional commas as thousands separators and decimals with up to 2 decimal places.
        [Range(0.99, 50000, ErrorMessage = "Please enter a value between 0.99 and 50,000.")]
        [Display(Name = "Part Cost")] // Specifies the display label for the part cost in the UI
        public decimal Cost { get; set; }

        // Collection of FaultParts associated with this part (optional)
        public virtual ICollection<FaultPart>? FaultParts { get; set; } // Represents the relationship between the Part and FaultPart entities
    }
}
