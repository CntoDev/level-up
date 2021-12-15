namespace Roster.Core.Events
{
    public record MemberEmailVerified(string Nickname) : IEvent;
}