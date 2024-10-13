using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KollamAutoEng_web.Models;
using Microsoft.AspNetCore.Identity;

namespace KollamAutoEng_web.Areas.Identity.Data
{
    // Custom user class inheriting from IdentityUser
    public class KollamAutoEng_webUser : IdentityUser
    {
        // Property for user's first name
        [Required(ErrorMessage = "Please enter First Name")] // Ensures the field is required
        [MaxLength(25)] // Limits the length to 25 characters
        [RegularExpression("^[A-Za-z]+( [A-Za-z]+)*$", ErrorMessage = "Only letters and single spaces between words are allowed.")] // Validates that the input only contains letters and single spaces
        [Display(Name = "First Name")] // Specifies how this property will be displayed in UI
        public string FirstName { get; set; }

        // Property for user's last name
        [Required(ErrorMessage = "Please enter Last Name")] // Ensures the field is required
        [MaxLength(25)] // Limits the length to 25 characters
        [RegularExpression("^[A-Za-z]+( [A-Za-z]+)*$", ErrorMessage = "Only letters and single spaces between words are allowed.")] // Validates input
        [Display(Name = "Last Name")] // Specifies the display name for the UI
        public string LastName { get; set; }

        // Property for user's email address
        [Required(ErrorMessage = "Please enter an email address")] // Ensures the field is required
        [DataType(DataType.EmailAddress)] // Specifies the data type for email
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")] // Validates the email format
        [EmailAddress] // Ensures the property is validated as an email
        [Display(Name = "Email")] // Specifies the display name for the UI
        public string Email { get; set; }

        // Property for user's phone number
        [DataType(DataType.PhoneNumber)] // Specifies the data type for phone number
        [MaxLength(17)] // Limits the length to 17 characters for international formats
        [RegularExpression(@"^\+((64 (\b(2[0-6])\b)-\d{3,4}-\d{4,5})|(91 \d{5}-\d{5}))$", // Validates international phone numbers for New Zealand and India
        ErrorMessage = "Phone Number is not valid.\n\n" +
               "For New Zealand:\n" +
               "+64 followed by a 2-digit area code (20-26),\n" +
               "a 3- or 4-digit local number,\n" +
               "and a 4- or 5-digit subscriber number.\n" +
               "(e.g., +64 20-345-6789 or +64 22-1234-5678).\n\n" +
               "For India:\n" +
               "+91 followed by two groups of 5 digits separated by a hyphen.\n" +
               "(e.g., +91 75920-12345).")]
        [Display(Name = "Phone Number")] // Specifies the display name for the UI
        public string PhoneNumber { get; set; }
    }
}
