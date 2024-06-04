using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public class Appointment
    {
        // This property represents the Appointment unique identifier.
        [Key]
        [Display(Name = "Appointment ID")] // Sets the display name for this property.
        public int AppointmentId { get; set; } // Primary Key

        public string AppointmentDate { get; set; }

        [Required]
        public int CustomerId { get; set; } // Foreign Key to Customer

        [Required]
        public int VehicleId { get; set; } // Foreign Key to Vehicle

        [Required]
        public int EmployeeId { get; set; } // Foreign Key to Employee

        // This property represents the Service Cost.
        [DataType(DataType.Currency)] // Specifies that it contains currency data.
        [Required(ErrorMessage = "Please enter Service Cost")]
        [RegularExpression(@"^\$?\d+(\.\d{2})?$", ErrorMessage = "Please enter a valid amount.")] // Validates that the cost follows the specific format and provides a error message if not.
        [Display(Name = "Service Cost")]
        public decimal ServiceCost { get; set; }

        // Navigation properties
        [ForeignKey("VehicleId")]
        public Customer Customer { get; set; }

        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public ICollection<FaultPart> FaultParts { get; set; } = new List<FaultPart>();

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
