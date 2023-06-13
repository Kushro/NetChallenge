using System;
using System.Collections.Generic;
using NetChallenge.Domain.Base;
using NetChallenge.Domain.Exceptions;
using NetChallenge.Domain.Utils;

namespace NetChallenge.Domain.Entities
{
    public class Office : Entity, IValidatableEntity
    {
        public Office(string name, string locationName, int maxCapacity, IEnumerable<string> availableResources)
        {
            Name = name;
            LocationName = locationName;
            MaxCapacity = maxCapacity;
            AvailableResources = availableResources;
            
            Validate();
        }

        public Office(Guid id, string name, string locationName, int maxCapacity, IEnumerable<string> availableResources)
        {
            Name = name;
            LocationName = locationName;
            MaxCapacity = maxCapacity;
            AvailableResources = availableResources;
            
            SetId(id);
            Validate();
        }
        
        public string Name { get; private set; }
        public string LocationName { get; private set; }
        public int MaxCapacity { get; private set; }
        public IEnumerable<string> AvailableResources { get; private set; }
        
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new BusinessException(
                    ExceptionMessageGeneratorUtil.PropertyCannotBeNullOrEmpty(nameof(Office),
                        nameof(Name)));
            
            if (string.IsNullOrWhiteSpace(LocationName))
                throw new BusinessException(
                    ExceptionMessageGeneratorUtil.PropertyCannotBeNullOrEmpty(nameof(Office),
                        nameof(LocationName)));
            
            if (MaxCapacity <= 0)
                throw new BusinessException(
                    ExceptionMessageGeneratorUtil.PropertyCannotBeLessThanOrEqualToZero(nameof(Office),
                        nameof(MaxCapacity)));
            
            if (AvailableResources == null)
                throw new ArgumentNullException(
                    ExceptionMessageGeneratorUtil.PropertyCannotBeNullOrEmpty(nameof(Office),
                        nameof(AvailableResources)));
        }
    }
}