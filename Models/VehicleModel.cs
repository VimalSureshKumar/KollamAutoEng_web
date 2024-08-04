using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KollamAutoEng_web.Models
{
    public class VehicleModel
    {
        [Key]
        [Display(Name = "Model ID")]
        public int ModelId { get; set; }

        [Required(ErrorMessage = "Please enter the model name.")]
        [Display(Name = "Model Name")]
        [MaxLength(50, ErrorMessage = "The model name cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "The model name can only contain letters, numbers, and spaces.")]
        public string ModelName { get; set; }

        [Required]
        [Display(Name = "Vehicle Brand")]
        public int BrandId { get; set; }
        public virtual VehicleBrand? VehicleBrand { get; set; }

        public virtual ICollection<Vehicle>? Vehicles { get; set; }
    }
}
