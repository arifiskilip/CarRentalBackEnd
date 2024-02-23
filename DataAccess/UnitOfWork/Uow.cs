using DataAccess.Contexts;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly CarRentalContext _context;
        public Uow(CarRentalContext context)
        {
            _context = context;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
