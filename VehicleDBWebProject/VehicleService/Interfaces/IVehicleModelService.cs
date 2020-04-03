using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleService
{
    public interface IVehicleModelService
    {
        Task<VehicleModel> AddVehicleModelAsync(VehicleModel vehicleModel);
        Task<VehicleModel> DeleteVehicleModelAsync(int id);
        Task<VehicleModel> GetOneVehicleModelAsync(int id);
        IList<VehicleModel> GetVehicleModels(Filtering filtering, Sorting sorting, Paging paging);
        Task<VehicleModel> UpdateAsync(VehicleModel modelChanged);
        int CountModels();

    }
}