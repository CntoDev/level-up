namespace Roster.Core.Events
{
    public record RecruitDischarged(string Nickname) : IEvent;
}