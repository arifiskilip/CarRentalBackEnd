using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class ColorDal : GenericRepository<Color>, IColorDal
    {
        public ColorDal(CarRentalContext context) : base(context)
        {
        }
    }
}
