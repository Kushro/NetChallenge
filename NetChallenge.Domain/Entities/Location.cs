using System;
using System.Collections.Generic;
using NetChallenge.Domain.Base;
using NetChallenge.Domain.Exceptions;
using NetChallenge.Domain.Utils;

namespace NetChallenge.Domain.Entities
{
    public class Location : Entity, IValidatableEntity
    {
        private readonly List<Office> _offices;
        
        public Location(string name, string neighborhood, List<Office> offices = null)
        {
            Name = name;
            Neighborhood = neighborhood;
            _offices = offices ?? new List<Office>();
            
            Validate();
        }

        public Location(Guid id, string name, string neighborhood, List<Office> offices = null)
        {
            Name = name;
            Neighborhood = neighborhood;
            _offices = offices ?? new List<Office>();
            
            SetId(id);
            Validate();
        }

        public string Name { get; private set; }
        public string Neighborhood { get; private set; }

        public IEnumerable<Office> Offices => _offices;
        
        public Location AddOffices(IEnumerable<Office> offices)
        {
            if(offices == null)
                throw new BusinessException( $"The {nameof(offices)} parameter cannot be null.");

            _offices.AddRange(offices);

            return this;
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new BusinessException(
                    ExceptionMessageGeneratorUtil.PropertyCannotBeNullOrEmpty(nameof(Location),
                        nameof(Name)));

            if (string.IsNullOrWhiteSpace(Neighborhood))
                throw new BusinessException(
                    ExceptionMessageGeneratorUtil.PropertyCannotBeNullOrEmpty(nameof(Location),
                        nameof(Neighborhood)));
        }
    }   
}