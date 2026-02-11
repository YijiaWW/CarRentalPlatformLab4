USE [VehicleInventoryDb];
GO

INSERT INTO [Vehicles] ([VehicleCode], [LocationId], [VehicleType], [Status])
VALUES 
('CAR-001', 101, 'Compact Sedan', 'Available'),
('CAR-002', 101, 'Midsize SUV', 'Rented'),
('CAR-003', 102, 'Luxury Sedan', 'Reserved'),
('CAR-004', 102, 'Minivan', 'UnderService'),
('CAR-005', 103, 'Pickup Truck', 'Available'),
('CAR-006', 101, 'Electric Hatchback', 'Available'),
('CAR-007', 103, 'Convertible', 'Rented'),
('CAR-008', 102, 'Full-size SUV', 'Available');
GO
