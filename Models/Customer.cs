using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string DateOfBirth { get; set; }
        public string Reference { get; set; }

        // Navigation property
        public ICollection<Vehicle> Vehicles { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
