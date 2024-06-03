using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; } // Primary Key
        public decimal Amount { get; set; }
        public string Date { get; set; }
        public int AppointmentId { get; set; } // Foreign Key to Appointment

        // Navigation property
        public Appointment Appointment { get; set; }

        // Method to calculate total cost including parts
        public decimal CalculateTotalCost() // Method for Total Cost
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
