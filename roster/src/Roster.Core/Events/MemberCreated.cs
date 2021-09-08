namespace Roster.Core.Events
{
    public record MemberCreated(string Nickname, string Email) : IEvent;
}