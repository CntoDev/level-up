using System.Threading.Tasks;
using MassTransit;
using Roster.Core.Commands;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Services;

namespace Roster.Core.Consumers
{
    public class PromotionConsumer : IConsumer<RecruitPromoted>
    {
        private readonly MemberService _memberService;

        public PromotionConsumer(MemberService memberService)
        {
            _memberService = memberService;
        }

        public Task Consume(ConsumeContext<RecruitPromoted> context)
        {
            var message = context.Message;
            _memberService.PromoteMember(new PromoteMemberCommand(message.Nickname, RankId.Reservist));
            return Task.CompletedTask;
        }
    }
}