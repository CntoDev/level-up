namespace Roster.Core.Events
{
    public record AutomaticDischargeToggled (string Nickname) : IEvent;
}