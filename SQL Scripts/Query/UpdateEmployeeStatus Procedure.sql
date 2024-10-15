CREATE PROCEDURE UpdateEmployeeStatus
    @EmployeeId INT,
    @NewStatus INT
AS
BEGIN
    IF @NewStatus IN (0, 1) -- Only allow 0 or 1 as valid statuses
    BEGIN
        UPDATE Employee
        SET Status = @NewStatus
        WHERE EmployeeId = @EmployeeId;

        SELECT 
            FirstName + ' ' + LastName AS EmployeeName,
            Status
        FROM 
            Employee
        WHERE 
            EmployeeId = @EmployeeId;
    END
    ELSE
    BEGIN
        RAISERROR('Invalid status value. Use 0 for inactive or 1 for active.', 16, 1);
    END
END
