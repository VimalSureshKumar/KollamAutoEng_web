using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KollamAutoEng_web.Models
{
    public class FaultPart
    {
        // This property represents the FaultPart's unique identifier.
        [Key]
        [Required]
        [Display(Name = "FaultPart ID")]
        public int FaultPartId { get; set; } // Primary Key

        [Required]
        [ForeignKey("FaultId")]
        public int FaultId { get; set; } // Foreign Key to Fault

        [Required]
        [ForeignKey("PartId")]
        public int PartId { get; set; } // Foreign Key to Part

        [Required]
        [ForeignKey("AppointmentId")]
        public int AppointmentId { get; set; } // Foreign Key to Appointment

        // Navigation properties
        public Fault Fault { get; set; }
        public Part Part { get; set; }
        public Appointment Appointment { get; set; }
    }
}
