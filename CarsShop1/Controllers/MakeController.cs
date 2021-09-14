using CarsShop1.Models;
using CarsShop1.Repository.MakeRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsShop1.Controllers
{
    [Route("api/[controller]")]
    public class MakeController : ControllerBase
    {
        private readonly IMakeRepository _repo;
        public MakeController(IMakeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Make>> Getall()
        {
            try
            {
                return Ok( _repo.GetAll());
                
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failier");
            }
        }
    }
}
