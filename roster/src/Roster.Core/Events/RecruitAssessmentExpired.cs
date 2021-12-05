using System;
using MassTransit;

namespace Roster.Core.Events
{
    public record RecruitAssessmentExpired(string Nickname, int Days, DateTime ScheduledForDate, Guid CorrelationId) : IScheduledEvent, CorrelatedBy<Guid>;
}