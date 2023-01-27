using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vega.Domains;
using Vega.Models;
using Vega.Repository;
using Vega.Resources;
using Vega.UnitOfwork;

namespace Vega.Controllers
{
    [Route("api/vehicles/{vehicleId}/photos")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        //private readonly int MAX_BYTES = 1 * 1024 * 1024;
        //private string[] ACCEPTED_FILE_TYPES = new[] {".jpg", ".jpeg",".png" };


        private readonly PhotoSettings _photoSettings;
        private readonly IHostEnvironment _host;
        private readonly IVehicleRepository _vehicelRepo;
        private readonly IPhotoRepository _photoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhotoController(IHostEnvironment host, IVehicleRepository vehicelRepo , IUnitOfWork unitOfWork, IMapper mapper, IPhotoRepository photoRepository, IOptions<PhotoSettings> options)
        {
            _photoSettings = options.Value;
            _photoRepository = photoRepository;
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

            if (file == null || file.Length == 0) return BadRequest("Null or Empty file");
            if (file.Length > _photoSettings.MaxBytes) return BadRequest("Max File Size exceeded");
            if (!_photoSettings.IsSupported(file.FileName))
                return BadRequest("Invalid File Type");
            
            var uploadsFolderPath =  Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads");
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoResource>>> GetPhotos(int vehicleId)
        {
            var result = await _photoRepository.GetPhotos(vehicleId);
            return Ok(_mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(result));
        }

    }
}
