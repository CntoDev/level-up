namespace Roster.Core.Events
{
    public record RecruitPromoted(string Nickname) : IEvent;
}