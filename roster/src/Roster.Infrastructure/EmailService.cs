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
    public class EmailService
    {
        private readonly MailJetOptions _options;
        private readonly MailjetClient _client;
        private readonly ILogger<EmailService> _logger;

        public EmailService(MailJetOptions options, ILogger<EmailService> logger)
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
        }

        public async Task SendApplicationConfirmation(string nickname, string emailAddress)
        {
            MailjetRequest request = new MailjetRequest()
            {
                Resource = Send.Resource
            };

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_options.FromEmail, _options.FromName))
                .WithSubject("Application received")
                .WithHtmlPart($"Dear {nickname}!<br /><br />Your application to join Carpe Noctem Tactical Operations has been received. An interviewer will contact you as soon as possible.<br /><br />Carpe Noctem Tactical Operations")
                .WithTo(new SendContact(emailAddress))
                .Build();

            var response = await _client.SendTransactionalEmailAsync(email);            
            
            if (response.Messages != null && response.Messages.Count() == 1)
            {
                _logger.LogInformation("Application submission e-mail successfully sent to {email}. Response was {@response}.", emailAddress, response);
                return;
            }

            _logger.LogError("Failure sending mail to {email} with response - {@response}", emailAddress, response);
        }

        public async Task SendRejectionEmail(string nickname, string emailAddress, string reason)
        {
            MailjetRequest request = new MailjetRequest()
            {
                Resource = Send.Resource
            };

            var email = new TransactionalEmailBuilder()
                .WithFrom(new SendContact(_options.FromEmail, _options.FromName))
                .WithSubject("Application rejected")
                .WithHtmlPart($"Dear {nickname}!<br /><br />Unfortunately, your application to join Carpe Noctem Tactical Operations has been rejected. Reason: {reason}<br /><br />Carpe Noctem Tactical Operations")
                .WithTo(new SendContact(emailAddress))
                .Build();

            var response = await _client.SendTransactionalEmailAsync(email);            
            
            if (response.Messages != null && response.Messages.Count() == 1)
            {
                _logger.LogInformation("Rejection e-mail successfully sent to {email}. Response was {@response}.", emailAddress, response);
                return;
            }

            _logger.LogError("Failure sending mail to {email} with response - {@response}", emailAddress, response);
        }
    }
}