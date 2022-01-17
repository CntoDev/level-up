using System;
using System.Collections.Generic;
using Roster.Core.Domain;

namespace Roster.Core
{
    public class ApplicationFormBuilder
    {
        private ApplicationForm _applicationForm;
        private readonly ICollection<string> _existingNicknames;

        private readonly MemberNicknameFactory _nicknameFactory;

        private readonly DiscordIdFactory _discordFactory;

        public ApplicationFormBuilder(ICollection<string> existingNicknames, DiscordIdFactory discordFactory)
        {
            _existingNicknames = existingNicknames;
            _nicknameFactory = new MemberNicknameFactory();
            _discordFactory = discordFactory;
        }

        public ApplicationForm Build()
        {
            return _applicationForm;
        }

        public ApplicationFormBuilder Create(string nicknameRaw, DateTime dateOfBirth, string emailRaw)
        {
            MemberNickname nickname = MemberNicknameFactory.CreateForApplicant(_existingNicknames, nicknameRaw);
            EmailAddress email = new(emailRaw);
            DateTime utcDateOfBirth = new DateTime(dateOfBirth.Ticks, DateTimeKind.Utc);
            _applicationForm = new ApplicationForm(nickname, utcDateOfBirth , email);

            return this;
        }

        public ApplicationFormBuilder SetBiNickname(string binickname)
        {
            _applicationForm.BiNickname = binickname;

            return this;
        }

        public ApplicationFormBuilder SetSteamId(string steamid)
        {
            _applicationForm.SteamId = steamid;

            return this;
        }

        public ApplicationFormBuilder SetGmailAddress(string address)
        {
            _applicationForm.Gmail = new EmailAddress(address);

            return this;
        }

        public ApplicationFormBuilder SetGithubNikcname(string ghnickname)
        {
            _applicationForm.GithubNickname = ghnickname;

            return this;
        }

        public ApplicationFormBuilder SetDiscordId(string discordid)
        {
            _applicationForm.DiscordId = _discordFactory.Create(discordid);

            return this;
        }

        public ApplicationFormBuilder SetTeamspeakId(string teamspeakid)
        {
            _applicationForm.TeamspeakId = teamspeakid;

            return this;
        }

        public ApplicationFormBuilder SetOwnedDlcs(ICollection<OwnedDlc> dlcs)
        {
            _applicationForm.OwnedDlcs = dlcs;

            return this;
        }

        public ApplicationFormBuilder SetPreferredPronouns(int preferredPronouns)
        {
            _applicationForm.PreferredPronouns = (Pronoun)preferredPronouns;
            return this;
        }

        public ApplicationFormBuilder SetTimeZone(string timeZone)
        {
            _applicationForm.TimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            return this;
        }

        public ApplicationFormBuilder SetLanguageSkillLevel(int languageSkillLevel)
        {
            _applicationForm.LanguageSkillLevel = (LanguageSkillLevel)languageSkillLevel;
            return this;
        }

        public ApplicationFormBuilder SetPreviousArmaExperience(string previousArmaExperience)
        {
            _applicationForm.PreviousArmaExperience = previousArmaExperience;
            return this;
        }

        public ApplicationFormBuilder SetPreviousArmaModExperience(string previousArmaModExperience)
        {
            _applicationForm.PreviousArmaModExperience = previousArmaModExperience;
            return this;
        }

        public ApplicationFormBuilder SetDesiredCommunityRole(string desiredCommunityRole)
        {
            _applicationForm.DesiredCommunityRole = desiredCommunityRole;
            return this;
        }

        public ApplicationFormBuilder SetAboutYourself(string aboutYourself)
        {
            _applicationForm.AboutYourself = aboutYourself;
            return this;
        }
    }
}