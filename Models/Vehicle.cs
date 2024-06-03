using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; } // Primary Key
        public string Brand { get; set; }
        public string Model { get; set; }
        public string VIN { get; set; }
        public string Registration { get; set; }
        public int Odometer { get; set; }
        public string DriveType { get; set; }
        public int CustomerId { get; set; } // Foreign Key to Customer

        // Navigation property
        public Customer Customer { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
