using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KollamAutoEng_web.Models
{
    public class FaultPart
    {
        [Key]
        [Display(Name = "FaultPart ID")]
        public int FaultPartId { get; set; }

        [Required]
        [Display(Name = "Fault ID")]
        public int FaultId { get; set; }

        public virtual Fault Fault { get; set; }

        [Required]
        [Display(Name = "Part ID")]
        public int PartId { get; set; }

        public virtual Part Part { get; set; }

        [Required]
        [Display(Name = "Appointment ID")]
        public int AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; }
    }
}
