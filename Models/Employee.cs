using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
        public decimal Pay { get; set; }
        public decimal Hours { get; set; }

        // Navigation property
        public ICollection<Appointment> Appointments { get; set; }
    }
}



