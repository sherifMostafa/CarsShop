using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.Domains
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<VehicleFeature> Vehicles { get; set; }


        public Feature()
        {
            Vehicles = new Collection<VehicleFeature>();
        }
    }
}
