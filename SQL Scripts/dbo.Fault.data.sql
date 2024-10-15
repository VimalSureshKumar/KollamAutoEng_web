SET IDENTITY_INSERT [dbo].[Fault] ON;

INSERT INTO [dbo].[Fault] 
    ([FaultId], [FaultName], [VehicleId], [CustomerId]) 
VALUES 
    (1,  N'Brake Pad Failure', 1,  1),
    (2,  N'Engine Overheating', 2,  2),
    (3,  N'Air Filter Blockage', 3,  3),
    (4,  N'Oil Leak', 4,  4),
    (5,  N'Battery Failure', 5,  5),
    (6,  N'Headlight Malfunction', 6,  6),
    (7,  N'Tail Light Out', 7,  7),
    (8,  N'Spark Plug Issue', 8,  8),
    (9,  N'Radiator Leak', 9,  9),
    (10, N'Fuel Pump Failure', 10, 10),
    (11, N'Water Pump Failure', 11, 11),
    (12, N'Clutch Slippage', 12, 12),
    (13, N'Transmission Problem', 13, 13),
    (14, N'Window Motor Failure', 14, 14),
    (15, N'Door Handle Issue', 15, 15),
    (16, N'Ignition Coil Fault', 16, 16),
    (17, N'Stimulator Fault', 17, 17),
    (18, N'Wheel Bearing Noise', 18, 18),
    (19, N'Sway Bar Link Damage', 19, 19),
    (20, N'Control Arm Wear', 20, 20),
    (21, N'Ball Joint Issue', 21, 21),
    (22, N'Strut Failure', 22, 22),
    (23, N'Spring Damage', 23, 23),
    (24, N'Fuel Filter Blockage', 24, 24),
    (25, N'Fuse Failure', 25, 25),
    (26, N'Cooling Fan Failure', 26, 26),
    (27, N'Hubcap Loss', 27, 27),
    (28, N'Brake Rotor Wear', 28, 28),
    (29, N'Cabin Filter Blockage', 29, 29),
    (30, N'General Engine Issue', 30, 30);

SET IDENTITY_INSERT [dbo].[Fault] OFF;