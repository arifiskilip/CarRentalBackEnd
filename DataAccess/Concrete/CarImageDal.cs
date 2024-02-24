using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class CarImageDal : GenericRepository<CarImage>, ICarImageDal
    {
        public CarImageDal(CarRentalContext context) : base(context)
        {
        }
    }
}
