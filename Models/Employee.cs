using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public class Employee
    {
        // This property represents the Employee's unique identifier.
        [Key]
        [Required]
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; } // Primary Key

        // This property represents the Employee's first name.
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Employee First Name"), MaxLength(25)]
        //---
        public string FirstName { get; set; }

        // This property represents the Employee's last name.
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Employee Last Name"), MaxLength(25)]
        //---
        public string LastName { get; set; }

        // This property represents the Employee's phone number.
        [DataType(DataType.PhoneNumber), MaxLength(17)]
        //---
        [Display(Name = "Employee Phone Number")]
        public string PhoneNumber { get; set; }

        //---
        [Required(ErrorMessage = "Please enter Employee Status"), MaxLength(5)]
        //---
        public bool Status { get; set; }

        // This property represents the Pay for each Employee.
        [DataType(DataType.Currency)] // Specifies that it contains currency data.
        [Required(ErrorMessage = "Please enter Employee Pay")]
        [RegularExpression(@"^\$?\d+(\.\d{2})?$", ErrorMessage = "Please enter a valid amount.")] // Validates that the fee follows the specific format and provides a error message if not.
        [Display(Name = "Employee Pay")]
        public decimal Pay { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter Employee Pay")]
        [Display(Name = "Employee Hours")]
        //---
        public decimal Hours { get; set; }

        // Navigation property
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}



