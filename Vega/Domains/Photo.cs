using Newtonsoft.Json;

namespace Vega.Domains
{
    public class Photo
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public int VehicleId { get; set; }
        [JsonIgnore]
        public Vehicle Vehicle { get; set; }
    }
}
