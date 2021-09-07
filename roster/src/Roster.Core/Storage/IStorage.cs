using System.Collections.Generic;
using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    public interface IStorage<T> where T: AggregateRoot {
        void Add(T aggregateRoot);
        void Remove(T aggregateRoot);
        T Find(object key);
        void Save();
    }
}