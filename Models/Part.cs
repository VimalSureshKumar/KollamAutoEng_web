using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class Part
    {
        [Key]
        public int PartId { get; set; } // Primary Key
        public string Reference { get; set; }
        public string PartName { get; set; }
        public decimal Cost { get; set; }

        // Navigation property
        public ICollection<FaultPart> FaultParts { get; set; }
    }
}
