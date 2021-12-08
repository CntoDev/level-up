using System.Threading.Tasks;
using MassTransit;
using Roster.Core.Domain;
using Roster.Core.Events;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Consumers
{
    public class EmailSender : IConsumer<MemberCreated>, IConsumer<ApplicationFormRejected>, IConsumer<ApplicationFormSubmitted>
    {
        private readonly EmailService _emailService;
        private readonly IStorage<Member> _memberStorage;
        private readonly IEventStore _eventStore;

        public EmailSender(IStorage<Member> memberStorage, IEventStore eventStore, EmailService emailVerificationService)
        {
            _memberStorage = memberStorage;
            _emailService = emailVerificationService;
            _eventStore = eventStore;
        }

        public async Task Consume(ConsumeContext<MemberCreated> context)
        {
            var message = context.Message;
            string verificationCode = EmailService.GenerateCode();

            // save verification code to storage
            var member = _memberStorage.Find(message.Nickname);
            member.ChallengeEmail(verificationCode);
            _memberStorage.Save();

            await _emailService.SendVerificationEmail(message.Email, verificationCode);

            // publish events after checking response
            _eventStore.Publish(member.Events());
        }

        public async Task Consume(ConsumeContext<ApplicationFormRejected> context)
        {
            var message = context.Message;
            await _emailService.SendRejectionEmail(message.Nickname, message.Email, message.Reason);
        }

        public async Task Consume(ConsumeContext<ApplicationFormSubmitted> context)
        {
            var message = context.Message;
            await _emailService.SendApplicationConfirmation(message.Nickname, message.Email);
        }
    }
}