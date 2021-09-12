namespace Roster.Core.Domain
{
    public class DiscordId
    {
        public string Id { get; private set; }

        internal DiscordId(string discordId)
        {
            Id = discordId;
        }
    }
}