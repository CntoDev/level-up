using System.Collections.Generic;
using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    public interface IStorage<T> where T: AggregateRoot {
        void Add(T aggregateRoot);
        void Remove(T aggregateRoot);
        T Find(object key);
        IEnumerable<T> Search(ISpecification<T> specification);
        void Save();
        IEnumerable<T> All();
    }
}