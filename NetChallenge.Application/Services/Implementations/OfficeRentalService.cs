using System;
using System.Collections.Generic;
using System.Linq;
using NetChallenge.Application.Dto.Input;
using NetChallenge.Application.Dto.Output;
using NetChallenge.Domain.Entities;
using NetChallenge.Domain.Exceptions;
using NetChallenge.Domain.Repositories;

namespace NetChallenge.Application.Services.Implementations
{
    public class OfficeRentalService : IOfficeRentalService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IBookingRepository _bookingRepository;

        public OfficeRentalService(ILocationRepository locationRepository, IOfficeRepository officeRepository,
            IBookingRepository bookingRepository)
        {
            _locationRepository = locationRepository;
            _officeRepository = officeRepository;
            _bookingRepository = bookingRepository;
        }

        public IEnumerable<BookingDto> GetBookings(string locationName, string officeName)
        {
            return _bookingRepository
                .AsEnumerable()
                .Where(b => b.LocationName == locationName && b.OfficeName == officeName)
                .Select(b => new BookingDto()
                {
                    LocationName = b.LocationName,
                    OfficeName = b.OfficeName,
                    DateTime = b.DateTime,
                    Duration = b.Duration,
                    UserName = b.UserName
                });
        }

        public void BookOffice(BookOfficeRequest payload)
        {
            var office = _officeRepository
                .AsEnumerable()
                .FirstOrDefault(o => o.LocationName == payload.LocationName && o.Name == payload.OfficeName);
            
            if(office == null)
                throw new BusinessException("An office with the provided criteria does not exist.");

            var overlappingBookings = _bookingRepository
                .AsEnumerable()
                .Any(b =>
                    b.LocationName == payload.LocationName
                    && b.OfficeName == payload.OfficeName
                    && ((b.DateTime == payload.DateTime && b.Duration == payload.Duration) //same booking
                        || (b.DateTime <= payload.DateTime //overlapping inside booking
                            && b.Duration > payload.Duration)
                        || (b.DateTime >= payload.DateTime //overlapping outside booking
                            && b.Duration < payload.Duration)
                        || (b.DateTime <= payload.DateTime //overlapping start
                            && b.DateTime + b.Duration > payload.DateTime)
                        || (b.DateTime < payload.DateTime + payload.Duration //overlapping end
                            && b.DateTime + b.Duration >= payload.DateTime + payload.Duration)));
            
            if(overlappingBookings)
                throw new BusinessException("An office is already booked within the provided time frame.");
            
            //Romperia los tests y no me lo piden explicitamente, pero lo infiero por la
            //frase "Las oficinas se alquilan por hora individualmente" de la intro
            /*
            var userHasBookingAtSameTimeInOtherOffices = _bookingRepository
                .AsEnumerable()
                .Any(b =>
                    b.UserName == payload.UserName
                    && b.DateTime == payload.DateTime);

            if (userHasBookingAtSameTimeInOtherOffices)
                throw new BusinessException("A user cannot book more than one office at the same datetime.");
            */

            var booking = new Booking(office.LocationName, office.Name, payload.DateTime, payload.Duration, payload.UserName);
            
            _bookingRepository.Add(booking);
        }

        public IEnumerable<LocationDto> GetLocations()
        {
            return _locationRepository
                .AsEnumerable()
                .GroupJoin(_officeRepository.AsEnumerable(),
                    l => l.Name,
                    o => o.LocationName,
                    (l, o) => l.AddOffices(o))
                .Select(l => new LocationDto()
                {
                    Name = l.Name,
                    Neighborhood = l.Neighborhood,
                });
        }

        public void AddLocation(AddLocationRequest payload)
        {
            var locationFound = _locationRepository
                .AsEnumerable()
                .Any(l => l.Name == payload.Name);
            
            if(locationFound) 
                throw new BusinessException("An already existent location cannot be added.");
            
            var location = new Location(payload.Name, payload.Neighborhood);
            
            _locationRepository.Add(location);
        }
        
        public IEnumerable<OfficeDto> GetOffices(string locationName)
        {
            return _officeRepository
                .AsEnumerable()
                .Where(o => string.Equals(o.LocationName, locationName, StringComparison.InvariantCultureIgnoreCase))
                .Select(o => new OfficeDto()
                {
                    LocationName = o.LocationName,
                    Name = o.Name,
                    MaxCapacity = o.MaxCapacity,
                    AvailableResources = o.AvailableResources.ToArray()
                });
        }

        public void AddOffice(AddOfficeRequest payload)
        {
            var officeFoundOnLocation = _officeRepository
                .AsEnumerable()
                .Any(o => string.Equals(o.Name, payload.Name, StringComparison.InvariantCultureIgnoreCase)
                    && string.Equals(o.LocationName, payload.LocationName, StringComparison.InvariantCultureIgnoreCase));
            
            var locationNotFound = _locationRepository
                .AsEnumerable()
                .All(l => l.Name != payload.LocationName);
            
            if (officeFoundOnLocation)
                throw new BusinessException("An office already exists on the provided location.");
            
            if(locationNotFound)
                throw new BusinessException("A location with the provided name does not exist.");

            var office = new Office(payload.Name, payload.LocationName, payload.MaxCapacity, payload.AvailableResources);
            
            _officeRepository.Add(office);
        }

        public IEnumerable<OfficeDto> GetOfficeSuggestions(SuggestionsRequest payload)
        {
            var query = _officeRepository
                .AsEnumerable()
                .Where(o => o.MaxCapacity >= payload.CapacityNeeded
                    && (payload.ResourcesNeeded.Any() 
                        && payload.ResourcesNeeded.All(rn =>
                            o.AvailableResources.Contains(rn))
                    || !payload.ResourcesNeeded.Any()))
                .OrderByDescending(o =>
                    !string.IsNullOrWhiteSpace(payload.PreferedNeigborHood)
                        && o.LocationName
                            .ToLower()
                            .Contains(payload.PreferedNeigborHood.ToLower()))
                .ThenBy(o => o.AvailableResources.Count())
                .ThenBy(o => o.MaxCapacity);

            return query.Select(o => new OfficeDto()
                {
                    LocationName = o.LocationName,
                    Name = o.Name,
                    MaxCapacity = o.MaxCapacity,
                    AvailableResources = o.AvailableResources.ToArray()
                });
        }
    }
}