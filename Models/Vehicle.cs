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
        [Display(Name = "Vehicle Brand")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Brand")]
        public int BrandId { get; set; }
        public virtual VehicleBrand VehicleBrand { get; set; }

        [Required]
        [Display(Name = "Vehicle Model")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Model")]
        public int ModelId { get; set; }
        public virtual VehicleModel VehicleModel { get; set; }

        [Required(ErrorMessage = "Please enter the Vehicle Identification Number (VIN).")]
        [Display(Name = "VIN")]
        [RegularExpression(@"^[A-HJ-NPR-Z0-9]{17}$", ErrorMessage = "The VIN must be exactly 17 characters long and can only contain capital letters " +
            "(A-H, J-N, P-R, S-Z) and digits (0-9), excluding I, O, and Q.")]
        public string VIN { get; set; }

        [Required(ErrorMessage = "Please enter the vehicle registration number.")]
        [Display(Name = "Registration")]
        [RegularExpression(@"^([A-Z]{2}\d{2}[A-Z]{2}\d{4}|[A-Z]{3}\d{3}|[A-Z]{2}\d{4}|[A-Z]{5})$", ErrorMessage = "The registration number must be in a valid format (e.g., MH12AB1234, ABC123, AB1234, or ABCDE).")]
        public string Registration { get; set; }

        [Required]
        [Display(Name = "Colour")]
        [MaxLength(10)]
        public Colour Colour { get; set; }

        [Required]
        [Display(Name = "Drive Type")]
        [MaxLength(3)]
        public DriveType DriveType { get; set; }

        [Required(ErrorMessage = "Please enter the odometer reading.")]
        [Display(Name = "Odometer")]
        [Range(0, 1000000, ErrorMessage = "The odometer reading must be between 0 and 1,000,000.")]
        public int Odometer { get; set; }

        [Required]
        [Display(Name = "Customer")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Fault> Faults { get; set; }
    }
}
