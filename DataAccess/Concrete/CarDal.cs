using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class CarDal : GenericRepository<Car>, ICarDal
    {
        public CarDal(CarRentalContext context) : base(context)
        {
        }
    }
}
