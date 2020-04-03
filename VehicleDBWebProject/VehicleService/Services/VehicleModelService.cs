using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleService
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly VehicleDBContext _VehicleDB;
        public VehicleModelService(VehicleDBContext vehicleDb)
        {
            _VehicleDB = vehicleDb;
        }

        public async Task<VehicleModel> AddVehicleModelAsync(VehicleModel vehicleModel)
        {
            using (var db = _VehicleDB)
            {
                db.VehicleModels.Add(vehicleModel);
                await db.SaveChangesAsync();
                return vehicleModel;
            }

        }

        public IList<VehicleModel> GetVehicleModels(Filtering filtering, Sorting sorting, Paging paging)
        {
            if (!String.IsNullOrEmpty(filtering.SearchName) && HasNameInDatabase(filtering.SearchName))
            {
                return _VehicleDB.VehicleModels.Where(f => f.Name.Contains(filtering.SearchName)).OrderBy(o => o.Name).Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToList();
            }
            else if (String.IsNullOrEmpty(filtering.SearchName))
            {
                return SortVehicleModels(sorting).Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToList();
            }
            else
            {
                return null;
            }
        }
        private IQueryable<VehicleModel> SortVehicleModels(Sorting sorting)
        {
            switch (sorting.SortBy)
            {
                case "VehicleMakeId":
                    if (sorting.OrderBy == "DESC")
                    {
                        return _VehicleDB.VehicleModels.OrderByDescending(o => o.VehicleMakeId);
                    }
                    else
                    {
                        return _VehicleDB.VehicleModels.OrderBy(o => o.VehicleMakeId);
                    }
                case "Abrv":
                    if (sorting.OrderBy == "DESC")
                    {
                        return _VehicleDB.VehicleModels.OrderByDescending(o => o.Abrv);
                    }
                    else
                    {
                        return _VehicleDB.VehicleModels.OrderBy(o => o.Abrv);
                    }
                default:
                    if (sorting.OrderBy == "DESC")
                    {
                        return _VehicleDB.VehicleModels.OrderByDescending(o => o.Name);
                    }
                    else
                    {
                        return _VehicleDB.VehicleModels.OrderBy(o => o.Name);
                    }

            }
        }

        public async Task<VehicleModel> DeleteVehicleModelAsync(int id)
        {
            using (var db = _VehicleDB)
            {
                VehicleModel model = db.VehicleModels.Find(id);
                db.VehicleModels.Remove(model);
                await db.SaveChangesAsync();
                return model;
            }
        }

        public async Task<VehicleModel> GetOneVehicleModelAsync(int id)
        {
            return await _VehicleDB.VehicleModels.Where(i => i.VehicleModelId == id).FirstOrDefaultAsync();
        }

        public async Task<VehicleModel> UpdateAsync(VehicleModel makeChanged)
        {
            using (var db = _VehicleDB)
            {
                var model = db.VehicleModels.Attach(makeChanged);
                model.State = EntityState.Modified;
                await db.SaveChangesAsync();
                return makeChanged;
            }
        }


        private bool HasNameInDatabase(string name)
        {
            return _VehicleDB.VehicleModels.Any(n => n.Name == name);
        }

        public int CountModels()
        {
            return _VehicleDB.VehicleModels.Count<VehicleModel>();
        }

    }
}
