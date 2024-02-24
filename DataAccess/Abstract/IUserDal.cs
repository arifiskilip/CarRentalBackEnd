using Core.DataAccess.EntityFramework;
using Core.Entities;
using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IUserDal : IGenericRepository<User>
    {
         List<OperationClaim> GetClaims(User user);
    }
}
