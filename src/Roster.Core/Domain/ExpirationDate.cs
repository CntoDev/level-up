using System;
namespace Roster.Core.Domain;

public struct ExpirationDate
{
    public ExpirationDate(DateTime endDate)
    {
        DateTime currentDate = DateTime.UtcNow;
        EndDate = endDate;
        TimeSpan checkinWindow = TimeSpan.FromDays(7);
        TimeSpan leftoverTime = endDate - currentDate;

        if (leftoverTime >= checkinWindow)
            NextCheckinDate = currentDate.Add(checkinWindow);
        else
            NextCheckinDate = currentDate.Add(leftoverTime);
    }

    public DateTime EndDate { get; }

    public DateTime NextCheckinDate { get; }

    public bool Expired => (EndDate - DateTime.UtcNow).Days < 1;
}