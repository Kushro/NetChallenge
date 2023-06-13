using System;
using System.Collections.Generic;
using System.Linq;
using NetChallenge.Domain.Entities;
using NetChallenge.Domain.Repositories;

namespace NetChallenge.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly List<Booking> _bookings;

        public BookingRepository()
        {
            _bookings = new List<Booking>();
        }
        
        public IEnumerable<Booking> AsEnumerable()
        {
            return _bookings.AsEnumerable();
        }

        public void Add(Booking item)
        {
            _bookings.Add(item);
        }
    }
}