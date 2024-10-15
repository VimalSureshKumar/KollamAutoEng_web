CREATE PROCEDURE GetCustomerAppointmentDetails
    @CustomerId INT
AS
BEGIN
    SELECT 
        a.AppointmentId,
        a.AppointmentDate,
        a.ServiceCost,
        a.AppointmentName,
        v.VIN,
        v.Registration,
        e.FirstName + ' ' + e.LastName AS EmployeeName,
        (SELECT SUM(ServiceCost) 
         FROM Appointment 
         WHERE CustomerId = @CustomerId) AS TotalServiceCost
    FROM 
        Appointment a
    INNER JOIN Vehicle v ON a.VehicleId = v.VehicleId
    INNER JOIN Employee e ON a.EmployeeId = e.EmployeeId
    WHERE 
        a.CustomerId = @CustomerId;
END
