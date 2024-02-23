using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class BrandDal : GenericRepository<Brand>, IBrandDal
    {
        public BrandDal(CarRentalContext context) : base(context)
        {
        }
    }
}
