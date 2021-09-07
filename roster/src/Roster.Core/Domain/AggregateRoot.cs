using System.Collections.Generic;
using Roster.Core.Events;

namespace Roster.Core.Domain
{
    public abstract class AggregateRoot : IEventSource
    {
        protected List<IEvent> _events = new List<IEvent>();

        public IEnumerable<IEvent> Events() => _events;
    }
}