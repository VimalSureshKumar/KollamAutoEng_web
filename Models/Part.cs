using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public class Part
    {
        [Key]
        [Display(Name = "Part ID")]
        public int PartId { get; set; }

        [Display(Name = "Reference")]
        [Required(ErrorMessage = "Please enter a reference.")]
        [RegularExpression(@"^[A-Z]{4}-\d{5}$", ErrorMessage = "The reference must be in the format ABCD-12345.")]
        public string Reference { get; set; }

        [Required(ErrorMessage = "Please enter Part Name")]
        [MaxLength(50, ErrorMessage = "The Part Name cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Only letters and spaces are allowed.")]
        [Display(Name = "Part Name")]
        public string PartName { get; set; }

        [Required(ErrorMessage = "Please enter Part Cost")]
        [DataType(DataType.Currency)]
        [RegularExpression("^(0|[1-9][0-9]*)(\\.[0-9]+)?$", ErrorMessage = "Please enter a valid positive number.")]
        [Range(0.99, 50000, ErrorMessage = "Please enter a value between 0 and 100,000.")]
        [Display(Name = "Part Cost")]
        public decimal Cost { get; set; }

        public virtual ICollection<FaultPart>? FaultParts { get; set; }
    }
}
