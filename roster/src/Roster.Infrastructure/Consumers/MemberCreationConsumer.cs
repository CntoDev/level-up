using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Consumers
{
    public class MemberCreationConsumer : IConsumer<ApplicationFormAccepted>
    {
        private readonly IMemberStorage _memberStorage;
        private readonly IEventStore _eventStore;
        private readonly ILogger<MemberCreationConsumer> _logger;

        public MemberCreationConsumer(IMemberStorage memberStorage, IEventStore eventStore, ILogger<MemberCreationConsumer> logger)
        {
            _memberStorage = memberStorage;
            _eventStore = eventStore;
            _logger = logger;
        }

        public Task Consume(ConsumeContext<ApplicationFormAccepted> context)
        {
            _logger.LogInformation("Consuming ApplicationFormAccepted {@event}", context.Message);
            var message = context.Message;

            Member member = new Member(message.Nickname, message.Email)
            {
                BiNickname = message.BiNickname,
                DateOfBirth = message.DateOfBirth,
                DiscordId = message.DiscordId,
                GithubNickname = message.GithubNickname,
                Gmail = message.Gmail,
                SteamId = message.SteamId,
                TeamspeakId = message.TeamspeakId
            };

            _memberStorage.Add(member);
            _memberStorage.Save();
            _eventStore.Publish(new MemberCreated(message.Nickname, message.Email));

            return Task.CompletedTask;
        }
    }
}