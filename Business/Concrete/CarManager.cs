using Business.Abstract;
using Business.ValidationRules.FluentValidaiton;
using Core.Aspects.Autofac.Validation;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            return new SuccessDataResult<Car>(addedCar, "Ekleme işlemi başarılı!");
        }

        public async Task<IDataResult<List<Car>>> GetAllByBrandIdAndColorIdAsync(int brandId, int colorId)
        {
            var result = await _carDal.GetAllAsyncV2(new() { x=>x.BrandId==brandId, x=>x.ColorId == colorId}, new() { x=>x.Brand, x => x.Color, x => x.CarImages });

            return new SuccessDataResult<List<Car>>(result, "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<List<Car>>> GetAllWithColorAndBrandAsync()
        {
            var result = await _carDal.GetAllAsync(null, x => x.Brand, x => x.Color, x=>x.CarImages);

            return new SuccessDataResult<List<Car>>(result, "Listeleme işlemi başarılı!");
        }

		public async Task<IDataResult<Car>> GetCarByIdAsync(int carId)
		{
            var checkCar = await _carDal.GetAsync(new()
            {
                x=> x.Id == carId
            }, new() { x=> x.Brand,x=> x.Color,x=> x.CarImages});
            if (checkCar != null)
            {
                return new SuccessDataResult<Car>(checkCar, "İlgili araç getirildi.");
            }
            return new ErrorDataResult<Car>(carId + " sahip araç yoktur.");
		}

		public async Task<IDataResult<List<Car>>> GetCarsByBrandIdAsync(int brandId)
        {
            var result = await _carDal.GetAllAsync(x => x.BrandId == brandId, x => x.Brand, x => x.Color,x=> x.CarImages);
            return new SuccessDataResult<List<Car>>(result, "Listeleme işlemi başarılı!");
        }

        public async Task<IDataResult<List<Car>>> GetCarsByColorIdAsync(int colorId)    
        {
            var result = await _carDal.GetAllAsync(x => x.ColorId == colorId, x => x.Brand, x => x.Color, x => x.CarImages);
            return new SuccessDataResult<List<Car>>(result, "Listeleme işlemi başarılı!");
        }
    }
}
