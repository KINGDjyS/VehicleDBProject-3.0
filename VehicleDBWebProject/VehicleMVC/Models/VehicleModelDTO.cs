using System.ComponentModel.DataAnnotations;

namespace VehicleMVC.Models
{
    public class VehicleModelDTO
    {
        public int VehicleModelId { get; set; }
        [Required]
        public int VehicleMakeId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
