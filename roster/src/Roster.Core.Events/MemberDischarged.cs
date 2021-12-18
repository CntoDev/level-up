namespace Roster.Core.Events
{
    public record MemberDischarged(string Nickname, DateTime DischargeDate, int DischargePath, string Comment) : IEvent;
}