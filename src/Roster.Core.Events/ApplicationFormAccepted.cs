namespace Roster.Core.Events
{
    public record ApplicationFormAccepted : IEvent
    {
        public ApplicationFormAccepted(string nickname,
                                       DateTime dateOfBirth,
                                       string email,
                                       string biNickname,
                                       string steamId,
                                       string gmail,
                                       string githubNickname,
                                       string discordId,
                                       string teamspeakId) 
        {
            Nickname = nickname;
            DateOfBirth = dateOfBirth;
            Email = email;
            BiNickname = biNickname;
            SteamId = steamId;
            Gmail = gmail;
            GithubNickname = githubNickname;
            DiscordId = discordId;
            TeamspeakId = teamspeakId;            
        }

        public string Nickname { get; }

        public DateTime DateOfBirth { get; }

        public string Email { get; }

        public string BiNickname { get; }

        public string SteamId { get; }

        public string Gmail { get; }

        public string GithubNickname { get; }

        public string DiscordId { get; }

        public string TeamspeakId { get; }
    }
}