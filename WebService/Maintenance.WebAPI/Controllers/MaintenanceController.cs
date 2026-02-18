using Microsoft.AspNetCore.Mvc;
using Maintenance.WebAPI.Services;
using Maintenance.WebAPI.Models;

namespace Maintenance.WebAPI.Controllers
{
    [ApiController]
    [Route("api/maintenance")]
    public class MaintenanceController : ControllerBase
    {
        private readonly IRepairHistoryService _service;

        public MaintenanceController(IRepairHistoryService service)
        {
            _service = service;
        }

        [HttpGet("vehicles/{vehicleId}/repairs")]
        public IActionResult GetRepairHistory(int vehicleId)
        {
            var history = _service.GetByVehicleId(vehicleId);
            return Ok(history);
        }

        [HttpGet("crash")]
        public IActionResult Crash()
        {
            int x = 0;
            int y = 5 / x;
            return Ok();
        }
    }
}
