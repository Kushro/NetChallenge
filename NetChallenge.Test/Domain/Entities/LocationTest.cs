using System.Collections.Generic;
using AutoFixture;
using NetChallenge.Domain.Entities;
using NetChallenge.Domain.Exceptions;
using Xunit;

namespace NetChallenge.Test.Domain.Entities
{
    public class LocationTest
    {
        private readonly Fixture _fixture;
        private readonly Location _subject;
        private readonly IEnumerable<Office> _offices;

        public LocationTest()
        {
            _fixture = new Fixture();
            
            _subject = new Location("Location 1", "Chinatown");

            _offices = _fixture.CreateMany<Office>();
        }
        
        [Fact]
        public void When_AddOfficesIsCalled_Then_OfficesShouldBeAdded()
        {
            _subject.AddOffices(_offices);
            
            Assert.Equal(_offices, _subject.Offices);
        }
        
        [Fact]
        public void When_AddOfficesIsCalledWithNull_Then_ShouldThrowBusinessException()
        {
            Assert.Throws<BusinessException>(() =>
                _subject.AddOffices(null));
        }
    }
}