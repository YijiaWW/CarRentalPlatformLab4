using System;

namespace VehicleInventory.Domain.Exceptions
{
    public class VehicleDomainException : Exception
    {
        public VehicleDomainException() { }
        public VehicleDomainException(string message) : base(message) { }
        public VehicleDomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
