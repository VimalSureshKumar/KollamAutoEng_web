using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class Part
    {
        [Required] // It ensures that the PartId is mandatory and cannot be null or left empty.
        [Display(Name = "Part ID")] // Sets the display name for this property.
        [Key]
        public int PartId { get; set; }
        public string Reference { get; set; }
        public string PartName { get; set; }   
    }
}
