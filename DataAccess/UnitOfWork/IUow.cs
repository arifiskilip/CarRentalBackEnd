using System;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IUow : IAsyncDisposable
    {
        Task<int> SaveAsync();
    }
}
