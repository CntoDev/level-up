using System;
using System.Collections.Generic;
using Roster.Core.Domain;

namespace Roster.Core.Commands
{
    public class ApplicationFormCommand
    {
        public string Nickname { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string Email { get; init; }
        public string BiNickname { get; init; }
        public string SteamId { get; init; }
        public string Gmail { get; init; }
        public string GithubNickname { get; init; }
        public string DiscordId { get; init; }
        public string TeamspeakId { get; init; }
        public int PreferredPronouns { get; init; }
        public string TimeZone { get; init; }
        public int LanguageSkillLevel { get; init; }
        public string PreviousArmaExperience { get; init; }
        public string PreviousArmaModExperience { get; init; }
        public string DesiredCommunityRole { get; init; }
        public string AboutYourself { get; init; }
        public ICollection<OwnedDlc> OwnedDlcs { get; init; }
    }
}