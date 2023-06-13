using System;
using System.Collections.Generic;
using System.Linq;
using NetChallenge.Domain.Entities;
using NetChallenge.Domain.Repositories;

namespace NetChallenge.Infrastructure.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly List<Office> _offices;

        public OfficeRepository()
        {
            _offices = new List<Office>();
            
            /*
            _offices = new List<Office>()
            {
                new Office("Office 1", "Location 1", 1, new[] {"Wood", "Gold"}),
                new Office("Office 2", "Location 2", 2, new[] {"Wool", "Rock"}),
                new Office("Office 3", "Location 3", 3, new[] {"Wool", "Rock"}),
                new Office("Office 4", "Location 4", 4, new[] {"Wool", "Rock"}),
                new Office("Office 5", "Location 5", 5, new[] {"Wool", "Rock"}),
            };
            */
        }
        
        public IEnumerable<Office> AsEnumerable()
        {
            return _offices.AsEnumerable();
        }

        public void Add(Office item)
        {
            _offices.Add(item);
        }
    }
}