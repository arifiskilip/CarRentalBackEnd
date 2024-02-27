using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {
        Task<IDataResult<List<Car>>> GetCarsByBrandIdAsync(int brandId);
        Task<IDataResult<List<Car>>> GetCarsByColorIdAsync(int colorId);
        Task<IDataResult<Car>> AddCarAsync(Car car);
        Task<IDataResult<List<Car>>> GetAllWithColorAndBrandAsync();
    }
}
