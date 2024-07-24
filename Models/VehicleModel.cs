using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KollamAutoEng_web.Models
{
    public class VehicleModel
    {
        [Key]
        [Display(Name = "Model ID")]
        public int ModelId { get; set; }

        [Required]
        [Display(Name = "Model Name")]
        public string ModelName { get; set; }

        [Required]
        [Display(Name = "Brand ID")]
        public int BrandId { get; set; }

        public virtual VehicleBrand VehicleBrand { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
