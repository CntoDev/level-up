using System;
using System.Collections.Generic;
using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    public interface IStorage<T> where T: AggregateRoot {
        void Add(T aggregateRoot);
        void Remove(T aggregateRoot);
        T Find(object key);
        IEnumerable<T> Search(ISpecification<T> specification);
        IEnumerable<T> Query(Func<T, bool> predicate);
        T QueryOne(Func<T, bool> predicate);
        PaginatedList<T> Page(Func<T, bool> predicate, Func<T, object> keySelector, int pageIndex, int pageSize);
        PaginatedList<T> Page(ISpecification<T> specification, Func<T, object> keySelector, int pageIndex, int pageSize);
        void Save();
        IEnumerable<T> All();
    }
}