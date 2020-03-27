using System.Linq;
using System.Threading.Tasks;

namespace VehicleService
{
    public interface IVehicleModelService
    {
        Task<VehicleModel> AddVehicleModelAsync(VehicleModel vehicleModel);
        Task<VehicleModel> DeleteVehicleModelAsync(int id);
        Task<VehicleModel> GetOneVehicleModelAsync(int id);
        IQueryable<VehicleModel> GetVehicleModels(Filtering filtering, Sorting sorting, Paging paging);
        Task<VehicleModel> UpdateAsync(VehicleModel modelChanged);
        int CountModels();

    }
}