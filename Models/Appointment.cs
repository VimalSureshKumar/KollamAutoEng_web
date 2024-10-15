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
        [MaxLength(25, ErrorMessage = "Appointment Name cannot exceed 25 characters.")] // Maximum of 10 characters allowed
        [MinLength(3, ErrorMessage = "Appointment Name must be at least 3 characters long.")] // Minimum of 3 characters required
        [RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = "Only letters and spaces are allowed.")] // Restricts input to letters and spaces only
        [Display(Name = "Appointment Name")] // Display label for UI
        public string AppointmentName { get; set; }

        // Appointment date - required and validated by a custom attribute
        [Required] // Field is mandatory
        [DateValidator(ErrorMessage = "The appointment date must be within one year from today.")] // Custom validation attribute to check if date is within one year
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
        [RegularExpression("^(0|[1-9][0-9]*)(\\.[0-9]+)?$", ErrorMessage = "Please enter a valid positive number.")] // Validates the input as a positive number (with decimals allowed)
        [Range(0.99, 50000, ErrorMessage = "Please enter a value between 0.99 and 50,000.")] // Ensures the service cost falls within the specified range
        [Display(Name = "Service Cost")] // Display label for UI
        public decimal ServiceCost { get; set; }

        // Collection of fault parts associated with the appointment (optional)
        public virtual ICollection<FaultPart>? FaultParts { get; set; } // Navigational property for related fault parts
    }
}
