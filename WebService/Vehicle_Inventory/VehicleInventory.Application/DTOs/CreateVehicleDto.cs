using System.ComponentModel.DataAnnotations;

namespace VehicleInventory.Application.DTOs
{
    public class CreateVehicleDto
    {
        [Required]
        public string VehicleCode { get; set; } = string.Empty;
        
        [Required]
        [Range(1, int.MaxValue)]
        public int LocationId { get; set; }
        
        [Required]
        public string VehicleType { get; set; } = string.Empty;
    }
}
