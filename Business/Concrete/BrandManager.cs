using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        public async Task<IDataResult<List<Brand>>> GetAllAsync()
        {
            var result = await _brandDal.GetAllAsync();
            return new SuccessDataResult<List<Brand>>(result, "Listeleme işlemi başarılı!");
        }
    }
}
