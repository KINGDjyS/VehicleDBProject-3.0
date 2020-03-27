using System.ComponentModel.DataAnnotations;

namespace VehicleService
{
    public class VehicleModel : IVehicleModel
    {
        public VehicleModel()
        {

        }

        [Key]
        public int VehicleModelId { get; set; }

        public int VehicleMakeId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }

        public virtual VehicleMake VehicleMake { get; set; }
    }
}
