using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vega.Resources
{
    public class SaveVehicleResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }

        public ContactResource Contact { get; set; }
      

        public ICollection<int> Features { get; set; }


        public SaveVehicleResource()
        {
            Features = new Collection<int>();
        }
    }

    public class ContactResource
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
