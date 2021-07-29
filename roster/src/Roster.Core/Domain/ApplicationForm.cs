using System;
using System.Collections.Generic;

namespace Roster.Core.Domain
{
    public class ApplicationForm
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
        public ICollection<Arma3Dlc> OwnedDlcs { get; set; }

        public ApplicationFormStatus status { get; private set; }

        internal ApplicationForm(string nickname, DateTime dateOfBirth, string email)
        {
            Nickname = nickname;
            DateOfBirth = dateOfBirth;
            Email = email;

            if(this.Validate()) {
                this.status = ApplicationFormStatus.Pending;
            } else {
                this.status = ApplicationFormStatus.Invalid;
            }
        }

        // TODO: implement validation logic
        public bool Validate()
        {
            return true;
        }
    }
}