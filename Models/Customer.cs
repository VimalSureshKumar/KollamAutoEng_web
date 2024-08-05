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

        [Required(ErrorMessage = "Please enter an phone number")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(17)] 
        [RegularExpression(@"^(\+64|0)\d{2,4}[\s-]?\d{4}[\s-]?\d{3,4}$|^(\+91|0)\d{10}$", ErrorMessage = "Please enter a valid phone number in New Zealand or India format.")]
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
