namespace Roster.Core.Domain
{
    public class DiscordId
    {
        public string Id { get; }

        public DiscordId(string id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id;
        }
    }
}