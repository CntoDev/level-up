using System;
using System.Collections.Generic;
using Roster.Core.Events;

namespace Roster.Core.Domain
{
    public class ApplicationForm : AggregateRoot
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
        public bool Accepted { get; private set; }
        public string InterviewerComment { get; private set; }

        internal ApplicationForm(MemberNickname nickname, DateTime dateOfBirth, EmailAddress email)
        {
            Nickname = nickname;
            DateOfBirth = dateOfBirth;
            Email = email;
        }

        private ApplicationForm() {}

        internal void Accept()
        {
            Accepted = true;
            Publish(new ApplicationFormAccepted(this));
        }

        internal void Reject(string comment)
        {
            Accepted = false;
            InterviewerComment = comment;
            Publish(new ApplicationFormRejected(Nickname.ToString(), Email.ToString(), comment));
        }
    }
}