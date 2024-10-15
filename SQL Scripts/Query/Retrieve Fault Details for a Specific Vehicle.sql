SELECT
    f.FaultId,
    f.FaultName,
    v.VIN,
    v.Registration,
    c.FirstName + ' ' + c.LastName AS CustomerName
FROM
    Fault f
INNER JOIN Vehicle v ON f.VehicleId = v.VehicleId
INNER JOIN Customer c ON f.CustomerId = c.CustomerId
WHERE
    v.VIN = '5NPE34AF8HH123456'; -- Replace with the desired vehicle's VIN