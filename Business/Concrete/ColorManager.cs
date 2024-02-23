using Business.Abstract;
using Core.Utilities.Pagination;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        private readonly IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        public async Task<IDataResult<PaginatedList<Color>>> GetAllByPagination(int pageIndex, int pageSize)
        {
            var colors = await _colorDal.GetAllAsync();
            var result = PaginatedList<Color>.Create(colors, pageIndex, pageSize);
            return new SuccessDataResult<PaginatedList<Color>>(result,"Listeleme işlemi başarılı!");
        }
    }
}
