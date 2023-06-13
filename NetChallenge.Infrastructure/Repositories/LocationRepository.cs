using System.Collections.Generic;
using System.Linq;
using NetChallenge.Domain.Entities;
using NetChallenge.Domain.Repositories;

namespace NetChallenge.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly List<Location> _locations;

        public LocationRepository()
        {
            _locations = new List<Location>();
            
            /*
            _locations = new List<Location>()
            {
                new Location("Location 1", "Neighborhood 1"),
                new Location("Location 2", "Neighborhood 2"),
                new Location("Location 3", "Neighborhood 3"),
                new Location("Location 4", "Neighborhood 4"),
                new Location("Location 5", "Neighborhood 5"),
            };
            */
        }

        public IEnumerable<Location> AsEnumerable()
        {
            return _locations.AsEnumerable();
        }

        public void Add(Location item)
        {
            _locations.Add(item);
        }
    }
}