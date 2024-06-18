using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public class Part
    {
        [Key]
        [Display(Name = "Part ID")]
        public int PartId { get; set; } // Primary Key

        [Display(Name = "Reference")]
        public string Reference { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Part Name"), MaxLength(75)]
        [Display(Name = "Part")]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter Part Cost")]
        [RegularExpression(@"^\$?\d+(\.\d{2})?$", ErrorMessage = "Please enter a valid amount.")]
        [Display(Name = "Cost")]
        public decimal Cost { get; set; }

        // Navigation property
        public virtual ICollection<FaultPart> FaultParts { get; set; }
    }

}
