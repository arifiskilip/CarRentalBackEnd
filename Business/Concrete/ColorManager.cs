using Business.Abstract;
using Business.Constants.ResultMessages;
using Business.ValidationRules.FluentValidaiton;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Pagination;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class ColorManager : IColorService
    {
		private readonly IColorDal _colorDal;
		private readonly IUow _uow;

		public ColorManager(IColorDal colorDal, IUow uow)
		{
			_colorDal = colorDal;
			_uow = uow;
		}

		[ValidationAspect(typeof(ColorValidator))]
        public async Task<IDataResult<Color>> AddAsync(Color color)
        {
            var addedColor = await _colorDal.AddAsync(color);
            return new SuccessDataResult<Color>(addedColor,Messages.General.SuccessAdded);
        }

		public async Task<IResult> DeleteAsync(int id)
		{
			var checkColor = await _colorDal.GetAsync(new()
			{
				x=> x.Id == id
			});
			if (checkColor != null)
			{
				await _colorDal.DeleteAsync(checkColor);
				await _uow.SaveAsync();
				return new SuccessResult(Messages.General.SuccessDelete);
			}
			return new ErrorResult(Messages.General.ErrorDelete);
		}

        public async Task<IDataResult<List<Color>>> GetAllAsync()
        {
            var result = await _colorDal.GetAllAsync();
            return new SuccessDataResult<List<Color>>(result, Messages.General.SuccessfulListing);
        }

        public async Task<IDataResult<PaginatedList<Color>>> GetAllByPaginationAsync(int pageIndex, int pageSize)
        {
            var colors = await _colorDal.GetAllAsync();
            var result = PaginatedList<Color>.Create(colors, pageIndex, pageSize);
            return new SuccessDataResult<PaginatedList<Color>>(result,Messages.General.SuccessfulListing);
        }

		public async Task<IDataResult<Color>> GetByIdAsync(int id)
		{
			var checkColor = await _colorDal.GetAsync(new()
			{
				x=> x.Id == id
			});
			if (checkColor != null)
			{
				return new SuccessDataResult<Color>(checkColor, Messages.General.SuccessfulListing);
			}
			return new ErrorDataResult<Color>(Messages.General.FailedListing);
		}
		[ValidationAspect(typeof(ColorValidator))]
		public async Task<IResult> UpdateAsync(Color color)
		{
			await _colorDal.UpdateAsync(color);
			await _uow.SaveAsync();
			return new SuccessResult(Messages.General.SuccessUpdate);
		}
	}
}
