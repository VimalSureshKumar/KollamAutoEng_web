using KollamAutoEng_web.ValidationAttributes;
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
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Please enter Customer First Name"), MaxLength(25)]
        [Display(Name = "Appointment Name")]
        public string AppointmentName { get; set; }

        [Required]
        [DateValidator(ErrorMessage = "The appointment date must be within one year from today.")]
        [DataType(DataType.Date)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [Required]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        [Required]
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter Service Cost")]
        [Range(0, 1000000, ErrorMessage = "Please enter a value between 0 and 1,000,000.")]
        [Display(Name = "Service Cost")]
        public decimal ServiceCost { get; set; }

        public virtual ICollection<FaultPart> FaultParts { get; set; }
    }
}
