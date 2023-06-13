using NetChallenge.Application.Services.Implementations;
using NetChallenge.Domain.Repositories;
using NetChallenge.Infrastructure.Repositories;

namespace NetChallenge.Test
{
    public class OfficeRentalServiceTest
    {
        protected OfficeRentalService Service;
        protected ILocationRepository LocationRepository;
        protected IOfficeRepository OfficeRepository;
        protected IBookingRepository BookingRepository;

        public OfficeRentalServiceTest()
        {
            LocationRepository = new LocationRepository();
            OfficeRepository = new OfficeRepository();
            BookingRepository = new BookingRepository();
            Service = new OfficeRentalService(LocationRepository, OfficeRepository, BookingRepository);
        }
    }
}