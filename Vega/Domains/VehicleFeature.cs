using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.Domains
{
    public class VehicleFeature
    {
        public int VehicleId { get; set; }
        public int FeatureId { get; set; }

        // [JsonIgnore]
        public Vehicle Vehicle { get; set; }
        // [JsonIgnore]
        public Feature Feature { get; set; }

    }
}
