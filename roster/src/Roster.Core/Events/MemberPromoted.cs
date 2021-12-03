namespace Roster.Core.Events
{
    public record MemberPromoted (string Nickname, int RankId) : IEvent;
}