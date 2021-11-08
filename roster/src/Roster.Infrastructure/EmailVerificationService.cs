using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Logging;
using Roster.Core.Events;
using Roster.Infrastructure.Configurations;

namespace Roster.Infrastructure
{
    public class EmailVerificationService
    {
        private readonly MailJetOptions _options;
        private readonly MailjetClient _client;
        private readonly ILogger<EmailVerificationService> _logger;

        public EmailVerificationService(MailJetOptions options, ILogger<EmailVerificationService> logger)
        {
            _options = options;
            _client = new MailjetClient(_options.Key, _options.Secret);
            _logger = logger;
        }

        internal String GenerateCode(string nickname)
        {
            Random random = new Random();
            return random.Next(int.MaxValue).ToString();
        }

        public async Task SendVerificationEmail(string emailAddress, string verificationCode)
        {
            string link = $"{_options.BaseUrl}/Roster/Member/Verify/{emailAddress}/{verificationCode}";

            MailjetRequest request = new MailjetRequest()
            {
                Resource = Send.Resource
            };

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_options.FromEmail, _options.FromName))
                .WithSubject(_options.Subject)
                .WithHtmlPart($"Please verify your e-mail provided in CNTO application form by clicking the <a href='{link}'>link</a>.")
                .WithTo(new SendContact(emailAddress))
                .Build();

            var response = await _client.SendTransactionalEmailAsync(email);

            if (response.Messages != null && response.Messages.Count() == 1)
            {
                _logger.LogInformation("Verification e-mail successfully sent to {email}. Response was {@response}.", emailAddress, response);
                return;
            }

            _logger.LogError("Failure sending mail to {email} with response - {@response}", emailAddress, response);
            throw new Exception("Failure sending mail.");
        }
    }
}