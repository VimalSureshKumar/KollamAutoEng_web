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

        [Required(ErrorMessage = "Please enter Paymet Amount")]
        [RegularExpression("^(0|[1-9][0-9]*)(\\.[0-9]+)?$", ErrorMessage = "Please enter a valid positive number.")]
        [Range(0, 500000, ErrorMessage = "Please enter a value between 0 and 500,000.")]
        [Display(Name = "Payment Amount")]
        public decimal Amount { get; set; }

        [Required]
        [DateValidator(ErrorMessage = "The payment date must be within one year from today.")]
        [DataType(DataType.Date)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required]
        [Display(Name = "Customer")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
