using System.Threading.Tasks;
namespace Vega.UnitOfwork
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}