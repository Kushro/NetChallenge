using System;
using FluentValidation;
using NetChallenge.Application.Dto.Input;

namespace NetChallenge.Application.Support.Validators
{
    public class BookOfficeRequestValidator : AbstractValidator<BookOfficeRequest>
    {
        public BookOfficeRequestValidator()
        {
            RuleFor(x => x.LocationName)
                .NotEmpty();
            RuleFor(x => x.OfficeName)
                .NotEmpty();
            RuleFor(x => x.UserName)
                .NotEmpty();
            RuleFor(x => x.Duration)
                .GreaterThan(TimeSpan.Zero);
            RuleFor(x => x.DateTime)
                .NotEmpty()
                .GreaterThan(DateTime.Now)
                .WithMessage("Booking date must be greater than the current date");
        }
    }
}