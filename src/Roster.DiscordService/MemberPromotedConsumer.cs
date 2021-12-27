using MassTransit;
using Roster.Core.Events;
using Roster.DiscordService.Configurations;

namespace Roster.DiscordService
{
    public class MemberPromotedConsumer : IConsumer<MemberPromoted>
    {
        readonly ILogger<MemberPromotedConsumer> _logger;

        readonly DiscordOptions _options;

        private DiscordService _service;

        public MemberPromotedConsumer(DiscordOptions options, ILogger<MemberPromotedConsumer> logger, DiscordService discord)
        {
            _logger = logger;
            _options = options;
            _service = discord;
        }

        public async Task Consume(ConsumeContext<MemberPromoted> context)
        {
            _logger.LogInformation("Received event {@event}", context.Message);

            UpdateMemberRankCommand updateRankCommand = new(context.Message.DiscordId, context.Message.OldRankId, context.Message.RankId);

            await _service.UpdateMemberRank(updateRankCommand);
        }
    }
}