using System.Linq;
using AutoFixture;
using Moq;
using NetChallenge.Application.Services.Implementations;
using NetChallenge.Domain.Entities;
using NetChallenge.Domain.Repositories;
using Xunit;

namespace NetChallenge.Test.Application.Services.Implementations
{
    public class OfficeRentalServiceTest
    {
        private readonly OfficeRentalService _subject;
        private readonly Fixture _fixture;
        private readonly Mock<ILocationRepository> _locationRepositoryMock;
        private readonly Mock<IOfficeRepository> _officeRepositoryMock;
        private readonly Mock<IBookingRepository> _bookingRepositoryMock;
        
        public OfficeRentalServiceTest()
        {
            _fixture = new Fixture();

            _locationRepositoryMock = new Mock<ILocationRepository>();
            _officeRepositoryMock = new Mock<IOfficeRepository>();
            _bookingRepositoryMock = new Mock<IBookingRepository>();

            _subject = new OfficeRentalService(
                _locationRepositoryMock.Object,
                _officeRepositoryMock.Object,
                _bookingRepositoryMock.Object);
        }
        
        [Fact]
        public void When_GetLocationsIsCalled_Then_ShouldReturnLocations()
        {
            var offices = _fixture.CreateMany<Office>();
            var locations = _fixture.CreateMany<Location>();
            
            _officeRepositoryMock
                .Setup(x => x.AsEnumerable())
                .Returns(offices);
            
            _locationRepositoryMock
                .Setup(x => x.AsEnumerable())
                .Returns(locations);

            var result = _subject.GetLocations();
            
            Assert.NotEmpty(result);
        }
        
        [Fact]
        public void When_GetLocationsIsCalled_Then_ShouldReturnNoLocations()
        {
            _officeRepositoryMock
                .Setup(x => x.AsEnumerable())
                .Returns(Enumerable.Empty<Office>());
            
            _locationRepositoryMock
                .Setup(x => x.AsEnumerable())
                .Returns(Enumerable.Empty<Location>());

            var result = _subject.GetLocations();

            Assert.Empty(result);
        }
    }
}