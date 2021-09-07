using System.Threading.Tasks;
using MassTransit;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Consumers
{
    public class ApplicationFormAcceptedConsumer : IConsumer<ApplicationFormAccepted>
    {
        private readonly IMemberStorage _memberStorage;

        public ApplicationFormAcceptedConsumer(IMemberStorage memberStorage)
        {
            _memberStorage = memberStorage;
        }

        public Task Consume(ConsumeContext<ApplicationFormAccepted> context)
        {
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

            return Task.CompletedTask;
        }
    }
}