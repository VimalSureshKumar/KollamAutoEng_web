SET IDENTITY_INSERT [dbo].[Appointment] ON

INSERT INTO [dbo].[Appointment] ([AppointmentId], [AppointmentDate], [CustomerId], [VehicleId], [EmployeeId], [ServiceCost], [AppointmentName]) 
VALUES 
(1, N'2024-08-08 09:00:00', 1, 1, 1, CAST(175.00 AS Decimal(18, 2)), N'WOF'),
(2, N'2024-08-09 10:00:00', 2, 2, 2, CAST(200.00 AS Decimal(18, 2)), N'Tire Replacement'),
(3, N'2024-08-10 11:00:00', 3, 3, 3, CAST(150.00 AS Decimal(18, 2)), N'Engine Repair'),
(4, N'2024-08-11 09:30:00', 4, 4, 4, CAST(125.00 AS Decimal(18, 2)), N'Brake Check'),
(5, N'2024-08-12 10:30:00', 5, 5, 5, CAST(250.00 AS Decimal(18, 2)), N'Battery Replacement'),
(6, N'2024-08-13 14:00:00', 6, 6, 6, CAST(180.00 AS Decimal(18, 2)), N'AC Repair'),
(7, N'2024-08-14 15:30:00', 7, 7, 7, CAST(225.00 AS Decimal(18, 2)), N'Oil Change'),
(8, N'2024-08-15 08:30:00', 8, 8, 8, CAST(195.00 AS Decimal(18, 2)), N'Transmission Check'),
(9, N'2024-08-16 09:45:00', 9, 9, 9, CAST(170.00 AS Decimal(18, 2)), N'Suspension Check'),
(10, N'2024-08-17 11:15:00', 10, 10, 10, CAST(205.00 AS Decimal(18, 2)), N'Wiper Replacement'),
(11, N'2024-08-18 13:45:00', 11, 11, 11, CAST(210.00 AS Decimal(18, 2)), N'Fuel System Check'),
(12, N'2024-08-19 10:00:00', 12, 12, 12, CAST(160.00 AS Decimal(18, 2)), N'Exhaust Repair'),
(13, N'2024-08-20 12:30:00', 13, 13, 13, CAST(230.00 AS Decimal(18, 2)), N'Coolant Flush'),
(14, N'2024-08-21 14:00:00', 14, 14, 14, CAST(240.00 AS Decimal(18, 2)), N'Tire Alignment'),
(15, N'2024-08-22 09:15:00', 15, 15, 15, CAST(135.00 AS Decimal(18, 2)), N'Light Replacement'),
(16, N'2024-08-23 11:00:00', 16, 16, 16, CAST(145.00 AS Decimal(18, 2)), N'Radiator Service'),
(17, N'2024-08-24 13:00:00', 17, 17, 17, CAST(190.00 AS Decimal(18, 2)), N'Clutch Replacement'),
(18, N'2024-08-25 15:00:00', 18, 18, 18, CAST(155.00 AS Decimal(18, 2)), N'Spark Plug Replacement'),
(19, N'2024-08-26 08:45:00', 19, 19, 19, CAST(175.00 AS Decimal(18, 2)), N'Filter Change'),
(20, N'2024-08-27 10:30:00', 20, 20, 20, CAST(185.00 AS Decimal(18, 2)), N'Window Repair'),
(21, N'2024-08-28 12:00:00', 21, 21, 21, CAST(220.00 AS Decimal(18, 2)), N'Interior Cleaning'),
(22, N'2024-08-29 14:30:00', 22, 22, 22, CAST(195.00 AS Decimal(18, 2)), N'Steering Alignment'),
(23, N'2024-08-30 09:00:00', 23, 23, 23, CAST(165.00 AS Decimal(18, 2)), N'Timing Belt Check'),
(24, N'2024-08-31 11:15:00', 24, 24, 24, CAST(180.00 AS Decimal(18, 2)), N'Air Filter Change'),
(25, N'2024-09-01 13:45:00', 25, 25, 25, CAST(205.00 AS Decimal(18, 2)), N'Alternator Repair'),
(26, N'2024-09-02 10:00:00', 26, 26, 26, CAST(175.00 AS Decimal(18, 2)), N'Wheel Balancing'),
(27, N'2024-09-03 12:30:00', 27, 27, 27, CAST(185.00 AS Decimal(18, 2)), N'Gearbox Service'),
(28, N'2024-09-04 14:00:00', 28, 28, 28, CAST(195.00 AS Decimal(18, 2)), N'Engine Overhaul'),
(29, N'2024-09-05 09:15:00', 29, 29, 29, CAST(165.00 AS Decimal(18, 2)), N'Fuel Pump Replacement'),
(30, N'2024-09-06 11:00:00', 30, 30, 30, CAST(190.00 AS Decimal(18, 2)), N'Suspension Tuning')

SET IDENTITY_INSERT [dbo].[Appointment] OFF