USE VehicleInventoryDb;
GO

/* ============================
   DROP TABLES (IF EXISTS)
   ============================ */

DROP TABLE IF EXISTS dbo.yijiawang_61005795_Inventory;
DROP TABLE IF EXISTS dbo.yijiawang_61005795_Vehicle;
DROP TABLE IF EXISTS dbo.yijiawang_61005795_VehicleType;
DROP TABLE IF EXISTS dbo.yijiawang_61005795_VehicleStatus;
DROP TABLE IF EXISTS dbo.yijiawang_61005795_VehicleLocation;
GO

/* ============================
   CREATE TABLES
   ============================ */

CREATE TABLE dbo.yijiawang_61005795_VehicleType (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE dbo.yijiawang_61005795_VehicleStatus (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(30) NOT NULL UNIQUE
);
GO

CREATE TABLE dbo.yijiawang_61005795_VehicleLocation (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL UNIQUE
);
GO

CREATE TABLE dbo.yijiawang_61005795_Vehicle (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Make NVARCHAR(50) NOT NULL,
    Model NVARCHAR(50) NOT NULL,
    VehicleTypeId INT NOT NULL,
    CONSTRAINT FK_yijiawang_61005795_Vehicle_VehicleType
        FOREIGN KEY (VehicleTypeId)
        REFERENCES dbo.yijiawang_61005795_VehicleType(Id)
);
GO

CREATE TABLE dbo.yijiawang_61005795_Inventory (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VehicleId INT NOT NULL,
    VehicleLocationId INT NOT NULL,
    VehicleStatusId INT NOT NULL,
    LastUpdated DATETIME2 NOT NULL DEFAULT SYSDATETIME(),

    CONSTRAINT FK_yijiawang_61005795_Inventory_Vehicle
        FOREIGN KEY (VehicleId)
        REFERENCES dbo.yijiawang_61005795_Vehicle(Id),

    CONSTRAINT FK_yijiawang_61005795_Inventory_Location
        FOREIGN KEY (VehicleLocationId)
        REFERENCES dbo.yijiawang_61005795_VehicleLocation(Id),

    CONSTRAINT FK_yijiawang_61005795_Inventory_Status
        FOREIGN KEY (VehicleStatusId)
        REFERENCES dbo.yijiawang_61005795_VehicleStatus(Id)
);
GO

/* ============================
   INSERT DATA
   ============================ */

INSERT INTO dbo.yijiawang_61005795_VehicleType (Name)
VALUES ('Sedan'), ('SUV'), ('Truck'), ('Van');
GO

INSERT INTO dbo.yijiawang_61005795_VehicleStatus (Name)
VALUES ('Available'), ('Reserved'), ('Rented'), ('Maintenance');
GO

INSERT INTO dbo.yijiawang_61005795_VehicleLocation (Name)
VALUES ('Kitchener'), ('Waterloo'), ('Cambridge'), ('Guelph');
GO

INSERT INTO dbo.yijiawang_61005795_Vehicle (Make, Model, VehicleTypeId)
VALUES
('Toyota', 'Camry', 1),
('Honda', 'Civic', 1),
('Ford', 'Escape', 2),
('Toyota', 'RAV4', 2),
('Ford', 'F-150', 3),
('Chevy', 'Express', 4);
GO

INSERT INTO dbo.yijiawang_61005795_Inventory
(VehicleId, VehicleLocationId, VehicleStatusId)
VALUES
(1, 1, 1),
(2, 1, 2),
(3, 2, 1),
(4, 2, 3),
(5, 3, 1),
(6, 3, 4),
(1, 4, 1),
(2, 4, 2),
(3, 1, 1),
(4, 2, 3);
GO
