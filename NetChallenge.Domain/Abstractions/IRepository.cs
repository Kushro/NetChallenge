using System.Collections.Generic;

namespace NetChallenge.Domain.Abstractions
{
    public interface IRepository<T>
    {
        IEnumerable<T> AsEnumerable();

        void Add(T item);
    }   
}