# API Contract Documentation

## Versioning Strategy

- **Current Version:** v1
- **Strategy:** URI Versioning (e.g., `/api/v1/vehicles`). _Note: Currently implemented as `/api/vehicles` for simplicity as per assignment scope, but future versions will be namespaced._

## Resources: Vehicles

Base URL: `/api/vehicles`

### 1. Get All Vehicles

- **Endpoint:** `GET /api/vehicles`
- **Description:** Retrieves a list of all vehicles in the inventory.
- **Response:** `200 OK`
  ```json
  [
    {
      "id": 1,
      "vehicleCode": "CAR-001",
      "locationId": 101,
      "vehicleType": "Sedan",
      "status": "Available"
    }
  ]
  ```

### 2. Get Vehicle By ID

- **Endpoint:** `GET /api/vehicles/{id}`
- **Description:** Retrieves details of a specific vehicle.
- **Parameters:**
  - `id` (int): Unique identifier of the vehicle.
- **Responses:**
  - `200 OK`: Vehicle found.
  - `404 Not Found`: Vehicle with the given ID does not exist.

### 3. Create Vehicle

- **Endpoint:** `POST /api/vehicles`
- **Description:** Adds a new vehicle to the inventory.
- **Request Body:**
  ```json
  {
    "vehicleCode": "CAR-002",
    "locationId": 101,
    "vehicleType": "SUV"
  }
  ```
- **Validation Rules:**
  - `VehicleCode`: Required.
  - `LocationId`: Required, must be > 0.
  - `VehicleType`: Required.
- **Responses:**
  - `201 Created`: Vehicle successfully created. Returns location header and created object.
  - `400 Bad Request`: Validation failure or business rule violation.

### 4. Update Vehicle Status

- **Endpoint:** `PUT /api/vehicles/{id}/status`
- **Description:** Updates the status of a vehicle (e.g., mark as Rented, Returned, Serviced).
- **Request Body:** `string` (The new status, e.g., "Rented", "Available")
- **Validation Rules:**
  - Status must be one of: `Available`, `Reserved`, `Rented`, `UnderService`.
  - Domain rules:
    - Cannot rent if already rented.
    - Cannot rent if under service.
    - Cannot reserve if not available.
- **Responses:**
  - `204 No Content`: Status successfully updated.
  - `400 Bad Request`: Invalid status or illegal transition (Domain Rule Violation).
  - `404 Not Found`: Vehicle not found.

### 5. Delete Vehicle

- **Endpoint:** `DELETE /api/vehicles/{id}`
- **Description:** Removes a vehicle from the inventory.
- **Responses:**
  - `204 No Content`: Deletion successful.
  - `404 Not Found`: Vehicle not found.
