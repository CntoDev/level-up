namespace Roster.Infrastructure.Configurations
{
    public class MailJetOptions
    {
        public string Key { get; set; }

        public string Secret { get; set; }

        public string FromEmail { get; set; }

        public string FromName { get; set; }

        public string Subject { get; set; }

        public object BaseUrl { get; set; }
    }
}