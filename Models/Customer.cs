using KollamAutoEng_web.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    // Enumeration for customer gender options, with a display name for "Prefer not to say"
    public enum Gender
    {
        Male,
        Female,
        Other,
        [Display(Name = "Prefer not to say")] Prefer_not_to_say
    }

    public class Customer
    {
        [Key] // Specifies that CustomerId is the primary key
        [Display(Name = "Customer ID")] // Display name for CustomerId in views
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter Customer First Name")] // Ensures first name is mandatory
        [MinLength(3)] // Ensures the name has a minimum of 3 characters
        [MaxLength(25)] // Ensures the name has a maximum of 25 characters
        [RegularExpression(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$", ErrorMessage = "Each word must start with a capital letter, and only letters and single spaces are allowed.")]
        // Ensures that the first letter of each word is capitalized and that only letters and single spaces are allowed
        [Display(Name = "First Name")] // Display name for FirstName in views
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Customer Last Name")] // Ensures last name is mandatory
        [MinLength(3)] // Ensures the last name has a minimum of 3 characters
        [MaxLength(25)] // Ensures the last name has a maximum of 25 characters
        [RegularExpression(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$", ErrorMessage = "Each word must start with a capital letter, and only letters and single spaces are allowed.")]
        // Ensures that each word in the last name starts with a capital letter, followed by lowercase letters, and only letters and single spaces are allowed
        [Display(Name = "Last Name")] // Display name for LastName in views
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter an email address")] // Ensures email address is mandatory
        [MaxLength(50)] // Limits email length to 50 characters
        [DataType(DataType.EmailAddress)] // Specifies that this is an email field
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
        // Ensures the email follows a valid format (e.g., example@domain.com)
        [EmailAddress] // Provides further validation for email address format
        [Display(Name = "Email")] // Display name for Email in views
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber), MaxLength(17)] // Specifies that this is a phone number field, with a maximum length of 17 characters
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
        // Validates phone number format for New Zealand and India
        [Display(Name = "Phone Number")] // Display name for PhoneNumber in views
        public string PhoneNumber { get; set; }

        [Display(Name = "Gender")] // Display name for Gender in views
        public Gender? Gender { get; set; } // Nullable Gender field (can be Male, Female, Other, or Prefer_not_to_say)

        [Required] // Ensures Date of Birth is mandatory
        [DateValidator2(ErrorMessage = "Invalid Date of Birth")] // Custom validation attribute for date of birth
        [DataType(DataType.Date)] // Specifies that this is a date field
        [Display(Name = "Date of Birth")] // Display name for DateOfBirth in views
        public DateTime? DateOfBirth { get; set; } // Nullable DateTime for storing Date of Birth

        // Navigation properties representing related entities
        public virtual ICollection<Vehicle>? Vehicles { get; set; } // A customer can have multiple vehicles
        public virtual ICollection<Appointment>? Appointments { get; set; } // A customer can have multiple appointments
        public virtual ICollection<Fault>? Faults { get; set; } // A customer can have multiple fault records
        public virtual ICollection<Payment>? Payments { get; set; } // A customer can have multiple payments
    }
}
