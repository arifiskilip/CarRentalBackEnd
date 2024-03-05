using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBrandService
    {
        Task<IDataResult<List<Brand>>> GetAllAsync();
        Task<IDataResult<Brand>> GetByIdAsync(int  id);
        Task<IResult> UpdateAsync(Brand brand);
        Task<IDataResult<Brand>> AddAsync(Brand brand);
        Task<IResult> DeleteAsync(Brand brand);
        Task<IResult> DeleteAsync(int id);
    }
}
