using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleService.Common;
using VehicleService.Models;

namespace VehicleService.Services
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehicleDBContext _VehicleDB;
        public VehicleMakeService(VehicleDBContext vehicleDB)
        {
            _VehicleDB = vehicleDB;
        }

        public async Task<VehicleMake> AddVehicleMakerAsync(VehicleMake vehicleMake)
        {
            using (var db = _VehicleDB)
            {
                db.VehicleMakes.Add(vehicleMake);
                await db.SaveChangesAsync();
                return vehicleMake;
            }

        }

        public IList<VehicleMake> GetVehicleMakes(Filtering filtering, Sorting sorting, Paging paging)
        {
            if (!String.IsNullOrEmpty(filtering.SearchName) && HasNameInDatabase(filtering.SearchName))
            {
                return _VehicleDB.VehicleMakes.Where(f => f.Name.Contains(filtering.SearchName)).OrderBy(o => o.Name).Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToList();
            }
            else if (String.IsNullOrEmpty(filtering.SearchName))
            {
                return SortVehicleMakers(sorting).Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).ToList();
            }
            else
            {
                return null;
            }
        }

        private IQueryable<VehicleMake> SortVehicleMakers(Sorting sorting)
        {
            switch (sorting.SortBy)
            {
                case "VehicleMakeId":
                    if (sorting.OrderBy == "DESC")
                    {
                        return _VehicleDB.VehicleMakes.OrderByDescending(o => o.VehicleMakeId);
                    }
                    else
                    {
                        return _VehicleDB.VehicleMakes.OrderBy(o => o.VehicleMakeId);
                    }
                case "Abrv":
                    if (sorting.OrderBy == "DESC")
                    {
                        return _VehicleDB.VehicleMakes.OrderByDescending(o => o.Abrv);
                    }
                    else
                    {
                        return _VehicleDB.VehicleMakes.OrderBy(o => o.Abrv);
                    }
                default:
                    if (sorting.OrderBy == "DESC")
                    {
                        return _VehicleDB.VehicleMakes.OrderByDescending(o => o.Name);
                    }
                    else
                    {
                        return _VehicleDB.VehicleMakes.OrderBy(o => o.Name);
                    }

            }
        }

        public async Task<VehicleMake> DeleteVehicleMakerAsync(int id)
        {
            using (var db = _VehicleDB)
            {
                VehicleMake make = db.VehicleMakes.Find(id);
                db.VehicleMakes.Remove(make);
                await db.SaveChangesAsync();
                return make;
            }
        }

        public async Task<VehicleMake> GetOneVehicleMakerAsync(int id)
        {
            return await _VehicleDB.VehicleMakes.Where(i => i.VehicleMakeId == id).FirstOrDefaultAsync();
        }

        public async Task<VehicleMake> UpdateAsync(VehicleMake makeChanged)
        {
            using (var db = _VehicleDB)
            {
                var make = db.VehicleMakes.Attach(makeChanged);
                make.State = EntityState.Modified;
                await db.SaveChangesAsync();
                return makeChanged;
            }
        }

        private bool HasNameInDatabase(string name)
        {
            return _VehicleDB.VehicleMakes.Any(n => n.Name == name);
        }

        public int CountMakers()
        {
            
            return _VehicleDB.VehicleMakes.Count<VehicleMake>();
        }

    }
}
