using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    public interface ISpecification<T> where T : AggregateRoot
    {
        bool Predicate(T arg);
    }
}