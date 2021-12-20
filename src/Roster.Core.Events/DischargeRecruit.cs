namespace Roster.Core.Events
{
    public record DischargeRecruit(string Nickname, string Reason) : IEvent;
}