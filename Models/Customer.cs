using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public class Customer
    {
        // This property represents the Customer's unique identifier.
        [Key]
        [Required]
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; } // Primary Key

        // This property represents the Customer's first name.
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Customer First Name"), MaxLength(25)]
        //---
        public string FirstName { get; set; }

        // This property represents the Customer's first name.
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Customer First Name"), MaxLength(25)]
        //----
        public string LastName { get; set; }

        // This property represents the Customer's email address.
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email is not valid.")]
        [Display(Name = "Customer Email")]
        public string Email { get; set; }

        // This property represents the Customer's phone number.
        [DataType(DataType.PhoneNumber), MaxLength(17)]
        //---
        [Display(Name = "Customer Phone Number")]
        public string PhoneNumber { get; set; }

        // This property represents the Customer's gender.
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Gender"), MaxLength(6)] // Limits the gender to a maximum of 6 characters.
        [RegularExpression(@"^(Male|Female|Other)$", ErrorMessage = "Please enter a valid gender.")] // Validates that the gender follows the specific format and provides a error message if not.
        [Display(Name = "Customer Gender")]
        public string Gender { get; set; }

        // This property represents the Customer's date of birth.
        [DataType(DataType.Date)] // Specifies that it contains a date.
        [Required(ErrorMessage = "Please Enter valid Date of Birth")] // Ensures that the date of birth is mandatory and provides a error message if not provided.
        [Display(Name = "Customer Date of Birth")]
        public string DateOfBirth { get; set; }

        [DataType(DataType.Custom)]
        [Required(ErrorMessage = "Please enter Reference"), MaxLength(15)]
        //---
        public string Reference { get; set; }

        // Navigation property
        public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
