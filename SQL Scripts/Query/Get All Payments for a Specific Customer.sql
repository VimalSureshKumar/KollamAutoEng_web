SELECT
    p.PaymentId,
    p.Amount,
    p.PaymentDate,
    c.FirstName + ' ' + c.LastName AS CustomerName,
    p.PaymentMethod
FROM
    Payment p
INNER JOIN Customer c ON p.CustomerId = c.CustomerId
WHERE
    c.FirstName = 'Noah' AND c.LastName = 'Brown'; -- Replace with the desired customer name