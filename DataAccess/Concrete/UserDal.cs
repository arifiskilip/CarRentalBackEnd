using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;
using DataAccess.Contexts;
using Entities.Concrete;
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
            var result = from oc in _context.Set<OperationClaim>()
                         join uoc in _context.Set<UserOperationClaim>()
                         on oc.Id equals uoc.OperationClaimId
                         where uoc.UserId == user.Id
                         select new OperationClaim
                         {
                             Id = oc.Id,
                             Name = oc.Name
                         };
            return result.ToList();
        }
    }
}
