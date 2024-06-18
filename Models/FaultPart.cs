using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KollamAutoEng_web.Models
{
    public class FaultPart
    {
        [Key]
        [Display(Name = "FaultPart ID")]
        public int FaultPartId { get; set; } // Primary Key

        [Required]
        [Display(Name = "Fault")]
        public int FaultId { get; set; } // Foreign Key to Fault

        [Required]
        [Display(Name = "Part")]
        public int PartId { get; set; } // Foreign Key to Part

        [Required]
        [Display(Name = "Appointment")]
        public int AppointmentId { get; set; } // Foreign Key to Appointment

        // Navigation properties
        public virtual Fault Fault { get; set; }

        public virtual Part Part { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}
