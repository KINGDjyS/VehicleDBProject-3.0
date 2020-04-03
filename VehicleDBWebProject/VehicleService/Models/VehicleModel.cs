using System.ComponentModel.DataAnnotations;

namespace VehicleService
{
    public class VehicleModel
    {
        [Key]
        public int VehicleModelId { get; set; }
        public int VehicleMakeId { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public virtual VehicleMake VehicleMake { get; set; }
    }
}
