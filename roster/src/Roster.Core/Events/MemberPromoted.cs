using System;

namespace Roster.Core.Events
{
    public record MemberPromoted (string Nickname,
                                  int RankId,
                                  DateTime PromotionDate) : IEvent;
}