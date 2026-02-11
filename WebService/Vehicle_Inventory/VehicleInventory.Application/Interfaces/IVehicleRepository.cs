using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleInventory.Domain.Entities;

namespace VehicleInventory.Application.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle?> GetByIdAsync(int id);
        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle); // EF Core tracks changes, but sometimes explicit update is used
        Task DeleteAsync(Vehicle vehicle);
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }
}
