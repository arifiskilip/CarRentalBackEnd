using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;

namespace DataAccess.Concrete
{
	public class CreditCardDal : GenericRepository<CreditCard>, ICreditCardDal
	{
		public CreditCardDal(CarRentalContext context) : base(context)
		{
		}
	}
}
