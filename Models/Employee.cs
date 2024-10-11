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

        [DataType(DataType.PhoneNumber), MaxLength(17)]
        [RegularExpression(@"^\+((64 (\b(2[0-6])\b)-\d{3,4}-\d{4,5})|(91 \d{5}-\d{5}))$",
        ErrorMessage = "Phone Number is not valid.\n\n" +
               "For New Zealand:\n" +
               "+64 followed by a 2-digit area code (20-26),\n" +
               "a 3- or 4-digit local number,\n" +
               "and a 4- or 5-digit subscriber number.\n" +
               "(e.g., +64 20-345-6789 or +64 22-1234-5678).\n\n" +
               "For India:\n" +
               "+91 followed by two groups of 5 digits separated by a hyphen.\n" +
               "(e.g., +91 75920-12345).")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Status")]
        public Status? Status { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter Employee Pay")]
        [RegularExpression("^(0|[1-9][0-9]*)(\\.[0-9]+)?$", ErrorMessage = "Please enter a valid positive number.")]
        [Range(0.99, 50000, ErrorMessage = "Please enter a value between 0 and 100,000.")]
        [Display(Name = "Pay")]
        public decimal Pay { get; set; }

        [Required(ErrorMessage = "Please enter Employee Hours")]
        [Range(0, 500, ErrorMessage = "Please enter a value between 0 and 500 hours.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Please enter a valid decimal value with up to two decimal places.")]
        [Display(Name = "Hours")]
        public decimal Hours { get; set; }

        public virtual ICollection<Appointment>? Appointments { get; set; }
    }
}
