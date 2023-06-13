using System;

namespace NetChallenge.Domain.Base
{
    public abstract class Entity
    {
        public Guid Id { get; private set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        protected void SetId(Guid id)
        {
            Id = id;
        }
    }
}