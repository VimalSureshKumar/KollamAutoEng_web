using KollamAutoEng_web.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KollamAutoEng_web.Models
{
    // Enumeration for payment methods with display names for user-friendly representation
    public enum PaymentMethod
    {
        [Display(Name = "Credit Card")] // Display label for Credit Card
        CreditCard = 0,

        [Display(Name = "Debit Card")] // Display label for Debit Card
        DebitCard = 1,

        [Display(Name = "Online Banking")] // Display label for Online Banking
        Online_Banking = 2,

        Cash = 3 // No custom display name, default to "Cash"
    }

    // Payment class representing a payment transaction entity
    public class Payment
    {
        // Primary key for the Payment entity
        [Key] // Marks this property as the primary key in the database
        [Display(Name = "Payment ID")] // Specifies the display label for the field in the UI
        public int PaymentId { get; set; }

        // Amount of the payment, required with validation for currency, range, and format
        [DataType(DataType.Currency)] // Specifies that this field represents a currency value
        [Required(ErrorMessage = "Please enter Payment Amount")] // Ensures this field is mandatory
        [RegularExpression("^(0|[1-9][0-9]*)(\\.[0-9]+)?$", ErrorMessage = "Please enter a valid positive number.")] // Validates the input to allow only positive numbers with optional decimal values
        [Range(0, 500000, ErrorMessage = "Please enter a value between 0 and 500,000.")] // Enforces a valid range for the payment amount
        [Display(Name = "Payment Amount")] // Specifies the display label for the payment amount field in the UI
        public decimal Amount { get; set; }

        // Date of the payment, required and validated by a custom DateValidator
        [Required] // Ensures the payment date is mandatory
        [DateValidator(ErrorMessage = "The payment date must be within one year from today.")] // Custom validation attribute to check if the date is within a valid range (within one year)
        [DataType(DataType.Date)] // Specifies the field as a date type for correct formatting in UI
        [Display(Name = "Payment Date")] // Specifies the display label for the payment date in the UI
        public DateTime? PaymentDate { get; set; }

        // Method of payment (e.g., Credit Card, Debit Card, etc.), required field
        [Required] // Ensures the payment method is mandatory
        [Display(Name = "Payment Method")] // Specifies the display label for the payment method in the UI
        public PaymentMethod? PaymentMethod { get; set; }

        // Customer associated with the payment, required field
        [Required] // Ensures the customer is mandatory
        [Display(Name = "Customer")] // Specifies the display label for the customer in the UI
        public int CustomerId { get; set; }

        // Navigation property for the customer linked to the payment
        public virtual Customer? Customer { get; set; } // Represents the relationship between Payment and Customer
    }
}
