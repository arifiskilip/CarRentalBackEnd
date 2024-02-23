using Core.Utilities.Pagination;
using Core.Utilities.Results;
using Entities.Concrete;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IColorService
    {
        Task<IDataResult<PaginatedList<Color>>> GetAllByPagination(int pageIndex, int pageSize);
    }
}
