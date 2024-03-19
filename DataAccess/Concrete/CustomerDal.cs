using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class CustomerDal : GenericRepository<Customer>, ICustomerDal
    {
        public CustomerDal(CarRentalContext context) : base(context)
        {
        }

		public async Task<Customer> GetUserIdByCustomerAsync(int userId)
		{
			Customer customer= await this._dbSet.FirstOrDefaultAsync(x => x.UserId == userId);
			return customer;
		}
	}
}
