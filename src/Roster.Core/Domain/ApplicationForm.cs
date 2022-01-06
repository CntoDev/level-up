using System;
using System.Collections.Generic;
using Roster.Core.Events;

namespace Roster.Core.Domain
{
    public class ApplicationForm : AggregateRoot
    {
        public MemberNickname Nickname { get; }
        public DateTime DateOfBirth { get; }
        public EmailAddress Email { get; }
        public string BiNickname { get; set; }
        public string SteamId { get; set; }
        public EmailAddress Gmail { get; set; }
        public string GithubNickname { get; set; }
        public DiscordId DiscordId { get; set; }
        public string TeamspeakId { get; set; }
        public Pronoun PreferredPronouns { get; set; }
        public TimeZoneInfo TimeZone { get; set; }
        public LanguageSkillLevel LanguageSkillLevel { get; set; }
        public string PreviousArmaExperience { get; set; }
        public string PreviousArmaModExperience { get; set; }
        public ICollection<OwnedDlc> OwnedDlcs { get; set; }
        public bool? Accepted { get; private set; }
        public string InterviewerComment { get; private set; }
        public bool Processed => Accepted.HasValue;

        internal ApplicationForm(MemberNickname nickname, DateTime dateOfBirth, EmailAddress email)
        {
            Nickname = nickname;
            DateOfBirth = dateOfBirth;
            Email = email;
        }

        private ApplicationForm() { }

        internal void Accept()
        {
            if (Processed)
                return;

            Accepted = true;
            Publish(BuildEvent(this));
        }

        internal void Reject(string comment)
        {
            if (Processed)
                return;
            
            Accepted = false;
            InterviewerComment = comment;
            Publish(new ApplicationFormRejected(Nickname.ToString(), Email.ToString(), comment));
        }

        public static ApplicationFormAccepted BuildEvent(ApplicationForm applicationForm)
        {
            return new ApplicationFormAccepted(
                applicationForm.Nickname.Nickname,
                applicationForm.DateOfBirth,
                applicationForm.Email.Email,
                applicationForm.BiNickname,
                applicationForm.SteamId,
                applicationForm.Gmail?.Email,
                applicationForm.GithubNickname,
                applicationForm.DiscordId?.Id,
                applicationForm.TeamspeakId
            );
        }
    }
}