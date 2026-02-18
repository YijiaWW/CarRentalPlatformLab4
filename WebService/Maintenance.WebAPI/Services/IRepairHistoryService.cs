using Maintenance.WebAPI.Models;
using System.Collections.Generic;

namespace Maintenance.WebAPI.Services
{
    public interface IRepairHistoryService
    {
        List<RepairHistoryDto> GetByVehicleId(int vehicleId);
        RepairHistoryDto AddRepair(RepairHistoryDto repair);
    }
}
