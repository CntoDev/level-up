namespace Roster.Core.Events
{
    public record MemberDischarged(string Nickname, DateTime DischargeDate, int DischargePath, bool IsAlumni, string Comment) : IEvent;
}