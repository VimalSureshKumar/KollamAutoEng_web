using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public class Fault
    {
        [Key]
        [Display(Name = "Fault ID")]
        public int FaultId { get; set; } // Primary Key

        [Required]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; } // Foreign Key to Vehicle

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; } // Foreign Key to Customer

        // Navigation properties
        public virtual Vehicle Vehicle { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<FaultPart> FaultParts { get; set; }
    }

}
