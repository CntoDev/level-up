using System;
using Roster.Core.Domain;

namespace Roster.Core.Events
{
    public record ApplicationFormAccepted : IEvent
    {
        public ApplicationFormAccepted()
        {

        }
        
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