using System.Collections.Generic;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Roster.Core.Events;

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

        public async Task PublishAsync<T>(T @event) where T : class
        {
            if (@event is IScheduledEvent)
            {
                IScheduledEvent scheduledEvent = (IScheduledEvent)@event;
                _logger.LogDebug("PUBLISH SCHEDULED {@event} {date}", scheduledEvent, scheduledEvent.ScheduledForDate);                
                await _messageScheduler.SchedulePublish<T>(scheduledEvent.ScheduledForDate, @event);
            }
            else
            {
                _logger.LogDebug("PUBLISH {@event}", @event);                
                await _bus.Publish<T>(@event);
            }
        }

        void IEventStore.Publish(IEnumerable<IEvent> events)
        {
            _logger.LogDebug("PUBLISH MULTIPLE {@events}", events);

            Task.Run(async () =>
            {
                foreach (var @event in events)
                {
                    await PublishAsync((dynamic)@event);
                    await Task.Delay(500);
                }
            });
        }
    }
}