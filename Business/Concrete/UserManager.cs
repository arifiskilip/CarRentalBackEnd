using Business.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.UnitOfWork;
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
            return new SuccessResult();
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
            return new SuccessDataResult<User>(await _userDal.GetAsync(new() { p => p.Id == userId }));
        }

        public async Task<IResult> UpdateAsync(User user)
        {
            await _userDal.UpdateAsync(user);
            return new SuccessResult();
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
