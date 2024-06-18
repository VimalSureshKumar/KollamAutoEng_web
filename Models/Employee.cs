using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public enum Status
    {
        Active, Inactive
    }
    public class Employee
    {
        [Key]
        [Display(Name = "Employee ID")]
        public int EmployeeId { get; set; } // Primary Key

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Employee First Name"), MaxLength(25)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Employee Last Name"), MaxLength(25)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber), MaxLength(17)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Status")]
        public Status Status { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter Employee Pay")]
        [RegularExpression(@"^\$?\d+(\.\d{2})?$", ErrorMessage = "Please enter a valid amount.")]
        [Display(Name = "Pay")]
        public decimal Pay { get; set; }

        [Required(ErrorMessage = "Please enter Employee Hours")]
        [Display(Name = "Hours")]
        public decimal Hours { get; set; }

        // Navigation property
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}



