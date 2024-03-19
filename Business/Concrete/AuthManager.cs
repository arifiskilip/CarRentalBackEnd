using Business.Abstract;
using Business.Constants.ResultMessages;
using Core.Entities;
using Core.Extensions;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.UnitOfWork;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IHttpContextAccessor _contextAccessor;
        private readonly IUow _uow;

		public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUow uow, IHttpContextAccessor contextAccessor)
		{
			_userService = userService;
			_tokenHelper = tokenHelper;
			_uow = uow;
			_contextAccessor = contextAccessor;
		}

		public async Task<IDataResult<User>> RegisterAsync(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            await _userService.AddAsync(user);
            await _uow.SaveAsync();
            return new SuccessDataResult<User>(user, Messages.Auth.SuccessRegister);
        }

        public async Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.GetByMailAsync(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.Auth.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.Auth.UsernameOrPaswordNotFound);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.Auth.SuccessLogin);
        }

        public async Task<IResult> UserExistsAsync(string email)
        {
            if (await _userService.GetByMailAsync(email) != null)
            {
                return new ErrorResult(Messages.Auth.CurrentUser);
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.Auth.CreatedToken);
        }
	}
}
