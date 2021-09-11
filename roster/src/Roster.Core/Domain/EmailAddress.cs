using System.Net.Mail;

namespace Roster.Core.Domain
{
    public class EmailAddress
    {
        public string Email { get; private set;}

        internal EmailAddress(string email)
        {
            EmailAddress.Validate(email);

            Email = email;
        }

        // Validate email address format.
        public static bool Validate(string email)
        {
            new MailAddress(email);
                
            return true;
        }
    }
}