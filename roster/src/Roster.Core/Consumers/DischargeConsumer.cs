using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

namespace Roster.Core.Consumers
{
    public class DischargeConsumer : IConsumer<DischargeRecruit>
    {
        private readonly IStorage<Member> _storage;
        private readonly ILogger<DischargeConsumer> _logger;

        public DischargeConsumer(IStorage<Member> storage, ILogger<DischargeConsumer> logger)
        {
            _storage = storage;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<DischargeRecruit> context)
        {
            var message = context.Message;
            _logger.LogInformation("Discharging recruit {nickname}.", message.Nickname);
            var member = _storage.Find(message.Nickname);
            member.DischargeRecruit(message.Reason);
            _storage.Save();
            return Task.CompletedTask;
        }
    }
}