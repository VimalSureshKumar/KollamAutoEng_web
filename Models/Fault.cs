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

        [Required(ErrorMessage = "Please enter Faulty Part")]
        [MaxLength(50, ErrorMessage = "The Fault Name cannot exceed 50 characters.")]
        [RegularExpression(@"^[A-Za-z\s]*$", ErrorMessage = "Only letters and spaces are allowed.")]
        [Display(Name = "Fault Name")]
        public string FaultName { get; set; }

        [Required]
        [Display(Name = "Vehicle")]
        public int VehicleId { get; set; }
        public virtual Vehicle? Vehicle { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }
        public virtual Customer? Customer { get; set; }

        public virtual ICollection<FaultPart>? FaultParts { get; set; }
    }
}
