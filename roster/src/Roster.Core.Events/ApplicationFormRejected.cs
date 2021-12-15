namespace Roster.Core.Events
{
    public record ApplicationFormRejected(string Nickname, string Email, string Reason) : IEvent;
}