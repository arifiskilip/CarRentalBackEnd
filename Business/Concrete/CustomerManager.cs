using Business.Abstract;
using Business.Constants.ResultMessages;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class CustomerManager : ICustomerService
	{
		private readonly ICustomerDal _customerDal;

		public CustomerManager(ICustomerDal customerDal)
		{
			_customerDal = customerDal;
		}

		public async Task<IDataResult<Customer>> GetUserIdByCustomerAsync(int userId)
		{
			var result = await _customerDal.GetUserIdByCustomerAsync(userId);
			if (result != null)
			{
				return new SuccessDataResult<Customer>(result, Messages.General.SuccessfulListing);
			}
			return new ErrorDataResult<Customer>(result, Messages.General.FailedListing);
		}
	}
}
