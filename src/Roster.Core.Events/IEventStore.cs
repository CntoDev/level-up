using System.Collections.Generic;

namespace Roster.Core.Events
{
    public interface IEventStore
    {
        Task PublishAsync<T>(T @event) where T : class;
        void Publish(IEnumerable<IEvent> events);
    }
}