using System;
using System.Collections.Generic;
using System.Linq;
using Roster.Core.Domain;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Storage
{
    public class Storage<T> : IStorage<T> where T : AggregateRoot
    {
        protected readonly RosterDbContext _rosterDbContext;

        public Storage(RosterDbContext rosterDbContext)
        {
            _rosterDbContext = rosterDbContext;
        }

        public void Add(T aggregateRoot) => _rosterDbContext.Add(aggregateRoot);

        public IEnumerable<T> All() => _rosterDbContext.Set<T>();

        public T Find(object key) => _rosterDbContext.Find<T>(key);

        public PaginatedList<T> Page(Func<T, bool> predicate, Func<T, object> keySelector, int pageIndex, int pageSize)
        {
            var items = _rosterDbContext.Set<T>()
                                        .Where(predicate);

            var page = items.OrderBy(keySelector)
                            .Skip((pageIndex-1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            return new PaginatedList<T>(page, items.Count(), pageIndex, pageSize);
        }

        public PaginatedList<T> Page(ISpecification<T> specification, Func<T, object> keySelector, int pageIndex, int pageSize) => Page(specification.Predicate, keySelector, pageIndex, pageSize);

        public IEnumerable<T> Query(Func<T, bool> predicate) => _rosterDbContext.Set<T>().Where(predicate);

        public T QueryOne(Func<T, bool> predicate) => _rosterDbContext.Set<T>().First(predicate);

        public void Remove(T aggregateRoot) => _rosterDbContext.Remove(aggregateRoot);

        public void Save() => _rosterDbContext.SaveChanges();

        public IEnumerable<T> Search(ISpecification<T> specification) =>  _rosterDbContext.Set<T>().Where(specification.Predicate);
    }
}