using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class VehicleBrand
    {
        [Key]
        [Display(Name = "Brand ID")]
        public int BrandId { get; set; } // Primary Key

        [Required]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; } // Brand Name

        // Navigation properties
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
