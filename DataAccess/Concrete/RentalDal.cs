using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class RentalDal : GenericRepository<Rental>, IRentalDal
    {
        public RentalDal(CarRentalContext context) : base(context)
        {
        }
    }
}
