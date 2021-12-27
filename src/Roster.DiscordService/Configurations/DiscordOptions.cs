namespace Roster.DiscordService.Configurations
{
    public class DiscordOptions
    {
        public string BotToken { get; set; }

        public ulong GuildId { get; set; }

        public Dictionary<string, ulong> RanksMap { get; set; }
    }
}