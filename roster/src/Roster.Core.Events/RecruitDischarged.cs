namespace Roster.Core.Events
{
    public record RecruitDischarged(string Nickname, string Reason) : IEvent;
}