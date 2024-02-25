using System.Collections.Generic;

namespace Core.Entities
{
    public class OperationClaim : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
