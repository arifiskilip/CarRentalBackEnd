using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using DataAccess.Contexts;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete
{
    public class UserDal : GenericRepository<User>, IUserDal
    {
        public UserDal(CarRentalContext context) : base(context)
        {
        }

        public List<OperationClaim> GetClaims(User user)
        {
            var result = from operationClaim in _context.Set<OperationClaim>()
                         join userOperationClaim in _context.Set<UserOperationClaim>()
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            return result.ToList();

        }
    }
}
