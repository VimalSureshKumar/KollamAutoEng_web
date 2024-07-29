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

        [Required(ErrorMessage = "Please enter valid Appointment Name")]
        [MaxLength(25)]
        [RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = "Only letters and spaces are allowed.")]
        [Display(Name = "Appointment Name")]
        public string AppointmentName { get; set; }

        [Required]
        [DateValidator(ErrorMessage = "The appointment date must be within one year from today.")]
        [DataType(DataType.Date)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }

        [Required]
        [Display(Name = "Customer")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Required]
        [Display(Name = "Vehicle")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid vehicle")]
        public int VehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        [Required]
        [Display(Name = "Employee")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        [Required(ErrorMessage = "Please enter Service Cost")]
        [RegularExpression("^(0|[1-9][0-9]*)(\\.[0-9]+)?$", ErrorMessage = "Please enter a valid positive number.")]
        [Range(0, 100000, ErrorMessage = "Please enter a value between 0 and 100,000.")]
        [Display(Name = "Service Cost")]
        public decimal ServiceCost { get; set; }

        public virtual ICollection<FaultPart> FaultParts { get; set; }
    }
}
