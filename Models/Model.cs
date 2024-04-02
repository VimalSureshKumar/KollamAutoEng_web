using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class Model
    {
        [Required] // It ensures that the CoachId is mandatory and cannot be null or left empty.
        [Display(Name = "Model ID")] // Sets the display name for this property.
        public int ModelId { get; set; }
    }
}
