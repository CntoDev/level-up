using System.Net.Mail;

namespace Roster.Core.Domain
{
    public class EmailAddress
    {
        public string Email { get; }

        public EmailAddress(string email)
        {
            EmailAddress.Validate(email);

            Email = email;
        }

        // Validate email address format.
        private static void Validate(string email)
        {
            if (!string.IsNullOrEmpty(email))
                _ = new MailAddress(email);
        }

        public override string ToString() => Email;
    }
}