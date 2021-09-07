using System;
using Roster.Core.Domain;

namespace Roster.Core.Events
{
    public record ApplicationFormAccepted : IEvent
    {
        public ApplicationFormAccepted(ApplicationForm applicationForm) 
        {
            Nickname = applicationForm.Nickname;
            DateOfBirth = applicationForm.DateOfBirth;
            Email = applicationForm.Email;
            BiNickname = applicationForm.BiNickname;
            SteamId = applicationForm.SteamId;
            Gmail = applicationForm.Gmail;
            GithubNickname = applicationForm.GithubNickname;
            DiscordId = applicationForm.DiscordId;
            TeamspeakId = applicationForm.TeamspeakId;            
        }

        public string Nickname { get; init; }

        public DateTime DateOfBirth { get; init; }

        public string Email { get; init; }

        public string BiNickname { get; init; }

        public string SteamId { get; init; }

        public string Gmail { get; init; }

        public string GithubNickname { get; init; }

        public string DiscordId { get; init; }

        public string TeamspeakId { get; init; }
    }
}