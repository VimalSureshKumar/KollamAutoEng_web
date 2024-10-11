SET IDENTITY_INSERT [dbo].[Customer] ON

INSERT INTO [dbo].[Customer] ([CustomerId], [FirstName], [LastName], [Email], [PhoneNumber], [Gender], [DateOfBirth]) VALUES 
(1, N'Vimal Kumar', N'Suresh Kumar', N'vimal.kumar@example.com', N'02109011821', 0, N'2006-08-30 00:00:00'),
(2, N'Sophie', N'Thompson', N'sophie.thompson@example.com', N'02112345678', 1, N'1990-05-12 00:00:00'),
(3, N'Lucas', N'Nguyen', N'lucas.nguyen@example.com', N'02123456789', 0, N'1985-07-20 00:00:00'),
(4, N'Isabella', N'Garcia', N'isabella.garcia@example.com', N'02134567890', 1, N'1995-11-15 00:00:00'),
(5, N'Mohammed', N'Khan', N'mohammed.khan@example.com', N'02145678901', 0, N'2000-02-28 00:00:00'),
(6, N'Ava', N'Smith', N'ava.smith@example.com', N'02156789012', 1, N'1992-04-10 00:00:00'),
(7, N'Ethan', N'Johnson', N'ethan.johnson@example.com', N'02167890123', 0, N'1988-09-05 00:00:00'),
(8, N'Emma', N'Williams', N'emma.williams@example.com', N'02178901234', 1, N'1994-06-25 00:00:00'),
(9, N'Noah', N'Brown', N'noah.brown@example.com', N'02189012345', 0, N'1991-03-16 00:00:00'),
(10, N'Olivia', N'Jones', N'olivia.jones@example.com', N'02190123456', 1, N'1996-12-01 00:00:00'),
(11, N'Liam', N'Davis', N'liam.davis@example.com', N'02201234567', 0, N'1987-08-14 00:00:00'),
(12, N'Mia', N'Miller', N'mia.miller@example.com', N'02212345678', 1, N'1993-10-09 00:00:00'),
(13, N'Aiden', N'Wilson', N'aiden.wilson@example.com', N'02223456789', 0, N'1999-01-22 00:00:00'),
(14, N'Charlotte', N'Moore', N'charlotte.moore@example.com', N'02234567890', 1, N'1997-07-30 00:00:00'),
(15, N'Elijah', N'Taylor', N'elijah.taylor@example.com', N'02245678901', 0, N'1986-05-18 00:00:00'),
(16, N'Amelia', N'Anderson', N'amelia.anderson@example.com', N'02256789012', 1, N'1998-03-25 00:00:00'),
(17, N'James', N'Thomas', N'james.thomas@example.com', N'02267890123', 0, N'1990-12-13 00:00:00'),
(18, N'Harper', N'Jackson', N'harper.jackson@example.com', N'02278901234', 1, N'1992-09-11 00:00:00'),
(19, N'Benjamin', N'White', N'benjamin.white@example.com', N'02289012345', 0, N'1994-11-29 00:00:00'),
(20, N'Evelyn', N'Harris', N'evelyn.harris@example.com', N'02290123456', 1, N'1993-02-08 00:00:00'),
(21, N'Alexander', N'Martin', N'alexander.martin@example.com', N'02301234567', 0, N'1991-06-03 00:00:00'),
(22, N'Abigail', N'Thomas', N'abigail.thomas@example.com', N'02312345678', 1, N'1995-07-07 00:00:00'),
(23, N'William', N'Clark', N'william.clark@example.com', N'02323456789', 0, N'1988-11-22 00:00:00'),
(24, N'Scarlett', N'Hall', N'scarlett.hall@example.com', N'02334567890', 1, N'1999-05-15 00:00:00');

INSERT INTO [dbo].[Customer] ([CustomerId], [FirstName], [LastName], [Email], [PhoneNumber], [Gender], [DateOfBirth]) VALUES 
(25, N'Rajesh', N'Sharma', N'rajesh.sharma@example.in', N'9876543210', 0, N'1980-01-15 00:00:00'),
(26, N'Anjali', N'Patel', N'anjali.patel@example.in', N'9876543211', 1, N'1992-03-22 00:00:00'),
(27, N'Arjun', N'Gupta', N'arjun.gupta@example.in', N'9876543212', 0, N'1985-05-10 00:00:00'),
(28, N'Priya', N'Singh', N'priya.singh@example.in', N'9876543213', 1, N'1997-07-11 00:00:00'),
(29, N'Karan', N'Mehta', N'karan.mehta@example.in', N'9876543214', 0, N'1990-09-30 00:00:00'),
(30, N'Neha', N'Verma', N'neha.verma@example.in', N'9876543215', 1, N'1994-11-19 00:00:00');

SET IDENTITY_INSERT [dbo].[Customer] OFF
