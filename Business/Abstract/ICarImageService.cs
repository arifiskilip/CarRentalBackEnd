using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        Task<IDataResult<List<CarImage>>> GetAllAsync();
        Task<IDataResult<List<CarImage>>> GetCarIdByCarImagesAsync(int carId);
        Task<IDataResult<CarImage>> GetAsync(int id);
        Task<IDataResult<CarImage>> AddAsync(int carId, IFormFile file);
        Task<IDataResult<CarImage>> UpdateAsync(CarImage carImage, IFormFile file);
        Task<IResult> DeleteAsync(int carImageId); 
	}
}
