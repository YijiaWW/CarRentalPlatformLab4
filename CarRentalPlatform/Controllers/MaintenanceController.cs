using System.Net.Http.Json;
using CarRentalPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CarRentalPlatform.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<MaintenanceController> _logger;

        public MaintenanceController(IHttpClientFactory httpClientFactory, ILogger<MaintenanceController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult History()
        {
            return View(new List<RepairHistoryViewModel>());
        }

        [HttpPost]
        public async Task<IActionResult> History(int vehicleId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("MaintenanceApi");
                var response = await client.GetAsync($"api/maintenance/vehicles/{vehicleId}/repairs");

                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.ErrorMessage = "Maintenance service is temporarily unavailable. Please try again.";
                    return View(new List<RepairHistoryViewModel>());
                }

                var repairs = await response.Content.ReadFromJsonAsync<List<RepairHistoryViewModel>>();
                return View(repairs ?? new List<RepairHistoryViewModel>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling maintenance API for vehicle {VehicleId}", vehicleId);
                ViewBag.ErrorMessage = "Unable to load repair history right now. Please verify API settings and try again.";
                return View(new List<RepairHistoryViewModel>());
            }
        }
    }
}
