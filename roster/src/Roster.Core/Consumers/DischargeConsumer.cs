using System.Threading.Tasks;
using MassTransit;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

namespace Roster.Core.Consumers
{
    public class DischargeConsumer : IConsumer<DischargeRecruit>
    {
        private readonly IStorage<Member> _storage;

        public DischargeConsumer(IStorage<Member> storage)
        {
            _storage = storage;    
        }

        public Task Consume(ConsumeContext<DischargeRecruit> context)
        {
            var message = context.Message;
            var member = _storage.Find(message.Nickname);
            member.DischargeRecruit(message.Reason);
            _storage.Save();
            return Task.CompletedTask;
        }
    }
}