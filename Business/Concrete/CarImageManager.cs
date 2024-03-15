using Business.Abstract;
using Business.Constants.ResultMessages;
using Business.ValidationRules.FluentValidaiton;
using Core.Aspects.Autofac.SecuredOperation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        private readonly IUow _uow;

        public CarImageManager(ICarImageDal carImageDal, IUow uow)
        {
            _carImageDal = carImageDal;
            _uow = uow;
        }
        public async Task<IDataResult<CarImage>> AddAsync(int carId, IFormFile file)
        {
            var result = BusinessRules<CarImage>.RunDataResult(CarImageMustBeMaxFive(carId));
            if (result !=null)
            {
                return result;
            }
            var imageResult = FileHelper.Add(file);
            if (imageResult.Success)
            {
                var addedCarImage = await _carImageDal.AddAsync(new()
                {
                    ImagePath = imageResult.Message,
                    CarId = carId
                });
                await _uow.SaveAsync();
                return new SuccessDataResult<CarImage>(addedCarImage,Messages.General.SuccessAdded);
            }
            FileHelper.Delete(imageResult.Message);
            return new ErrorDataResult<CarImage>(imageResult.Message);
          
        }
        public async Task<IDataResult<List<CarImage>>> GetAllAsync()
        {
            var carImages = await _carImageDal.GetAllAsync();
            return new SuccessDataResult<List<CarImage>>(carImages,Messages.General.SuccessfulListing);
        }

        public async Task<IDataResult<CarImage>> GetAsync(int id)
        {

            var carImage = await _carImageDal.GetAsync(new() { x=> x.Id == id }, new() { x=> x.Car});
            if (carImage == null)
            {
                return new ErrorDataResult<CarImage>(Messages.CarImage.CarImageNotFound);
            }
            return new SuccessDataResult<CarImage>(carImage, Messages.General.SuccessfulListing);
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public async Task<IDataResult<CarImage>> UpdateAsync(CarImage carImage, IFormFile file)
        {
            var getCarImage = await _carImageDal.GetAsync(new() { x=> x.Id==carImage.Id});
            var fileResult = FileHelper.Add(file);
            if (fileResult.Success)
            {
                FileHelper.Delete(carImage.ImagePath);
                getCarImage.ImagePath = fileResult.Message;
                getCarImage.CarId = carImage.CarId;
                await _carImageDal.UpdateAsync(getCarImage);
                await _uow.SaveAsync();
                return new SuccessDataResult<CarImage>(getCarImage, Messages.General.SuccessUpdate);
            }
            return new ErrorDataResult<CarImage>(fileResult.Message);
        }

		public async Task<IDataResult<List<CarImage>>> GetCarIdByCarImagesAsync(int carId)
		{
            var result = await _carImageDal.GetAllAsync(x => x.CarId == carId, x => x.Car);
            return new SuccessDataResult<List<CarImage>>(result, Messages.General.SuccessfulListing);
		}

		public async Task<IResult> DeleteAsync(int carImageId)
		{
            var checkCarImage = await _carImageDal.GetAsync(new()
            {
                x=> x.Id==carImageId,
            });
            if (checkCarImage != null)
            {
                await _carImageDal.DeleteAsync(checkCarImage);
                await _uow.SaveAsync();
                FileHelper.Delete(checkCarImage.ImagePath);
                return new SuccessResult(Messages.General.SuccessDelete);
            }
            return new ErrorResult(Messages.General.ErrorDelete);

		}

		//Ruless

		public IDataResult<CarImage> CarImageMustBeMaxFive(int carId)
        {
            var result = _carImageDal.CountAsync(x => x.CarId == carId).Result;
            if (result < 5) return new SuccessDataResult<CarImage>();
            return new ErrorDataResult<CarImage>(Messages.CarImage.MustHaveFiveCarPictrues);
        }

		
	}
}
