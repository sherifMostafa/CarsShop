using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.Resources
{
    public class MakeResource  : KeyValuePairResource
    {
        public List<KeyValuePairResource> Models { get; set; } 
        public MakeResource()
        {
            Models = new List<KeyValuePairResource>();
        }
    }
}
