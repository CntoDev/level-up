using System.Collections.Generic;
using System.Linq;
using Roster.Core.Domain;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Storage
{
    public abstract class Storage<T> : IStorage<T> where T : AggregateRoot
    {
        protected readonly RosterDbContext _rosterDbContext;

        public Storage(RosterDbContext rosterDbContext)
        {
            _rosterDbContext = rosterDbContext;
        }

        public void Add(T aggregateRoot) => _rosterDbContext.Add(aggregateRoot);

        public IEnumerable<T> All() => _rosterDbContext.Set<T>();

        public T Find(object key) => _rosterDbContext.Find<T>(key);

        public void Remove(T aggregateRoot) => _rosterDbContext.Remove(aggregateRoot);

        public void Save() => _rosterDbContext.SaveChanges();

        public IEnumerable<T> Search(ISpecification<T> specification) =>  _rosterDbContext.Set<T>().Where(specification.Predicate);
    }
}