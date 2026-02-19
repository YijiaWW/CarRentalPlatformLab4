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

        [HttpGet]
        public async Task<IActionResult> Usage()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("MaintenanceApi");
                // Using the correct route "api/maintenance/usage" based on our API implementation
                var result = await client.GetFromJsonAsync<object>("api/maintenance/usage");
                return View(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling usage endpoint");
                ViewBag.ErrorMessage = "Unable to load usage statistics.";
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Transfer(int fromId, int toId, decimal amount)
        {
            // Note: The API endpoint for Transfer has not been implemented in the Maintenance.WebAPI yet.
            // This code assumes an endpoint exists at "api/maintenance/transfer".
            
            if (amount <= 0) return View(); // Simple check or just return view if no parameters

            try
            {
                var client = _httpClientFactory.CreateClient("MaintenanceApi");
                var response = await client.PostAsync(
                    $"api/maintenance/transfer?fromId={fromId}&toId={toId}&amount={amount}",
                    null);
                
                var content = await response.Content.ReadAsStringAsync();
                ViewBag.Result = content;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling transfer endpoint");
                ViewBag.Result = "Error executing transfer.";
            }

            return View();
        }
    }
}
