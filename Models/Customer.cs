using KollamAutoEng_web.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public enum Gender
    {
        Male, Female, Other, [Display(Name = "Prefer not to say")] Prefer_not_to_say
    }

    public class Customer
    {
        [Key]
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter Customer First Name")]
        [MaxLength(25)]
        [RegularExpression("^[A-Za-z]+( [A-Za-z]+)*$", ErrorMessage = "Only letters and single spaces between words are allowed.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Customer Last Name")]
        [MaxLength(25)]
        [RegularExpression("^[A-Za-z]+( [A-Za-z]+)*$", ErrorMessage = "Only letters and single spaces between words are allowed.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter an email address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

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

        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }

        [Required]
        [DateValidator2(ErrorMessage = "Invalid Date of Birth")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        public virtual ICollection<Vehicle>? Vehicles { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }
        public virtual ICollection<Fault>? Faults { get; set; }
        public virtual ICollection<Payment>? Payments { get; set; }
    }
}
