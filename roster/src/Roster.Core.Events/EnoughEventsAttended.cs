namespace Roster.Core.Events
{
    public record EnoughEventsAttended(string Nickname) : IEvent;
}