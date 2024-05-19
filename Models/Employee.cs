using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class Employees
    {
        [Required] // It ensures that the CoachId is mandatory and cannot be null or left empty.
        [Display(Name = "Employee ID")] // Sets the display name for this property.
        [Key]
        public int EmployeeId { get; set; }

        // This property represents the Employee's full name.
        [DataType(DataType.Text)] // Specifies that it contains text data.
        [Required(ErrorMessage = "Please enter Full Name"), MaxLength(40)] // Makes sure the full name is mandatory and provides a error message if not provided and Limits the full name to a maximum of 50 characters.
        [RegularExpression(@"^[A-Z][a-zA-Z]*(\s[A-Z][a-zA-Z]*){1,2}$", ErrorMessage = "Please enter a valid fullname starting with capital letters.")] // Validates that the full name follows the specific format and provides a error message if not.
        [Display(Name = "Employee Full Name")]
        public string Employee_Name { get; set; }

        // This property represents the Employee's phone number.
        [DataType(DataType.PhoneNumber)] // Specifies that it contains a phone number.
        [Required(ErrorMessage = "Please enter Mobile Number"), MaxLength(17)] // Ensures the phone number is mandatory and provides a error message if not provided and Limits the phone number to a maximum of 17 characters.
        [RegularExpression(@"^\(\+64\) \d{2}-\d{3}-\d{4}$", ErrorMessage = "Phone Number is not valid. Accepted format (+64) 12-345-6789 for example.")] // Validates that the phone number follows the specific format and provides a error message if not and how it is accepted.
        [Display(Name = "Phone Number")]
        public string Employee_Phone_Number { get; set;}

        public bool Employee_Status { get; set; }

        // This property represents the Employee's fee.
        [DataType(DataType.Currency)] // Specifies that it contains currency data.
        [Required(ErrorMessage = "Please enter Amount")]
        [RegularExpression(@"^\$?\d+(\.\d{2})?$", ErrorMessage = "Please enter a valid amount.")] // Validates that the fee follows the specific format and provides a error message if not.
        [Display(Name = "Employee Pay")]
        public decimal Employee_Pay { get; set; }

        // This property represents the Employee's Hours.
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "Please enter Hours Worked by Employee")]
        [Display(Name = "Employee Hour")]
        public decimal Employee_Hours { get; set;}
    }
}
