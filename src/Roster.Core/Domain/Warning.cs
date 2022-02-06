using System;
using Roster.Core.Events;

namespace Roster.Core.Domain;

public class Warning : AggregateRoot
{
    readonly long _id;

    public Warning(ApplicationFormSubmitted message) : this(typeof(ApplicationFormSubmitted))
    {        
        Message = $"New Application has been submitted by {message.Nickname}.";
    }

    public Warning(EnoughEventsAttended message) : this(typeof(EnoughEventsAttended))
    {
        Message = $"Recruit {message.Nickname} has attended enough events to be promoted.";
    }

    public Warning(RecruitAssessmentExpired message): this(typeof(RecruitAssessmentExpired))
    {
        Message = $"Recruit {message.Nickname} did not pass bootcamp and mod assessment on time.";
    }

    public Warning(RecruitTrialExpired message) : this(typeof(RecruitTrialExpired))
    {
        Message = $"Recruit {message.Nickname} did not pass recruitment trial on time.";
    }

    private Warning(Type type)
    {
        Type = type.Name;
        UtcTime = DateTime.UtcNow;
    }

    private Warning(){}

    public WarningId Id => new WarningId(_id);

    public string Type { get; }

    public string Message { get; }

    public DateTime UtcTime { get; }
}