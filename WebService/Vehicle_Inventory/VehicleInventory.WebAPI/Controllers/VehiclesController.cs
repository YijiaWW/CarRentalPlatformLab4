using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Services;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVehicleDto createDto)
        {
            try
            {
                var createdVehicle = await _vehicleService.CreateVehicleAsync(createDto);
                return CreatedAtAction(nameof(GetById), new { id = createdVehicle.Id }, createdVehicle);
            }
            catch (VehicleDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the vehicle." });
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            try
            {
                var success = await _vehicleService.UpdateVehicleStatusAsync(id, status);
                if (!success) return NotFound();
                return NoContent();
            }
            catch (System.ArgumentException ex) // Service throws ArgumentException for invalid status
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (VehicleDomainException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _vehicleService.DeleteVehicleAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
