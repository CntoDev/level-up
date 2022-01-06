using System;

namespace Roster.Core.Events
{
    public record MemberPromoted (string Nickname,
                                  int OldRankId,
                                  int RankId,
                                  DateTime PromotionDate) : IEvent;
}