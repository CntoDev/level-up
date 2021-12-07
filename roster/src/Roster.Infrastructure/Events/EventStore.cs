using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Events
{
    public class EventStore : IEventStore
    {
        private readonly IBus _bus;
        private readonly IMessageScheduler _messageScheduler;
        private readonly IStorage<EventState> _storage;

        public EventStore(IBus bus, IMessageScheduler messageScheduler, IStorage<EventState> storage)
        {
            _bus = bus;
            _messageScheduler = messageScheduler;
            _storage = storage;
        }

        public void Publish<T>(T @event) where T : class, IEvent
        {
            if (@event is IScheduledEvent)
            {
                IScheduledEvent scheduledEvent = (IScheduledEvent)@event;
                _messageScheduler.SchedulePublish(scheduledEvent.ScheduledForDate, @event);
            }
            else
                _bus.Publish(@event);

            EventState eventState = EventState.CreateFromEvent(@event);
            _storage.Add(eventState);
            _storage.Save();
        }

        void IEventStore.Publish(IEnumerable<IEvent> events)
        {
            Task.Run(async () =>
            {
                foreach (var @event in events)
                {
                    Publish((dynamic)@event);
                    await Task.Delay(500);
                }
            });
        }
    }
}