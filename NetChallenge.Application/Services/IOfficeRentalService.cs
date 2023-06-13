using System.Collections.Generic;
using NetChallenge.Application.Dto.Input;
using NetChallenge.Application.Dto.Output;

namespace NetChallenge.Application.Services
{
    public interface IOfficeRentalService : IBookingService, ILocationService, IOfficeService { }
}