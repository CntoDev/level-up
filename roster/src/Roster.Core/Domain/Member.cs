using System;
using Roster.Core.Events;

namespace Roster.Core.Domain
{
    public class Member : AggregateRoot
    {
        public Member(string nickname, string email)
        {
            Nickname = nickname;
            Email = email;
        }

        public string Nickname { get; }

        public DateTime DateOfBirth { get; set; }

        public string Email { get; }

        public string BiNickname { get; set; }

        public string SteamId { get; set; }

        public string Gmail { get; set; }

        public string GithubNickname { get; set; }

        public string DiscordId { get; set; }

        public string TeamspeakId { get; set; }
    }
}