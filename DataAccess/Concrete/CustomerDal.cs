using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class CustomerDal : GenericRepository<Customer>, ICustomerDal
    {
        public CustomerDal(CarRentalContext context) : base(context)
        {
        }
    }
}
