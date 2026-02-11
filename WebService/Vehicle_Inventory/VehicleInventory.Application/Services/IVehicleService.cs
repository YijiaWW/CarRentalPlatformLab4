using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;

namespace VehicleInventory.Application.Services
{
    public interface IVehicleService
    {
        Task<VehicleDto> CreateVehicleAsync(CreateVehicleDto createDto);
        Task<VehicleDto?> GetVehicleByIdAsync(int id);
        Task<IEnumerable<VehicleDto>> GetAllVehiclesAsync();
        Task<bool> UpdateVehicleStatusAsync(int id, string status);
        Task<bool> DeleteVehicleAsync(int id);
    }
}
