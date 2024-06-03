using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; } // Primary Key
        public string AppointmentDate { get; set; }
        [Required]
        public int CustomerId { get; set; } // Foreign Key to Customer
        [Required]
        public int VehicleId { get; set; } // Foreign Key to Vehicle
        [Required]
        public int EmployeeId { get; set; } // Foreign Key to Employee
        public decimal ServiceCost { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
        public Employee Employee { get; set; }
        public ICollection<FaultPart> FaultParts { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
