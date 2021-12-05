using System;

namespace Roster.Core.Events
{
    public interface IScheduledEvent : IEvent {
        DateTime ScheduledForDate { get; }
    }
}