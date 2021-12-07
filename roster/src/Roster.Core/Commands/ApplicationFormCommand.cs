using System;
using System.Collections.Generic;
using Roster.Core.Domain;

namespace Roster.Core.Commands
{
    public class ApplicationFormCommand
    {
        public string Nickname { get; }
        public DateTime DateOfBirth { get; }
        public string Email { get; }
        public string BiNickname { get; init; }
        public string SteamId { get; init; }
        public string Gmail { get; init; }
        public string GithubNickname { get; init; }
        public string DiscordId { get; init; }
        public string TeamspeakId { get; init; }
        public ICollection<OwnedDlc> OwnedDlcs { get; init; }

        public ApplicationFormCommand(string nickname, DateTime dateOfBirth, string email)
        {
            Nickname = nickname;
            DateOfBirth = dateOfBirth;
            Email = email;
        }
    }
}