using FluentValidation;
using NetChallenge.Application.Dto.Input;

namespace NetChallenge.Application.Support.Validators
{
    public class AddLocationRequestValidator : AbstractValidator<AddLocationRequest>
    {
        public AddLocationRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Neighborhood)
                .NotEmpty();
        }
    }
}