using System;
using MassTransit;

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
                                DateTime JoinDate,
                                Guid CorrelationId) : IEvent, CorrelatedBy<Guid>;
}