using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using NetChallenge.Application.Dto.Input;
using NetChallenge.Application.Services;
using NetChallenge.Application.Services.Implementations;
using NetChallenge.Application.Support.Validators;
using System.Globalization;

namespace NetChallenge.Application.Support
{
    public static class DependencyRegisterExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IOfficeRentalService, OfficeRentalService>();

            services.AddFluentValidationAutoValidation();
            
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");

            services.AddScoped<IValidator<AddLocationRequest>, AddLocationRequestValidator>();
            services.AddScoped<IValidator<AddOfficeRequest>, AddOfficeRequestValidator>();
            services.AddScoped<IValidator<BookOfficeRequest>, BookOfficeRequestValidator>();
            services.AddScoped<IValidator<SuggestionsRequest>, SuggestionRequestValidator>();
        }
    }   
}