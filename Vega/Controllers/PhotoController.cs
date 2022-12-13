using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using Vega.Domains;
using Vega.Repository;
using Vega.Resources;
using Vega.UnitOfwork;

namespace Vega.Controllers
{
    [Route("api/vehicles/{vehicleId}/photos")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IHostEnvironment _host;
        private readonly IVehicleRepository _vehicelRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhotoController(IHostEnvironment host, IVehicleRepository vehicelRepo , IUnitOfWork unitOfWork, IMapper mapper)
        {
            _host = host;
            _vehicelRepo = vehicelRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicle = await _vehicelRepo.getVehicel(vehicleId, includeRelated: false);
            
            if(vehicle == null)
                return NotFound();


            var uploadsFolderPath =  Path.Combine(_host.ContentRootPath, "uploads");
            if(!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream  = new FileStream(filePath,FileMode.Create))
            {
                await file.CopyToAsync(stream);

            }

            var photo = new Photo { FileName = fileName};

            vehicle.Photos.Add(photo);

            await _unitOfWork.CompleteAsync();

            return Ok(_mapper.Map<PhotoResource>(photo));

        }
    }
}
