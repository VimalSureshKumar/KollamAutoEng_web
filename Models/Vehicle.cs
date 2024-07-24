using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public enum DriveType
    {
        FWD, RWD, [Display(Name = "4WD")] _4WD, AWD
    }

    public enum Colour
    {
        Red, White, Black, Grey, Blue, Cyan, Yellow, Orange, Other
    }

    public class Vehicle
    {
        [Key]
        [Display(Name = "Vehicle ID")]
        public int VehicleId { get; set; }

        [Required]
        [Display(Name = "Brand ID")]
        public int BrandId { get; set; }

        public virtual VehicleBrand VehicleBrand { get; set; }

        [Required]
        [Display(Name = "Model ID")]
        public int ModelId { get; set; }

        public virtual VehicleModel VehicleModel { get; set; }

        [Required]
        [Display(Name = "VIN")]
        public string VIN { get; set; }

        [Required]
        [Display(Name = "Registration")]
        public string Registration { get; set; }

        [Required]
        [Display(Name = "Colour")]
        public Colour Colour { get; set; }

        [Required]
        [Display(Name = "Drive Type")]
        public DriveType DriveType { get; set; }

        [Required]
        [Display(Name = "Odometer")]
        public int Odometer { get; set; }

        [Required]
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Fault> Faults { get; set; }
    }
}
