using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IGenericRepository<Customer>
    {
        Task<Customer> GetUserIdByCustomerAsync(int userId);

	}
}
