using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Domains;
using Vega.Extentions;
using Vega.Persistence;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Vega.Repository
{
    public class VehicleRepository : IVehicleRepository
    {

        private readonly  VegaDbContext _context ;
        public VehicleRepository(VegaDbContext context)
        {
            _context= context;
        }

        public async Task<IEnumerable<Vehicle>> getAllVehicle(VehicleQuery queryObj) {
            var query =  _context.Vehicles
            .Include(p => p.Features).ThenInclude(p=> p.Feature)
            .Include(p => p.Model).ThenInclude( p=> p.Make).AsQueryable();
            if (queryObj.MakeId.HasValue)
                query = query.Where(p => p.Model.MakeId == queryObj.MakeId);
            if (queryObj.ModelId.HasValue)
                query = query.Where(p => p.ModelId == queryObj.ModelId);


            //Expression<Func<Vehicle, object>> exp;
            //Func<Vehicle, string> func = v => v.ContactName;

            var columnMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["Make"] = v => v.Model.Make.Name,
                ["Model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName
                //["id"] = v => v.Id
            };

           //query = ApplyOrdering(queryObj,query,columnMap);
           query = query.ApplyOrdering(queryObj,columnMap);
           query = query.ApplyPaging(queryObj);







            //if (queryObj.IsSortAscending)
            //    query = query.OrderBy(columnMap[queryObj.SortBy]);
            //else
            //    query = query.OrderByDescending(columnMap[queryObj.SortBy]);



            //---------------------  this is Duplicated -------------------------------
            //if (queryObj.SortBy == "make")
            //    query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.Model.Make.Name) : query.OrderByDescending(v => v.Model.Make.Name);

            //if (queryObj.SortBy == "model")
            //    query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.Model.Name) : query.OrderByDescending(v => v.Model.Name);

            //if (queryObj.SortBy == "contactName")
            //    query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.ContactName) : query.OrderByDescending(v => v.ContactName);

            //if (queryObj.SortBy == "id")
            //    query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.Id) : query.OrderByDescending(v => v.Id);

            return await query.ToListAsync();
        }

        //private IQueryable<Vehicle> ApplyOrdering(VehicleQuery queryObj,IQueryable<Vehicle> query, Dictionary<string, Expression<Func<Vehicle, object>>> columnMap) {
        //    if (queryObj.IsSortAscending)
        //        return query.OrderBy(columnMap[queryObj.SortBy]);
        //    else
        //        return query.OrderByDescending(columnMap[queryObj.SortBy]);
        //}

        public async Task<Vehicle> getVehicel(int id , bool includeRelated = true) {
            if(!includeRelated) 
               return await  _context.Vehicles.FindAsync(id);

            return await _context.Vehicles
            .Include(p => p.Features).ThenInclude(p=> p.Feature)
            .Include(p => p.Model).ThenInclude( p=> p.Make) 
            .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void AddVehicle(Vehicle vehicle) {
             _context.Vehicles.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle) {
            _context.Remove(vehicle);
        }
    }

    
}