using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class VehicleBrand
    {
        [Key]
        [Display(Name = "Brand ID")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Please enter the brand name.")]
        [Display(Name = "Brand Name")]
        [MaxLength(50, ErrorMessage = "The brand name cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "The brand name can only contain letters and spaces.")]
        public string BrandName { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}