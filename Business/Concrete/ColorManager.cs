using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Pagination;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
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

        [CacheRemoveAspect("IColorService.Get")]
        public async Task<IDataResult<Color>> AddAsync(Color color)
        {
            var addedColor = await _colorDal.AddAsync(color);
            return new SuccessDataResult<Color>(addedColor,"Ekeleme işlemi başarılı!");
        }
        [CacheAspect]
        public async Task<IDataResult<List<Color>>> GetAllAsync()
        {
            var result = await _colorDal.GetAllAsync();
            return new SuccessDataResult<List<Color>>(result, "Listeleme işlemi başarılı!");
        }

        [CacheAspect]
        public async Task<IDataResult<PaginatedList<Color>>> GetAllByPaginationAsync(int pageIndex, int pageSize)
        {
            var colors = await _colorDal.GetAllAsync();
            var result = PaginatedList<Color>.Create(colors, pageIndex, pageSize);
            return new SuccessDataResult<PaginatedList<Color>>(result,"Listeleme işlemi başarılı!");
        }
    }
}
