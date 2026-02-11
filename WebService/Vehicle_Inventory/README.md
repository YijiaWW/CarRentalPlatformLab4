# Vehicle Inventory Microservice

## Architecture Overview

This microservice follows **Clean Architecture** principles and **Domain-Driven Design (DDD)**.
It is structured into four distinct layers to ensure separation of concerns, testability, and maintainability.

### Layers

1.  **Domain (`VehicleInventory.Domain`)**
    - **Purpose:** The core of the application. Contains enterprise logic and business rules.
    - **Components:** `Vehicle` Entity, `VehicleStatus` Enum, `VehicleDomainException`.
    - **Dependencies:** None. Independent of frameworks and external libraries.

2.  **Application (`VehicleInventory.Application`)**
    - **Purpose:** Orchestrates use cases and application logic.
    - **Components:** `IVehicleService` (Use Cases), `IVehicleRepository` (Interface), DTOs (`VehicleDto`, `CreateVehicleDto`).
    - **Dependencies:** Refers to **Domain**. No dependence on Infrastructure or WebAPI.

3.  **Infrastructure (`VehicleInventory.Infrastructure`)**
    - **Purpose:** Implements interfaces defined in the Application layer and handles external concerns (Database, I/O).
    - **Components:** `InventoryDbContext` (EF Core), `VehicleRepository` (Implementation).
    - **Dependencies:** Refers to **Application**. Uses Entity Framework Core (SQL Server).

4.  **WebAPI (`VehicleInventory.WebAPI`)**
    - **Purpose:** Entry point for the application. Exposes RESTful endpoints.
    - **Components:** `VehiclesController`, Swagger Configuration, Dependency Injection setup.
    - **Dependencies:** Refers to **Application** and **Infrastructure**.

## Domain Model & Business Rules

### Vehicle Aggregate

The `Vehicle` entity serves as the aggregate root and enforces the following invariants:

- **Identity:** Vehicles are identified by an internal `Id` and a business key `VehicleCode`.
- **Status Management:** Status transitions are encapsulated in methods (`MarkAvailable`, `MarkRented`, etc.) rather than public setters.

### Key Business Rules

1.  **Rentals:**
    - A vehicle cannot be rented if it is already `Rented`.
    - A vehicle cannot be rented if it is `UnderService`.
    - A vehicle cannot be rented directly if it is `Reserved` (must be explicitly released or handled via specific workflow).
2.  **Reservations:**
    - Only `Available` vehicles can be marked as `Reserved`.
3.  **Service:**
    - A `Rented` vehicle cannot be marked as `UnderService` (must be returned first).

## Run Instructions

### Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or Express)

### Setup & Run

1.  **Clone the repository**.
2.  **Navigate to the project folder:**
    ```bash
    cd WebService/Vehicle_Inventory
    ```
3.  **Apply Database Migrations:**
    ```bash
    dotnet ef database update --project VehicleInventory.Infrastructure --startup-project VehicleInventory.WebAPI
    ```
4.  **Run the API:**
    ```bash
    dotnet run --project VehicleInventory.WebAPI
    ```
5.  **Access Swagger UI:**
    Open your browser to `https://localhost:5001/swagger` (or the port indicated in the terminal).

## Known Limitations

- **Authentication:** Currently operating without auth (public API).
- **Versioning:** Basic implementation; full URI versioning not enforced in routing.
- **Concurrency:** Optimistic concurrency control is not explicitly handled in this version.
