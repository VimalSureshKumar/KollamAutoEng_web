using KollamAutoEng_web.ValidationAttributes;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace KollamAutoEng_web.Models
{
    // Appointment class representing appointment details for service booking
    public class Appointment
    {
        // Primary key for the Appointment entity
        [Key] // Marks this property as the primary key
        [Display(Name = "Appointment ID")] // Specifies the display label for the field in the UI
        public int AppointmentId { get; set; }

        // Name of the appointment - required with min and max length validation
        [Required(ErrorMessage = "Please enter valid Appointment Name")] // Field is mandatory
        [MaxLength(30, ErrorMessage = "Appointment Name cannot exceed 25 characters.")] // Maximum of 30 characters allowed
        [MinLength(3, ErrorMessage = "Appointment Name must be at least 3 characters long.")] // Minimum of 3 characters required
        [RegularExpression("^[A-Z][a-zA-Z\\s]*$", ErrorMessage = "First letter must be capitalized, and only letters and spaces are allowed.")]
        [Display(Name = "Appointment Name")] // Display label for UI
        public string AppointmentName { get; set; }

        // Appointment date - required and validated by a custom attribute
        [Required] // Field is mandatory
        [DateValidator(ErrorMessage = "The appointment date must be within 2 weeks from today.")] // Custom validation attribute to check if date is within one year
        [DataType(DataType.Date)] // Specifies that the field should be treated as a date
        [Display(Name = "Appointment Date")] // Display label for UI
        public DateTime? AppointmentDate { get; set; }

        // Foreign key for Customer - required
        [Required(ErrorMessage = "Please Select Customer")] // Field is mandatory
        [Display(Name = "Customer")] // Display label for UI
        public int CustomerId { get; set; }

        // Navigational property to link the appointment with the Customer entity
        public virtual Customer? Customer { get; set; }

        // Foreign key for Vehicle - required
        [Required(ErrorMessage = "Please Select Vehicle")] // Field is mandatory
        [Display(Name = "Vehicle")] // Display label for UI
        public int VehicleId { get; set; }

        // Navigational property to link the appointment with the Vehicle entity
        public virtual Vehicle? Vehicle { get; set; }

        // Foreign key for Employee - required
        [Required(ErrorMessage = "Please Select Employee")] // Field is mandatory
        [Display(Name = "Employee")] // Display label for UI
        public int EmployeeId { get; set; }

        // Navigational property to link the appointment with the Employee entity
        public virtual Employee? Employee { get; set; }

        // Service cost - required, validated as currency, and restricted to a valid range
        [DataType(DataType.Currency)] // Specifies that the field should be treated as a currency value
        [Required(ErrorMessage = "Please enter Payment Amount")] // Field is mandatory
        [RegularExpression(@"^(0|[1-9][0-9]{0,4}(,[0-9]{3})*)(\.[0-9]{1,2})?$", ErrorMessage = "Please enter a valid positive number (e.g., 50,000 or 50000).")]
        // This regex allows numbers with optional commas as thousands separators and decimals with up to 2 decimal places.
        [Range(0.99, 50000, ErrorMessage = "Please enter a value between 0.99 and 50,000.")]
        [Display(Name = "Service Cost")] // Display label for UI
        public decimal ServiceCost { get; set; }

        // Collection of fault parts associated with the appointment
        public virtual ICollection<FaultPart>? FaultParts { get; set; } // Navigational property for related fault parts
    }
}
