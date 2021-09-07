using System.Collections.Generic;
using Roster.Core.Events;

namespace Roster.Core.Domain
{
    public abstract class AggregateRoot : IEventSource
    {
        private List<IEventStore> _stores = new List<IEventStore>();

        public void Register<T>(IEventStore store) where T: class, IEvent
        {
            _stores.Add(store);
        }

        protected void Publish<T>(T @event) where T : class, IEvent
        {
            foreach (var store in _stores)
                store.Publish(@event);
        }
    }
}