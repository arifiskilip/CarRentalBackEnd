using Core.Utilities.Pagination;
using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IColorService
    {
        Task<IDataResult<PaginatedList<Color>>> GetAllByPaginationAsync(int pageIndex, int pageSize);
        Task<IDataResult<Color>> AddAsync(Color color);
        Task<IDataResult<List<Color>>> GetAllAsync();
		Task<IDataResult<Color>> GetByIdAsync(int id);
		Task<IResult> UpdateAsync(Color color);
		Task<IResult> DeleteAsync(Color color);
		Task<IResult> DeleteAsync(int id);
	}
}
