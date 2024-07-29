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
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Please enter Employee First Name")]
        [MaxLength(25)]
        [RegularExpression("^[A-Za-z]+( [A-Za-z]+)*$", ErrorMessage = "Only letters and single spaces between words are allowed.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Employee Last Name")]
        [MaxLength(25)]
        [RegularExpression("^[A-Za-z]+( [A-Za-z]+)*$", ErrorMessage = "Only letters and single spaces between words are allowed.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(17)]
        [RegularExpression(@"^(\+64|0)\d{2,4}[\s-]?\d{4}[\s-]?\d{3,4}$|^(\+91|0)\d{10}$", ErrorMessage = "Please enter a valid phone number in New Zealand or India format.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [MaxLength(10)]
        [Display(Name = "Employee Status")]
        public Status Status { get; set; }

        [Required(ErrorMessage = "Please enter Employee Pay")]
        [RegularExpression("^(0|[1-9][0-9]*)(\\.[0-9]+)?$", ErrorMessage = "Please enter a valid positive number.")]
        [Range(0, 100000, ErrorMessage = "Please enter a value between 0 and 100,000.")]
        [Display(Name = "Employee Pay")]
        public decimal Pay { get; set; }

        [Required(ErrorMessage = "Please enter Employee Hours")]
        [Range(0, 500, ErrorMessage = "Please enter a value between 0 and 500 hours.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Please enter a valid decimal value with up to two decimal places.")]
        [Display(Name = "Hours")]
        public decimal Hours { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}



