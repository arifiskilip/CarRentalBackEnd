using Entities.Concrete;
using FluentValidation;
using System;

namespace Business.ValidationRules.FluentValidaiton
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(x => x.RentDate).NotEmpty().NotNull();
            RuleFor(x => x.RentDate).GreaterThanOrEqualTo(DateTime.Now);
        }
    }
}
