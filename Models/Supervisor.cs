namespace KollamAutoEng_web.Models
{
    public class Supervisor
    {
        public int SupervisorId { get; set; }
        public string Supervisor_Name { get; set; }
        public string Supervisor_Phone_Number { get; set; }
        public bool Supervisor_Status { get; set; }
        public decimal Supervisor_Pay { get; set; }
        public decimal Supervisor_Hours { get; set; }
    }
}
