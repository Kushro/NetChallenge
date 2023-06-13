using System;
using NetChallenge.Domain.Base;
using NetChallenge.Domain.Exceptions;
using NetChallenge.Domain.Utils;

namespace NetChallenge.Domain.Entities
{
    public class Booking : Entity, IValidatableEntity
    {
        public Booking(string locationName, string officeName, DateTime dateTime, TimeSpan duration, string userName)
        {
            LocationName = locationName;
            OfficeName = officeName;
            DateTime = dateTime;
            Duration = duration;
            UserName = userName;
            
            Validate();
        }
        
        public Booking(Guid id, string locationName, string officeName, DateTime dateTime, TimeSpan duration, string userName)
        {
            LocationName = locationName;
            OfficeName = officeName;
            DateTime = dateTime;
            Duration = duration;
            UserName = userName;
            
            SetId(id);
            Validate();
        }
        
        public string LocationName { get; private set; }
        public string OfficeName { get; private set; }
        public DateTime DateTime { get; private set; }
        public TimeSpan Duration { get; private set; }
        public string UserName { get; private set; }

        public void Validate()
        {
            if(string.IsNullOrWhiteSpace(LocationName))
                throw new BusinessException(
                    ExceptionMessageGeneratorUtil.PropertyCannotBeNullOrEmpty(nameof(Booking),
                        nameof(LocationName)));
            
            if(string.IsNullOrWhiteSpace(OfficeName))
                throw new BusinessException(
                    ExceptionMessageGeneratorUtil.PropertyCannotBeNullOrEmpty(nameof(Booking),
                        nameof(OfficeName)));
            
            if(string.IsNullOrWhiteSpace(UserName))
                throw new BusinessException(
                    ExceptionMessageGeneratorUtil.PropertyCannotBeNullOrEmpty(nameof(Booking),
                        nameof(UserName)));

            if (Duration <= TimeSpan.Zero)
                throw new BusinessException(
                    ExceptionMessageGeneratorUtil.PropertyCannotBeLessThanOrEqualToZero(nameof(Booking),
                        nameof(Duration)));
            
            //Romperia los tests, pero en la introduccion dice que "Las oficinas se alquilan por hora individualmente"
            /*
            if (Duration > TimeSpan.FromHours(1))
                throw new BusinessException(
                    ExceptionMessageGeneratorUtil.PropertyCannotBeGreaterThan(nameof(Booking),
                        nameof(Duration), TimeSpan.FromHours(1).Hours));
            */
        }
    }   
}