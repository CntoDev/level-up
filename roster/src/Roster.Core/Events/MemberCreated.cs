using System;

namespace Roster.Core.Events
{
    public record MemberCreated(string Nickname,
                                string Email,
                                string BiNickname,
                                DateTime DateOfBirth,
                                string DiscordId,
                                string GithubNickname,
                                string Gmail,
                                string SteamId,
                                string TeamspeakId,
                                DateTime JoinDate) : IEvent;
}