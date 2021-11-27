using System;
using System.Collections.Generic;
using Roster.Core.Domain;

namespace Roster.Core.Commands
{
    public class ApplicationFormCommand
    {
        public string Nickname { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Email { get; private set; }
        public string BiNickname { get; set; }
        public string SteamId { get; set; }
        public string Gmail { get; set; }
        public string GithubNickname { get; set; }
        public string DiscordId { get; set; }
        public string TeamspeakId { get; set; }
        public ICollection<OwnedDlc> OwnedDlcs { get; set; }

        public ApplicationFormCommand(string nickname, DateTime dateOfBirth, string email)
        {
            Nickname = nickname;
            DateOfBirth = dateOfBirth;
            Email = email;
        }
    }
}