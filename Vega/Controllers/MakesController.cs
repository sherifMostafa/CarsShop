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
using Vega.Resources;

namespace Vega.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakesController : ControllerBase
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;

        public MakesController(VegaDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<MakeResource>> getMakes()
        {
            var makes = await _context.Makes.Include(p => p.Models).ToListAsync();
            return _mapper.Map<List<Make>, List<MakeResource>>(makes);
            //return await _context.Makes.ToListAsync();
        }
        //[HttpGet]
        //public async Task<IEnumerable<Model>> getMakes()
        //{
        //    return await _context.Models.Include( p => p.Make).ToListAsync();
        //    //return await _context.Makes.ToListAsync();
        //}
    }
}
