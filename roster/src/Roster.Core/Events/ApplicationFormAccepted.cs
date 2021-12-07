using System;
using Roster.Core.Domain;

namespace Roster.Core.Events
{
    public record ApplicationFormAccepted : IEvent
    {
        public ApplicationFormAccepted(ApplicationForm applicationForm) 
        {
            Nickname = applicationForm.Nickname.Nickname;
            DateOfBirth = applicationForm.DateOfBirth;
            Email = applicationForm.Email.Email;
            BiNickname = applicationForm.BiNickname;
            SteamId = applicationForm.SteamId;
            Gmail = applicationForm.Gmail?.Email;
            GithubNickname = applicationForm.GithubNickname;
            DiscordId = applicationForm.DiscordId?.Id;
            TeamspeakId = applicationForm.TeamspeakId;            
        }

        public string Nickname { get; }

        public DateTime DateOfBirth { get; }

        public string Email { get; }

        public string BiNickname { get; }

        public string SteamId { get; }

        public string Gmail { get; }

        public string GithubNickname { get; }

        public string DiscordId { get; }

        public string TeamspeakId { get; }
    }
}