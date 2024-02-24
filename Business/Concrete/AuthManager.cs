using Business.Abstract;
using Core.Entities;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess.UnitOfWork;
using Entities.Dtos;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private readonly IUow _uow;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUow uow)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _uow = uow;
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
            return new SuccessDataResult<User>(user, "Kayıt işlemi başarılı!");
        }

        public async Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.GetByMailAsync(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>("Kullanıcı bulunamadı");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>("Kullanıcı adı veye şifre hatalı!");
            }

            return new SuccessDataResult<User>(userToCheck, "Giriş işlemi başarılı!");
        }

        public async Task<IResult> UserExistsAsync(string email)
        {
            if (await _userService.GetByMailAsync(email) != null)
            {
                return new ErrorResult("Böyle bir kullanıcı zaten mevcut!");
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu!");
        }
    }
}
