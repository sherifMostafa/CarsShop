using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Domains;
using Vega.Persistence;

namespace Vega.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly VegaDbContext _context;
        public PhotoRepository(VegaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await _context.Photos.Where(p => p.VehicleId == vehicleId).ToListAsync(); 
        }
    }
}
