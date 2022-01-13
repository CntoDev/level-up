using System;

namespace Roster.Core.Events
{
    public record MemberPromoted (string Nickname,
                                  int RankId,
                                  int OldRankId,
                                  DateTime PromotionDate) : IEvent;
}