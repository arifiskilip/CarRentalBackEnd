using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class UserDal : GenericRepository<User>, IUserDal
    {
        public UserDal(CarRentalContext context) : base(context)
        {
        }
    }
}
