using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class FaultPart
    {
        [Key]
        public int FaultPartId { get; set; } // Primary Key
        [Required]
        public int FaultId { get; set; } // Foreign Key to Fault
        [Required]
        public int PartId { get; set; } // Foreign Key to Part
        public int AppointmentId { get; set; } // Foreign Key to Appointment

        // Navigation properties
        public Fault Fault { get; set; }
        public Part Part { get; set; }
        public Appointment Appointment { get; set; }
    }
}
