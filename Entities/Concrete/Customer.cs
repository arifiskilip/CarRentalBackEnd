using Core.Entities;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Customer  : IEntity
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public int UserId { get; set; }


        public virtual User User { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}
