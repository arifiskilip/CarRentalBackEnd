using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class CreditCartManager : ICreditCardService
	{
		private readonly ICreditCardDal _creditCardDal;

		public CreditCartManager(ICreditCardDal creditCardDal)
		{
			_creditCardDal = creditCardDal;
		}

		public async Task<IResult> CheckCreditCardAsync(CreditCard creditCard)
		{
			var asd = creditCard.CardOwner.Trim().ToUpper().Replace(" ", "");
			var resut = await _creditCardDal.GetAsync(new()
			{
				x=> x.CardNumber == creditCard.CardNumber,
				x=> x.CVV == creditCard.CVV,
				x=> x.CardOwner == creditCard.CardOwner.ToLower().Replace(" ",""),	
				x=> x.ExpirationDate == creditCard.ExpirationDate
			});
			if (resut != null)
			{
				return new SuccessResult("Geçerli kredi kartı.");
			}
			return new ErrorResult("Geçersiz kredi kartı.");
		}
	}
}
