using Business.Abstract;
using Business.Constants.ResultMessages;
using Business.ValidationRules.FluentValidaiton;
using Core.Aspects.Autofac.SecuredOperation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class BrandManager : IBrandService
    {
        private readonly IBrandDal _brandDal;
		private readonly IUow _uow;

		public BrandManager(IBrandDal brandDal, IUow uow)
		{
			_brandDal = brandDal;
			_uow = uow;
		}

		[SecuredOperation("admin")]
		[ValidationAspect(typeof(BrandValidator))]
		public async Task<IDataResult<Brand>> AddAsync(Brand brand)
		{
			var addedBrand = await _brandDal.AddAsync(brand);
			await _uow.SaveAsync();
			return new SuccessDataResult<Brand>(addedBrand,Messages.General.SuccessAdded);
		}

		//public async Task<IResult> DeleteAsync(Brand brand)
		//{
		//	var checkUser = await _brandDal.GetAsync(new()
		//	{
		//		x=> x.Id == brand.Id
		//	});
		//	if (checkUser != null)
		//	{
		//		await _brandDal.DeleteAsync(checkUser);
		//		await _uow.SaveAsync();
		//		return new SuccessResult(Messages.General.SuccessDelete);
		//	}
		//	return new ErrorResult(Messages.General.ErrorDelete);
		//}
		[SecuredOperation("admin")]
		public async Task<IResult> DeleteAsync(int id)
		{
			var checkUser = await _brandDal.GetAsync(new()
			{
				x=> x.Id == id
			});
			if (checkUser != null)
			{
				await _brandDal.DeleteAsync(checkUser);
				await _uow.SaveAsync();
				return new SuccessResult(Messages.General.SuccessDelete);
			}
			return new ErrorResult(Messages.General.ErrorDelete);
		}

		public async Task<IDataResult<List<Brand>>> GetAllAsync()
        {
            var result = await _brandDal.GetAllAsync();
            return new SuccessDataResult<List<Brand>>(result, Messages.General.SuccessfulListing);
        }

		public async Task<IDataResult<Brand>> GetByIdAsync(int id)
		{
			var checkUser = await _brandDal.GetAsync(new()
			{
				x=> x.Id == id
			});
			if (checkUser != null)
			{
				return new SuccessDataResult<Brand>(checkUser,Messages.General.SuccessfulListing);
			}
			return new ErrorDataResult<Brand>(Messages.General.FailedListing);
		}
		[SecuredOperation("admin")]
		[ValidationAspect(typeof(BrandValidator))]
		public async Task<IResult> UpdateAsync(Brand brand)
		{
			await _brandDal.UpdateAsync(brand);
			await _uow.SaveAsync();
			return new SuccessResult(Messages.General.SuccessUpdate);
		}
	}
}
