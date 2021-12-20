namespace Roster.Core.Events
{
    public record BootcampCompleted(string Nickname) : IEvent;
}