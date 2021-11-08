using System.Collections.Generic;

namespace Roster.Core.Events
{
    public interface IEventStore
    {
        void Publish<T>(T @event) where T : class, IEvent;
        void Publish<T>(IEnumerable<T> events) where T : class, IEvent;
    }
}