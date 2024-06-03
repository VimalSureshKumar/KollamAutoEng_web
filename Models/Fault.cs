using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class Fault
    {
        [Key]
        public int FaultId { get; set; } // Primary Key
        public string Description { get; set; }

        // Navigation property
        public ICollection<FaultPart> FaultParts { get; set; }
    }
}
