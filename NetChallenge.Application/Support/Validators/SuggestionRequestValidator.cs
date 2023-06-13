using FluentValidation;
using NetChallenge.Application.Dto.Input;

namespace NetChallenge.Application.Support.Validators
{
    public class SuggestionRequestValidator : AbstractValidator<SuggestionsRequest>
    {
        public SuggestionRequestValidator()
        {
            RuleFor(x => x.CapacityNeeded)
                .GreaterThan(0);
        }
    }
}