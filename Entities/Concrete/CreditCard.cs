using Core.Entities;

namespace Entities.Concrete
{
	public class CreditCard : IEntity
	{
        public int Id { get; set; }
        public string CardOwner { get; set; }
        public string CardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVV { get; set; }
    }
}
