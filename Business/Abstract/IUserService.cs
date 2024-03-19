 using Core.Entities;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<IResult> AddAsync(User user);
        Task<IResult> DeleteAsync(User user);
        Task<IResult> UpdateAsync(User user, IFormFile file);
        Task<IDataResult<List<User>>> GetAllAsync();
        Task<IDataResult<User>> GetByUserIdAsync(int userId);
        List<OperationClaim> GetClaims(User user);
        Task<User> GetByMailAsync(string email);
    }
}
