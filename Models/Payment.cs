using KollamAutoEng_web.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KollamAutoEng_web.Models
{
    public class Payment
    {
        [Key]
        [Display(Name = "Payment ID")]
        public int PaymentId { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Please enter Payment Amount")]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        [Required]
        [DateValidator(ErrorMessage = "The payment date must be within one year from today.")]
        [DataType(DataType.Date)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
