using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class Fault
    {
        [Required] // It ensures that the CoachId is mandatory and cannot be null or left empty.
        [Display(Name = "Fault ID")] // Sets the display name for this property.
        public int FaultId { get; set; }

    }
}
