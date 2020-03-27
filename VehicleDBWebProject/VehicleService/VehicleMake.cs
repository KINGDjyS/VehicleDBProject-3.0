using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VehicleService
{
    public class VehicleMake : IVehicleMake
    {
        public VehicleMake()
        {

        }

        [Key]
        public int VehicleMakeId { get; set; }
        public string Name { get; set; }

        public string Abrv { get; set; }
        public ICollection<VehicleModel> Models { get; set; }
    }
}
