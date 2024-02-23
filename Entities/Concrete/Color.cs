using Core.Entities;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Color :IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public ICollection<Car> Cars { get; set; }

    }
}
