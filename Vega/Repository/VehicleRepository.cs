using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Domains;
using Vega.Persistence;

namespace Vega.Repository
{
    public class VehicleRepository : IVehicleRepository
    {

        private readonly  VegaDbContext _context ;
        public VehicleRepository(VegaDbContext context)
        {
            _context= context;
        }

        public async Task<IEnumerable<Vehicle>> getAllVehicle() {
            return await _context.Vehicles
            .Include(p => p.Features).ThenInclude(p=> p.Feature)
            .Include(p => p.Model).ThenInclude( p=> p.Make) 
            .ToListAsync();
        }


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