using System.Collections.Generic;
using NetChallenge.Application.Dto.Input;
using NetChallenge.Application.Dto.Output;

namespace NetChallenge.Application.Services
{
    public interface ILocationService
    {
        IEnumerable<LocationDto> GetLocations();
        void AddLocation(AddLocationRequest payload);
    }
}