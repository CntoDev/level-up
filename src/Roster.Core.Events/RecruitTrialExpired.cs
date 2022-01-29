using MassTransit;

namespace Roster.Core.Events
{
    public record RecruitTrialExpired(string Nickname, int Days, DateTime EndDate, DateTime ScheduledForDate, Guid CorrelationId) : IScheduledEvent, CorrelatedBy<Guid>;
}