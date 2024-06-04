using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public class Part
    {
        // This property represents the Part's unique identifier.
        [Key]
        [Required]
        [Display(Name = "Part ID")]
        public int PartId { get; set; } // Primary Key

        public string Reference { get; set; }

        // This property represents the Part's name.
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Part Name"), MaxLength(75)]
        public string PartName { get; set; }

        // This property represents the Cost for each part.
        [DataType(DataType.Currency)] // Specifies that it contains currency data.
        [Required(ErrorMessage = "Please enter Part Cost")]
        [RegularExpression(@"^\$?\d+(\.\d{2})?$", ErrorMessage = "Please enter a valid amount.")] // Validates that the fee follows the specific format and provides a error message if not.
        [Display(Name = "Part Cost")]
        public decimal Cost { get; set; }

        // Navigation property
        public ICollection<FaultPart> FaultParts { get; set; } = new List<FaultPart>();
    }
}
