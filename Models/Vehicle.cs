using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public enum DriveType
    {
        FWD, 
        RWD,
        [Display(Name = "4WD")]
        _4WD, 
        AWD
    }
    public class Vehicle
    {
        [Key]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; } // Primary Key

        [Required]
        [Display(Name = "Brand")]
        public int BrandId { get; set; } // Foreign Key to VehicleBrand

        [Required]
        [Display(Name = "Model")]
        public int ModelId { get; set; } // Foreign Key to VehicleModel

        [Required]
        [Display(Name = "VIN")]
        public string VIN { get; set; } // Vehicle Identification Number

        [Required]
        [Display(Name = "Registration")]
        public string Registration { get; set; } // Vehicle Registration

        [Required]
        [Display(Name = "Odometer")]
        public int Odometer { get; set; } // Odometer Reading

        [Required]
        [Display(Name = "Drive Type")]
        public DriveType DriveType { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; } // Foreign Key to Customer

        // Navigation properties
        public virtual Customer Customer { get; set; }

        [Display(Name = "Vehicle Brand")]
        public virtual VehicleBrand VehicleBrand { get; set; }

        [Display(Name = "Vehicle Model")]
        public virtual VehicleModel VehicleModel { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Fault> Faults { get; set; }
    }
}
