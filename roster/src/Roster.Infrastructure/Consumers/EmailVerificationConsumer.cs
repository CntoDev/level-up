using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using MassTransit;
using Roster.Core.Events;
using Roster.Core.Storage;
using Roster.Infrastructure.Configurations;

namespace Roster.Infrastructure.Consumers
{
    public class EmailVerificationConsumer : IConsumer<ApplicationFormAccepted>
    {
        private readonly EmailVerificationService _emailVerificationService;
        private readonly IMemberStorage _memberStorage;
        private readonly IEventStore _eventStore;

        public EmailVerificationConsumer(MailJetOptions options, IMemberStorage memberStorage, IEventStore eventStore, EmailVerificationService emailVerificationService)
        {
            _memberStorage = memberStorage;
            _emailVerificationService = emailVerificationService;
            _eventStore = eventStore;
        }

        public async Task Consume(ConsumeContext<ApplicationFormAccepted> context)
        {
            var message = context.Message;
            string verificationCode = _emailVerificationService.GenerateCode(message.Nickname);

            // save verification code to storage
            var member = _memberStorage.Find(message.Nickname);
            member.ChallengeEmail(verificationCode);
            _memberStorage.Save();

            await _emailVerificationService.SendVerificationEmail(message.Email, verificationCode);

            // publish events after checking response
            _eventStore.Publish(member.Events());
        }
    }
}