using System.Collections.Generic;
using MassTransit;
using Roster.Core.Events;

namespace Roster.Infrastructure.Events
{
    public class EventStore : IEventStore
    {
        private readonly IBus _bus;

        public EventStore(IBus bus)
        {
            _bus = bus;
        }

        public void Publish<T>(T @event) where T : class, IEvent
        {
            _bus.Publish<T>(@event);
        }

        void IEventStore.Publish<T>(IEnumerable<T> events)
        {
            foreach (var @event in events)
                Publish(@event);
        }
    }
}