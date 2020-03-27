﻿using System.Linq;
using System.Threading.Tasks;

namespace VehicleService
{
    public interface IVehicleMakeService
    {
        Task<VehicleMake> AddVehicleMakerAsync(VehicleMake vehicleMake);
        Task<VehicleMake> DeleteVehicleMakerAsync(int id);
        Task<VehicleMake> GetOneVehicleMakerAsync(int id);
        IQueryable<VehicleMake> GetVehicleMakes(Filtering filtering, Sorting sorting, Paging paging);
        Task<VehicleMake> UpdateAsync(VehicleMake makeChanged);
        int CountMakers();
    }
}