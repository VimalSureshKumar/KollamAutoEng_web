SELECT
    a.AppointmentId,
    a.AppointmentDate,
    c.FirstName + ' ' + c.LastName AS CustomerName,
    v.VIN,
    v.Registration,
    e.FirstName + ' ' + e.LastName AS EmployeeName,
    a.ServiceCost,
    a.AppointmentName
FROM
    Appointment a
INNER JOIN Customer c ON a.CustomerId = c.CustomerId
INNER JOIN Vehicle v ON a.VehicleId = v.VehicleId
INNER JOIN Employee e ON a.EmployeeId = e.EmployeeId;