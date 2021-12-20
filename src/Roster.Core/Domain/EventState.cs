using System;
using Newtonsoft.Json;
using Roster.Core.Events;

namespace Roster.Core.Domain
{
    public class EventState : AggregateRoot
    {
        public static EventState CreateFromEvent<T>(T @event) where T : IEvent
        {
            EventState eventState = new()
            {
                EventDate = DateTime.UtcNow,
                EventType = typeof(T).Name,
                Json = JsonConvert.SerializeObject(@event)
            };
            return eventState;
        }

        public long Id { get; private set; }

        public DateTime EventDate { get; private set; }

        public string EventType { get; private set; }

        public string Json { get; private set; }
    }
}