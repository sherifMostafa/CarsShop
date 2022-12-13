using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.Domains
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }
        public Model Model { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }
        public DateTime LastUpdate { get; set; }

        public ICollection<VehicleFeature> Features { get; set; }
        public ICollection<Photo> Photos { get; set; }


        public Vehicle()
        {
            Features = new Collection<VehicleFeature>();
            Photos = new Collection<Photo>();
        }
    }

}
