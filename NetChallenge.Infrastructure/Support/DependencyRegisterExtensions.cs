using Microsoft.Extensions.DependencyInjection;
using NetChallenge.Domain.Repositories;
using NetChallenge.Infrastructure.Repositories;

namespace NetChallenge.Infrastructure.Support
{
    public static class DependencyRegisterExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IOfficeRepository, OfficeRepository>();
            services.AddSingleton<ILocationRepository, LocationRepository>();
            services.AddSingleton<IBookingRepository, BookingRepository>();
        }
    }
}