using System.Threading.Tasks;
using MassTransit;
using Roster.Core.Events;
using Roster.Core.Services;

namespace Roster.Core.Consumers
{
    public class MemberCreationConsumer : IConsumer<ApplicationFormAccepted>
    {
        private readonly MemberService _service;
        public MemberCreationConsumer(MemberService service)
        {
            _service = service;
        }

        public Task Consume(ConsumeContext<ApplicationFormAccepted> context)
        {
            var message = context.Message;
            _service.AcceptMember(message);
            return Task.CompletedTask;
        }
    }
}