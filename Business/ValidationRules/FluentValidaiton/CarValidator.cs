using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidaiton
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(x => x.ModelName).NotEmpty().NotNull();
            RuleFor(x => x.ModelName).MinimumLength(2);
            RuleFor(x => x.DailyPrice).GreaterThan(0); 
        }
    }
}
