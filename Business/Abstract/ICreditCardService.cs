using Core.Utilities.Results;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Abstract
{
	public interface ICreditCardService
	{
		Task<IResult> CheckCreditCardAsync(CreditCard creditCard);
	}
}
