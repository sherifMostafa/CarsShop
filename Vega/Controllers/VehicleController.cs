using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vega.Domains;
using Vega.Persistence;
using Vega.Repository;
using Vega.Resources;
using Vega.UnitOfwork;

namespace Vega.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {

        public readonly IMapper _mapper;
        private readonly IVehicleRepository _repo;
        private readonly IUnitOfWork _unitofwork;
        public VehicleController(
            IMapper mapper,
              IVehicleRepository repo ,
               IUnitOfWork unitofwork
               )
        {
            _mapper = mapper;
            _repo = repo;
            _unitofwork = unitofwork;
        }

        [HttpGet]
        public async Task<IActionResult> getVehicles([FromQuery] VehicleQueryResource filterResource)
        {
            var filter = _mapper.Map<VehicleQueryResource, VehicleQuery>(filterResource);
            var vehicles = await _repo.getAllVehicle(filter);
            var v = _mapper.Map<VehicleResource[]>(vehicles);
            return Ok(v);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getVehicle(int id)
        {
            var vehicle = await _repo.getVehicel(id);
            if(vehicle == null) 
                return NotFound();
                
            var v = _mapper.Map<VehicleResource>(vehicle);
            return Ok(v);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle(SaveVehicleResource vehicle)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var v = _mapper.Map<SaveVehicleResource, Vehicle>(vehicle);
            v.LastUpdate = DateTime.Now;

            _repo.AddVehicle(v);
            await _unitofwork.CompleteAsync();

             v = await _repo.getVehicel(v.Id);

            return Ok(_mapper.Map<Vehicle , VehicleResource>(v));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id,[FromBody]SaveVehicleResource vehicleR)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

             var vehicle = await _repo.getVehicel(id);

            if (vehicle == null)
                return NotFound();

            _mapper.Map<SaveVehicleResource, Vehicle>(vehicleR, vehicle);
            vehicle.LastUpdate = DateTime.Now;
         
             await _unitofwork.CompleteAsync();

            return Ok(_mapper.Map<Vehicle , VehicleResource>(vehicle));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            Vehicle vehicle = await _repo.getVehicel(id ,includeRelated: false);

            if (vehicle == null)
                return NotFound();

            _repo.RemoveVehicle(vehicle);
              await _unitofwork.CompleteAsync();

            return Ok();
        }

    }
}
