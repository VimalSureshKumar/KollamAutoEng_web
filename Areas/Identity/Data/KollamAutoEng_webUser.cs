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
        [Required(ErrorMessage = "Please enter Customer First Name")] // Ensures first name is mandatory
        [MinLength(3)] // Ensures the name has a minimum of 3 characters
        [MaxLength(25)] // Ensures the name has a maximum of 25 characters
        [RegularExpression(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$", ErrorMessage = "Each word must start with a capital letter, and only letters and single spaces are allowed.")]
        // Ensures that the first letter of each word is capitalized and that only letters and single spaces are allowed
        [Display(Name = "First Name")] // Display name for FirstName in UI
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Customer Last Name")] // Ensures last name is mandatory
        [MinLength(3)] // Ensures the last name has a minimum of 3 characters
        [MaxLength(25)] // Ensures the last name has a maximum of 25 characters
        [RegularExpression(@"^([A-Z][a-z]+)(\s[A-Z][a-z]+)*$", ErrorMessage = "Each word must start with a capital letter, and only letters and single spaces are allowed.")]
        // Ensures that each word in the last name starts with a capital letter, followed by lowercase letters, and only letters and single spaces are allowed
        [Display(Name = "Last Name")] // Display name for LastName in UI
        public string LastName { get; set; }

        // Property for user's email address
        [Required(ErrorMessage = "Please enter an email address")] // Ensures the field is required
        [DataType(DataType.EmailAddress)] // Specifies the data type for email
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")] // Validates the email format
        [EmailAddress] // Ensures the property is validated as an email
        [Display(Name = "Email")] // Specifies the display name for the UI
        public string Email { get; set; }

        // Property for user's phone number
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
        [Display(Name = "Phone Number")] // Display name for PhoneNumber in UI
        public string PhoneNumber { get; set; }
    }
}
