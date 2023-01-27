using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Domains;

namespace Vega.Repository
{
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetPhotos(int vehicleId);
    }
}
