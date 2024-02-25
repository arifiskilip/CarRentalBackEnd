using Business.Abstract;
using Business.Aspects.Autofac;
using Business.ValidationRules.FluentValidaiton;
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

        [ValidationAspect(typeof(CarImageValidator))]
        public async Task<IDataResult<CarImage>> AddAsync(CarImage carImage, IFormFile file)
        {
            var result = BusinessRules<CarImage>.RunDataResult(CarImageMustBeMaxFive(carImage.CarId));
            if (result !=null)
            {
                return result;
            }
            var imageResult = FileHelper.Add(file);
            if (imageResult.Succes)
            {
                carImage.ImagePath = imageResult.Message;
                var addedCarImage = await _carImageDal.AddAsync(carImage);
                await _uow.SaveAsync();
                return new SuccessDataResult<CarImage>(addedCarImage, "Ekleme işlemi başarılı!");
            }
            return new ErrorDataResult<CarImage>(imageResult.Message);
          
        }
        [SecuredOperation("admin,member")]
        public async Task<IDataResult<List<CarImage>>> GetAllAsync()
        {
            var carImages = await _carImageDal.GetAllAsync();
            return new SuccessDataResult<List<CarImage>>(carImages,"Listeleme işlemi başarılı.");
        }

        public async Task<IDataResult<CarImage>> GetAsync(int id)
        {

            var carImage = await _carImageDal.GetAsync(new() { x=> x.Id == id});
            if (carImage == null)
            {
                return new ErrorDataResult<CarImage>("İlgili resim bulunamadı!");
            }
            return new SuccessDataResult<CarImage>(carImage, "Başarılı işlem!");
        }

        [ValidationAspect(typeof(CarImageValidator))]
        public async Task<IDataResult<CarImage>> UpdateAsync(CarImage carImage, IFormFile file)
        {
            var getCarImage = await _carImageDal.GetAsync(new() { x=> x.Id==carImage.Id});
            var fileResult = FileHelper.Add(file);
            if (fileResult.Succes)
            {
                FileHelper.Delete(carImage.ImagePath);
                getCarImage.ImagePath = fileResult.Message;
                getCarImage.CarId = carImage.CarId;
                await _carImageDal.UpdateAsync(getCarImage);
                await _uow.SaveAsync();
                return new SuccessDataResult<CarImage>(getCarImage, "Güncelleme işlemi başarılı!");
            }
            return new ErrorDataResult<CarImage>(fileResult.Message);
        }


        //Ruless

        public IDataResult<CarImage> CarImageMustBeMaxFive(int carId)
        {
            var result = _carImageDal.CountAsync(x => x.CarId == carId).Result;
            if (result < 5) return new SuccessDataResult<CarImage>();
            return new ErrorDataResult<CarImage>("İlgili araç resmi en fazla 5 adet olmalı");
        }

        [SecuredOperation("admin")]
        public string Deneme()
        {
            return "Çalıştı";
        }
    }
}
