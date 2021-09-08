using System.Collections.Generic;
using Roster.Core.Events;

namespace Roster.Core.Domain
{
    public abstract class AggregateRoot : IEventSource
    {
        private List<IEvent> _events = new List<IEvent>();

        public IEnumerable<IEvent> Events() => _events;

        protected void Publish(IEvent @event)
        {
            _events.Add(@event);
        }
    }
}