using Maintenance.WebAPI.Models;
using System.Linq;
using System.Collections.Generic;

namespace Maintenance.WebAPI.Services
{
    public class FakeRepairHistoryService : IRepairHistoryService
    {
        private readonly List<RepairHistoryDto> _repairs = new()
        {
            new RepairHistoryDto
            {
                Id = 1,
                VehicleId = 1,
                RepairDate = DateTime.Now.AddDays(-10),
                Description = "Oil change",
                Cost = 89.99m,
                PerformedBy = "Quick Lube"
            },
            new RepairHistoryDto
            {
                Id = 2,
                VehicleId = 1,
                RepairDate = DateTime.Now.AddDays(-40),
                Description = "Brake pad replacement",
                Cost = 350.00m,
                PerformedBy = "Auto Repair Pro"
            }
        };

        public List<RepairHistoryDto> GetByVehicleId(int vehicleId)
        {
            return _repairs.Where(r => r.VehicleId == vehicleId).ToList();
        }

        public RepairHistoryDto AddRepair(RepairHistoryDto repair)
        {
            repair.Id = _repairs.Max(r => r.Id) + 1;
            repair.VehicleId = repair.VehicleId <= 0 ? 1 : repair.VehicleId; // Default to 1 for test
            _repairs.Add(repair);
            return repair;
        }
    }
}
