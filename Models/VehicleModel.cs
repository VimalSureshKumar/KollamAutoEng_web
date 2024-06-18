using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KollamAutoEng_web.Models
{
    public class VehicleModel
    {
        [Key]
        [Display(Name = "Model ID")]
        public int ModelId { get; set; } // Primary Key

        [Required]
        [Display(Name = "Model Name")]
        public string ModelName { get; set; } // Model Name

        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; } // Foreign Key to VehicleBrand

        // Navigation properties
        public virtual VehicleBrand VehicleBrand { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
