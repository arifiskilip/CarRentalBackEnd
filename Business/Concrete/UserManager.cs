using Business.Abstract;
using Business.Constants.ResultMessages;
using Core.Entities;
using Core.Utilities.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
	public class UserManager : IUserService
	{
		private readonly IUserDal _userDal;
		private readonly IUow _uow;
		public UserManager(IUserDal userDal, IUow uow)
		{
			_userDal = userDal;
			_uow = uow;
		}
		public async Task<IResult> AddAsync(User user)
		{
				await _userDal.AddAsync(user);
				await _uow.SaveAsync();
				return new SuccessResult(Messages.General.SuccessAdded);
		}

		public async Task<IResult> DeleteAsync(User user)
		{
			await _userDal.DeleteAsync(user);
			return new SuccessResult();
		}

		public async Task<IDataResult<List<User>>> GetAllAsync()
		{
			return new SuccessDataResult<List<User>>(await _userDal.GetAllAsync());
		}

		public async Task<IDataResult<User>> GetByUserIdAsync(int userId)
		{
			var user = await _userDal.GetAsync(new() { p => p.Id == userId });
			if (user != null)
			{
				return new SuccessDataResult<User>(user, Messages.General.SuccessfulListing);
			}
			return new ErrorDataResult<User>(Messages.General.FailedListing);
		}

		public async Task<IResult> UpdateAsync(User user, IFormFile? file)
		{
			var getUser = await this.GetByUserIdAsync(user.Id);

			if (file != null)
			{
				var imageResult = FileHelper.Add(file);
				if (imageResult.Success)
				{
					if (getUser.Data.Image != null)
					{
						FileHelper.Delete(getUser.Data.Image);
					}
					getUser.Data.FirstName = user.FirstName;
					getUser.Data.LastName = user.LastName;
					getUser.Data.Email = user.Email;
					getUser.Data.Image = imageResult.Message;
					await _userDal.UpdateAsync(getUser.Data);
					await _uow.SaveAsync();
					return new SuccessResult(Messages.General.SuccessUpdate);
				}
			}
			return new ErrorResult();

		}
		public List<OperationClaim> GetClaims(User user)
		{
			return _userDal.GetClaims(user);
		}
		public async Task<User> GetByMailAsync(string email)
		{
			return await _userDal.GetAsync(new() { u => u.Email == email });

		}
	}
}
