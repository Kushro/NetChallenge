using System.Collections.Generic;
using NetChallenge.Application.Dto.Input;
using NetChallenge.Application.Dto.Output;

namespace NetChallenge.Application.Services
{
    public interface IBookingService
    {
        IEnumerable<BookingDto> GetBookings(string locationName, string officeName);
        void BookOffice(BookOfficeRequest payload);
        IEnumerable<OfficeDto> GetOfficeSuggestions(SuggestionsRequest payload);
    }
}