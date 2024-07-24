using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public class Fault
    {
        [Key]
        [Display(Name = "Fault ID")]
        public int FaultId { get; set; }

        [Required]
        [Display(Name = "Fault Name")]
        public string FaultName { get; set; }

        [Required]
        [Display(Name = "Part Name")]
        public string PartName { get; set; }

        [Required]
        [Display(Name = "Vehicle ID")]
        public int VehicleId { get; set; }

        public virtual Vehicle Vehicle { get; set; }

        [Required]
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<FaultPart> FaultParts { get; set; } = new List<FaultPart>();
    }
}
