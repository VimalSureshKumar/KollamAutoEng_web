using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class Staff
    {
        [Required] // It ensures that the CoachId is mandatory and cannot be null or left empty.
        [Display(Name = "Staff ID")] // Sets the display name for this property.
        public int StaffId { get; set; }
    }
}
