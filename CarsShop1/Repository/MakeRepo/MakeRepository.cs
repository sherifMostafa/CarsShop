using CarsShop1.Models;
using CarsShop1.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsShop1.Repository.MakeRepo
{
    public class MakeRepository : Repository<Make>, IMakeRepository 
    {
        public MakeRepository(ApplicationDbContext context) : base(context)
        {
                
        }

    }
}
