using System;
using System.Collections.Generic;

namespace Roster.Core.Domain
{
    public class ApplicationForm
    {
        public MemberNickname Nickname { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public EmailAddress Email { get; private set; }
        public string BiNickname { get; set; }
        public string SteamId { get; set; }
        public EmailAddress Gmail { get; set; }
        public string GithubNickname { get; set; }
        public DiscordId DiscordId { get; set; }
        public string TeamspeakId { get; set; }
        public ICollection<Arma3Dlc> OwnedDlcs { get; set; }

        internal ApplicationForm(MemberNickname nickname, DateTime dateOfBirth, EmailAddress email)
        {
            Nickname = nickname;
            DateOfBirth = dateOfBirth;
            Email = email;
        }

        private ApplicationForm() {}
    }
}