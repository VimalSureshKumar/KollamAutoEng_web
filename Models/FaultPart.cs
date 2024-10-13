using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KollamAutoEng_web.Models
{
    // FaultPart class representing a part that is faulty in relation to a fault, vehicle, customer, and appointment
    public class FaultPart
    {
        // Primary key for the FaultPart entity
        [Key] // Marks this property as the primary key in the database
        [Display(Name = "FaultPart ID")] // Specifies the display label for the field in the UI
        public int FaultPartId { get; set; }

        // Foreign key for Fault - required
        [Required] // Ensures this field is mandatory
        [Display(Name = "Fault")] // Specifies the display label for the fault field in the UI
        public int FaultId { get; set; }

        // Navigational property linking FaultPart to the associated Fault entity
        public virtual Fault? Fault { get; set; } // Defines the relationship between the FaultPart and Fault

        // Foreign key for Part - required
        [Required] // Ensures this field is mandatory
        [Display(Name = "Part")] // Specifies the display label for the part field in the UI
        public int PartId { get; set; }

        // Navigational property linking FaultPart to the associated Part entity
        public virtual Part? Part { get; set; } // Defines the relationship between the FaultPart and Part

        // Foreign key for Appointment - required
        [Required] // Ensures this field is mandatory
        [Display(Name = "Appointment")] // Specifies the display label for the appointment field in the UI
        public int AppointmentId { get; set; }

        // Navigational property linking FaultPart to the associated Appointment entity
        public virtual Appointment? Appointment { get; set; } // Defines the relationship between the FaultPart and Appointment

        // Foreign key for Customer - required
        [Required] // Ensures this field is mandatory
        [Display(Name = "Customer")] // Specifies the display label for the customer field in the UI
        public int CustomerId { get; set; }

        // Navigational property linking FaultPart to the associated Customer entity
        public virtual Customer? Customer { get; set; } // Defines the relationship between the FaultPart and Customer

        // Foreign key for Vehicle - required
        [Required] // Ensures this field is mandatory
        [Display(Name = "Vehicle")] // Specifies the display label for the vehicle field in the UI
        public int VehicleId { get; set; }

        // Navigational property linking FaultPart to the associated Vehicle entity
        public virtual Vehicle? Vehicle { get; set; } // Defines the relationship between the FaultPart and Vehicle
    }
}
