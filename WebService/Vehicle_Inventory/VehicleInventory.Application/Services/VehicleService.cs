using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Entities;
using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _repository;

        public VehicleService(IVehicleRepository repository)
        {
            _repository = repository;
        }

        public async Task<VehicleDto> CreateVehicleAsync(CreateVehicleDto createDto)
        {
            // Application validation (Project requirement: "Validation rules related to use cases")
            if (createDto == null) throw new ArgumentNullException(nameof(createDto));

            var vehicle = new Vehicle(createDto.VehicleCode, createDto.LocationId, createDto.VehicleType);
            
            await _repository.AddAsync(vehicle);
            await _repository.SaveChangesAsync();

            return MapToDto(vehicle);
        }

        public async Task<VehicleDto?> GetVehicleByIdAsync(int id)
        {
            var vehicle = await _repository.GetByIdAsync(id);
            if (vehicle == null) return null;
            return MapToDto(vehicle);
        }

        public async Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync()
        {
            var vehicles = await _repository.GetAllAsync();
            return vehicles.Select(MapToDto);
        }

        public async Task<bool> UpdateVehicleStatusAsync(int id, string status)
        {
            var vehicle = await _repository.GetByIdAsync(id);
            if (vehicle == null) return false;

            // Normalize status string
            // Requirements: "Call domain behavior methods instead of changing status directly"
            switch (status?.ToLower())
            {
                case "available":
                    vehicle.MarkAvailable();
                    break;
                case "rented":
                    vehicle.MarkRented();
                    break;
                case "reserved":
                    vehicle.MarkReserved();
                    break;
                case "underservice":
                case "serviced":
                    vehicle.MarkServiced();
                    break;
                default:
                    throw new ArgumentException($"Invalid status: {status}. Allowed values: Available, Rented, Reserved, UnderService/Serviced.");
            }

            await _repository.UpdateAsync(vehicle);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _repository.GetByIdAsync(id);
            if (vehicle == null) return false;

            await _repository.DeleteAsync(vehicle);
            await _repository.SaveChangesAsync();
            return true;
        }

        private static VehicleDto MapToDto(Vehicle vehicle)
        {
            return new VehicleDto
            {
                Id = vehicle.Id,
                VehicleCode = vehicle.VehicleCode,
                LocationId = vehicle.LocationId,
                VehicleType = vehicle.VehicleType,
                Status = vehicle.Status.ToString()
            };
        }
    }
}
