using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public class Appointment
    {
        [Key]
        [Display(Name = "Appointment ID")]
        public int AppointmentId { get; set; } // Primary Key

        [Required(ErrorMessage = "Appointment date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; } // Foreign Key to Customer

        [Required]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; } // Foreign Key to Vehicle

        [Required]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; } // Foreign Key to Employee

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter Service Cost")]
        [RegularExpression(@"^\$?\d+(\.\d{2})?$", ErrorMessage = "Please enter a valid amount.")]
        [Display(Name = "Service Cost")]
        public decimal ServiceCost { get; set; }

        // Navigation properties
        public virtual Customer Customer { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<FaultPart> FaultParts { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }

}
