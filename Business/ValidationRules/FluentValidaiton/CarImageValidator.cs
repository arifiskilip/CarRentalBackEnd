using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidaiton
{
    public class CarImageValidator : AbstractValidator<CarImage>
    {
        public CarImageValidator()
        {
            RuleFor(x=> x.CarId).NotEmpty();
        }
    }
}
