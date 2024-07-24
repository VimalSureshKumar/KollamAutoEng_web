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
        [Display(Name = "Fault")]
        public int FaultId { get; set; }

        public virtual Fault Fault { get; set; }

        [Required]
        [Display(Name = "Part")]
        public int PartId { get; set; }

        public virtual Part Part { get; set; }

        [Required]
        [Display(Name = "Appointment")]
        public int AppointmentId { get; set; }
        public virtual Appointment Appointment { get; set; }

        [Required]
        [Display(Name ="Customer")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [Required]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }
    }
}
