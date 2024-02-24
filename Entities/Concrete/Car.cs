using Core.Entities;
using System.Collections.Generic;

namespace Entities.Concrete
{
    public class Car : IEntity
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string ModelName { get; set; }
        public string ModelYear { get; set; }
        public double DailyPrice { get; set; }
        public string Description { get; set; }


        public virtual Brand Brand { get; set; }
        public virtual Color Color { get; set; }
        public ICollection<Rental> Rentals { get; set; }
        public ICollection<CarImage> CarImages { get; set; }
    }
}
