using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class VehicleBrand
    {
        [Key]
        [Display(Name = "Brand ID")]
        public int BrandId { get; set; }

        [Required]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<VehicleModel> VehicleModels { get; set; }
    }
}