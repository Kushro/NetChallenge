using FluentValidation;
using NetChallenge.Application.Dto.Input;

namespace NetChallenge.Application.Support.Validators
{
    public class AddOfficeRequestValidator : AbstractValidator<AddOfficeRequest>
    {
        public AddOfficeRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.LocationName)
                .NotEmpty();
            RuleFor(x => x.MaxCapacity)
                .GreaterThan(0);
            RuleFor(x => x.AvailableResources)
                .NotEmpty();
        }
    }
}