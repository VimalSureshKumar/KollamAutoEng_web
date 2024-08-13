using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public enum DriveType
    {
        FWD = 0,
        RWD = 1,
        [Display(Name = "4WD")] _4WD = 2,
        AWD = 3
    }
    public enum Colour
    {
        Red = 0,
        White = 1,
        Black = 2,
        Grey = 3,
        Blue = 4,
        Cyan = 5,
        Yellow = 6,
        Orange = 7,
        Other = 8
    }

    public class Vehicle
    {
        [Key]
        [Display(Name = "Vehicle ID")]
        public int VehicleId { get; set; }

        [Required]
        [Display(Name = "Vehicle Brand")]
        public int BrandId { get; set; }
        public virtual VehicleBrand? VehicleBrand { get; set; }

        [Required]
        [Display(Name = "Vehicle Model")]
        public int ModelId { get; set; }
        public virtual VehicleModel? VehicleModel { get; set; }

        [Required(ErrorMessage = "Please enter the Vehicle Identification Number (VIN).")]
        [Display(Name = "VIN")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "The VIN must be exactly 17 characters long and can only contain capital letters " +
            "(A-H, J-N, P-R, S-Z) and digits (0-9), excluding I, O, and Q.")]
        public string VIN { get; set; }

        [Required(ErrorMessage = "Please enter the vehicle registration number.")]
        [Display(Name = "Registration")]
        [RegularExpression(@"^([A-Z]{2}\d{2}[A-Z]{2}\d{4}|[A-Z]{3}\d{3}|[A-Z]{2}\d{4}|[A-Z]{5}|[A-Z]{3}\d{3}[A-Z]?|[A-Z]{2}\d{4})$", ErrorMessage = "The registration number must be in a valid format (e.g., MH12AB1234, ABC123, AB1234, ABCDE, ABC123, or ABC123D).")]
        public string Registration { get; set; }

        [Required]
        [Display(Name = "Colour")]
        public Colour? Colour { get; set; }

        [Required]
        [Display(Name = "Drive Type")]
        public DriveType? DriveType { get; set; }

        [Required(ErrorMessage = "Please enter the odometer reading.")]
        [Display(Name = "Odometer")]
        [Range(0, 1000000, ErrorMessage = "The odometer reading must be between 0 and 1,000,000.")]
        public int Odometer { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }
        public virtual ICollection<Fault>? Faults { get; set; }
    }
}
