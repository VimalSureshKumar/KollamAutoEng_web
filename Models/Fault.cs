using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    public class Fault
    {
        // This property represents the Fault's unique identifier.
        [Key]
        [Required]
        [Display(Name = "Fault ID")]
        public int FaultId { get; set; } // Primary Key

        // ---
        // ---
        // ---
        public string Description { get; set; }

        // Navigation property
        public ICollection<FaultPart> FaultParts { get; set; } = new List<FaultPart>();
    }
}
