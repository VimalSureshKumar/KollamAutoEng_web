using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KollamAutoEng_web.Models
{
    public class Payment
    {
        [Key]
        [Display(Name = "Payment ID")]
        public int PaymentId { get; set; } // Primary Key

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter Payment Amount")]
        [RegularExpression(@"^\$?\d+(\.\d{2})?$", ErrorMessage = "Please enter a valid amount.")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Display(Name = "Appointment")]
        public int AppointmentId { get; set; } // Foreign Key to Appointment

        // Navigation property
        public virtual Appointment Appointment { get; set; }

        public decimal CalculateTotalCost()
        {
            decimal totalCost = Appointment.ServiceCost;
            foreach (var faultPart in Appointment.FaultParts)
            {
                totalCost += faultPart.Part.Cost;
            }
            return totalCost;
        }
    }
}
