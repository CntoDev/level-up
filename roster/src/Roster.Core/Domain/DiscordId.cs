namespace Roster.Core.Domain
{
    public class DiscordId
    {
        public string Id { get; private set; }

        public DiscordId(string id)
        {
            Id = id;
        }
    }
}