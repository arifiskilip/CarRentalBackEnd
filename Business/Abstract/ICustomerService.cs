using Core.Utilities.Results;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        Task<IDataResult<Customer>> GetUserIdByCustomerAsync(int userId);
    }
}
