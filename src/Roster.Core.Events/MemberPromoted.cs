using System;

namespace Roster.Core.Events
{
    public record MemberPromoted(string Nickname,
                                 string DiscordId,
                                 int RankId,
                                 int OldRankId,
                                 DateTime PromotionDate) : IEvent;
}