CREATE PROCEDURE GetAppointmentsByCustomer
    @CustomerId INT
AS
BEGIN
    SELECT 
        a.AppointmentId,
        a.AppointmentDate,
        a.ServiceCost,
        a.AppointmentName
    FROM 
        Appointment a
    WHERE 
        a.CustomerId = @CustomerId;
END