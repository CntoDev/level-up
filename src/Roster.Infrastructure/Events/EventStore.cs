using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Events
{
    public class EventStore : IEventStore
    {
        private readonly IBus _bus;
        private readonly IMessageScheduler _messageScheduler;
        private readonly ILogger<EventStore> _logger;

        public EventStore(IBus bus, IMessageScheduler messageScheduler, ILogger<EventStore> logger)
        {
            _bus = bus;
            _messageScheduler = messageScheduler;
            _logger = logger;
        }

        public void Publish<T>(T @event) where T : class, IEvent
        {
            if (@event is IScheduledEvent)
            {
                IScheduledEvent scheduledEvent = (IScheduledEvent)@event;                
                _messageScheduler.SchedulePublish(scheduledEvent.ScheduledForDate, @event);
                _logger.LogInformation("Published scheduled event {@event} for {date}", scheduledEvent, scheduledEvent.ScheduledForDate);
            }
            else
            {
                _bus.Publish(@event);
                _logger.LogInformation("Published event {@event}", @event);
            }
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