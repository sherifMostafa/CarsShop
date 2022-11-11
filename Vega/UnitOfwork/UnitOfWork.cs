using System.Threading.Tasks;
using Vega.Persistence;

namespace Vega.UnitOfwork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VegaDbContext _context;
        public UnitOfWork(VegaDbContext context)
        {
            _context = context;
        }
        public async Task CompleteAsync()
        {
             await _context.SaveChangesAsync();
        }
    }
}