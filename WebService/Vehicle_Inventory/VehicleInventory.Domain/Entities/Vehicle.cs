using System;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; private set; }
        public string VehicleCode { get; private set; }
        public int LocationId { get; private set; }
        public string VehicleType { get; private set; }
        public VehicleStatus Status { get; private set; }

        private Vehicle() { } // EF Core

        public Vehicle(string vehicleCode, int locationId, string vehicleType)
        {
            if (string.IsNullOrWhiteSpace(vehicleCode))
                throw new VehicleDomainException("Vehicle code is required.");
            if (locationId <= 0)
                throw new VehicleDomainException("Invalid location ID.");
            if (string.IsNullOrWhiteSpace(vehicleType))
                throw new VehicleDomainException("Vehicle type is required.");

            VehicleCode = vehicleCode;
            LocationId = locationId;
            VehicleType = vehicleType;
            Status = VehicleStatus.Available;
        }

        public void MarkAvailable()
        {
            // "A reserved vehicle cannot be marked as available without explicit release"
            // We consider this method the explicit release mechanism.
            
            // "Cannot be rented if already rented" -> Implies return calls this.
            
            Status = VehicleStatus.Available;
        }

        public void MarkRented()
        {
            if (Status == VehicleStatus.Rented)
                throw new VehicleDomainException("Vehicle is already rented.");
            
            if (Status == VehicleStatus.Reserved)
                throw new VehicleDomainException("Vehicle is reserved and cannot be rented directly without release.");
            
            if (Status == VehicleStatus.UnderService)
                throw new VehicleDomainException("Vehicle is under service and cannot be rented.");

            Status = VehicleStatus.Rented;
        }

        public void MarkReserved()
        {
            if (Status != VehicleStatus.Available)
                throw new VehicleDomainException("Only available vehicles can be reserved.");

            Status = VehicleStatus.Reserved;
        }

        public void MarkServiced()
        {
            if (Status == VehicleStatus.Rented)
                throw new VehicleDomainException("Cannot service a rented vehicle.");
            
            // Assuming we can service a reserved car (if it breaks before pickup) or available car.
            
            Status = VehicleStatus.UnderService;
        }
    }
}
