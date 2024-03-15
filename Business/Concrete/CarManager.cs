using Business.Abstract;
using Business.Constants.ResultMessages;
using Business.ValidationRules.FluentValidaiton;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IUow _uow;

        public CarManager(ICarDal carDal, IUow uow)
        {
            _carDal = carDal;
            _uow = uow;
        }

        [ValidationAspect(typeof(CarValidator))]
        public async Task<IDataResult<Car>> AddCarAsync(Car car)
        {
            var addedCar = await _carDal.AddAsync(car);
            await _uow.SaveAsync();
            return new SuccessDataResult<Car>(addedCar, Messages.General.SuccessAdded);
        }

        public async Task<IDataResult<List<Car>>> GetAllByBrandIdAndColorIdAsync(int brandId, int colorId)
        {
            var result = await _carDal.GetAllAsyncV2(new() { x=>x.BrandId==brandId, x=>x.ColorId == colorId}, new() { x=>x.Brand, x => x.Color, x => x.CarImages });

            return new SuccessDataResult<List<Car>>(result, Messages.General.SuccessfulListing);
        }

        public async Task<IDataResult<List<Car>>> GetAllWithColorAndBrandAsync()
        {
            var result = await _carDal.GetAllAsync(null, x => x.Brand, x => x.Color, x=>x.CarImages);

            return new SuccessDataResult<List<Car>>(result, Messages.General.SuccessfulListing);
        }

		public async Task<IDataResult<Car>> GetCarByIdAsync(int carId)
		{
            var checkCar = await _carDal.GetAsync(new()
            {
                x=> x.Id == carId
            }, new() { x=> x.Brand,x=> x.Color,x=> x.CarImages});
            if (checkCar != null)
            {
                return new SuccessDataResult<Car>(checkCar, Messages.General.SuccessfulListing);
            }
            return new ErrorDataResult<Car>(carId + Messages.Car.CarNotFound);
		}

		public async Task<IDataResult<List<Car>>> GetCarsByBrandIdAsync(int brandId)
        {
            var result = await _carDal.GetAllAsync(x => x.BrandId == brandId, x => x.Brand, x => x.Color,x=> x.CarImages);
            return new SuccessDataResult<List<Car>>(result, Messages.General.SuccessfulListing);
        }

        public async Task<IDataResult<List<Car>>> GetCarsByColorIdAsync(int colorId)    
        {
            var result = await _carDal.GetAllAsync(x => x.ColorId == colorId, x => x.Brand, x => x.Color, x => x.CarImages);
            return new SuccessDataResult<List<Car>>(result, Messages.General.SuccessfulListing);
        }
		public async Task<IResult> UpdateAsync(Car car)
		{
			await _carDal.UpdateAsync(car);
			await _uow.SaveAsync();
			return new SuccessResult(Messages.General.SuccessUpdate);
		}

		public async Task<IResult> DeleteAsync(int id)
		{
			var checkCar = await _carDal.GetAsync(new()
			{
				x=> x.Id == id
			});
			if (checkCar != null)
			{
				await _carDal.DeleteAsync(checkCar);
				await _uow.SaveAsync();
				return new SuccessResult(Messages.General.SuccessDelete);
			}
			return new ErrorResult(Messages.General.ErrorDelete);
		}
	}
}
