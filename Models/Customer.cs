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

        [Required(ErrorMessage = "Please enter Customer First Name"), MaxLength(25)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter Customer Last Name"), MaxLength(25)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber), MaxLength(17)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please Enter valid Date of Birth")]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Fault> Faults { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
