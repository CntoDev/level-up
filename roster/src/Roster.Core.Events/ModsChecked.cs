namespace Roster.Core.Events
{
    public record ModsChecked(string Nickname) : IEvent;
}