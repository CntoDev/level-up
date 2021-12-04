using System;
using System.Threading.Tasks;
using MassTransit;
using Newtonsoft.Json;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Consumers
{
    public class EventSink : 
        IConsumer<ApplicationFormAccepted>, IConsumer<ApplicationFormRejected>, IConsumer<ApplicationFormSubmitted>, IConsumer<EmailChallenged>,
        IConsumer<MemberCreated>, IConsumer<MemberEmailVerified>, IConsumer<MemberPromoted>
    {
        private readonly IEventStateStorage _storage;

        public EventSink(IEventStateStorage storage)
        {
            _storage = storage;
        }

        public Task Consume(ConsumeContext<ApplicationFormAccepted> context) => StoreEvent<ApplicationFormAccepted>(context);

        public Task Consume(ConsumeContext<ApplicationFormRejected> context) => StoreEvent<ApplicationFormRejected>(context);

        public Task Consume(ConsumeContext<ApplicationFormSubmitted> context) => StoreEvent<ApplicationFormSubmitted>(context);

        public Task Consume(ConsumeContext<EmailChallenged> context) => StoreEvent<EmailChallenged>(context);

        public Task Consume(ConsumeContext<MemberCreated> context) => StoreEvent<MemberCreated>(context);

        public Task Consume(ConsumeContext<MemberEmailVerified> context) => StoreEvent<MemberEmailVerified>(context);

        public Task Consume(ConsumeContext<MemberPromoted> context) => StoreEvent<MemberPromoted>(context);

        private Task StoreEvent<T>(ConsumeContext<T> context) where T: class, IEvent
        {
            T @event = context.Message;
            EventState eventState = EventState.CreateFromEvent(@event);
            _storage.Add(eventState);
            _storage.Save();
            return Task.CompletedTask;
        }        
    }
}