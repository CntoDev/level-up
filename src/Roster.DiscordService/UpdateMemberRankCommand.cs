namespace Roster.DiscordService
{
    public class UpdateMemberRankCommand
    {
        public string DiscordNickname { get; }

        public int? OldRank { get; }

        public int NewRank { get; }

        public UpdateMemberRankCommand(string discordNickname, int? oldRank, int newRank)
        {
            DiscordNickname = discordNickname;
            OldRank = oldRank;
            NewRank = newRank;
        }
    }
}