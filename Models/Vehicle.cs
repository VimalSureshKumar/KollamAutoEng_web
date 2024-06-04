using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public class Vehicle
    {
        // This property represents the Vehicle's unique identifier.
        [Key]
        [Required]
        [Display(Name = "Vehicle ID")]
        public int VehicleId { get; set; } // Primary Key

        public string Brand { get; set; }

        public string Model { get; set; }

        public string VIN { get; set; }

        public string Registration { get; set; }

        public int Odometer { get; set; }

        public string DriveType { get; set; }

        [Required]
        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; } // Foreign Key to Customer

        // Navigation property
        public Customer Customer { get; set; }

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
