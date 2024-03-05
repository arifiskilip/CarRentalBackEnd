using Business.Abstract;
using Business.Constants.ResultMessages;
using Business.ValidationRules.FluentValidaiton;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;
        private readonly IUow _uow;

		public RentalManager(IRentalDal rentalDal, IUow uow, ICreditCardService creditCardService)
		{
			_rentalDal = rentalDal;
			_uow = uow;
		}

		[ValidationAspect(typeof(RentalValidator))]
        public async Task<IDataResult<Rental>> AddAsync(Rental rental)
        {
            var rentalCar = await _rentalDal.AddAsync(rental);
            await _uow.SaveAsync();
            return new SuccessDataResult<Rental>(rentalCar, Messages.Rental.SuccessRental);
        }


        public async Task<IResult> CheckRentalCarAsync(int carId)
        {
            var checkCar = await _rentalDal.GetAsync(new() { x => x.CarId == carId });
            if (checkCar != null && !checkCar.IsDelivered)
            {
                return new ErrorResult(Messages.Rental.CarBeingUsed);
            }
            return new SuccessResult();
        } 

    }
}
