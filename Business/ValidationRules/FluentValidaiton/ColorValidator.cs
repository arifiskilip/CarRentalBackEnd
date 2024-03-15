using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidaiton
{
	public class ColorValidator: AbstractValidator<Color>
	{
        public ColorValidator()
        {
			RuleFor(x => x.Name).NotEmpty().NotNull();
			RuleFor(x => x.Name).MinimumLength(3).MaximumLength(50);
		}
    }
}
