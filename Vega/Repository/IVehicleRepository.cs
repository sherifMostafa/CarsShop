using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Domains;

namespace Vega.Repository
{
    public interface IVehicleRepository
    {
         Task<IEnumerable<Vehicle>> getAllVehicle(VehicleQuery obj);
         Task<Vehicle> getVehicel(int id , bool includeRelated = true);
         void AddVehicle(Vehicle vehicle);
         void RemoveVehicle(Vehicle vehicle);
    }
}