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
        public string Reference { get; set; }

        [Required(ErrorMessage = "Please enter Part Name"), MaxLength(75)]
        [Display(Name = "Part Name")]
        public string PartName { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter Part Cost")]
        [Display(Name = "Cost")]
        public decimal Cost { get; set; }

        public virtual ICollection<FaultPart> FaultParts { get; set; }
    }
}
