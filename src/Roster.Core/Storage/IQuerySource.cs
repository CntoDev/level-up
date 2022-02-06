using System.Linq;
using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    public interface IQuerySource
    {
        IQueryable<ApplicationForm> ApplicationForms { get; }
        IQueryable<Member> Members { get; }
        IQueryable<Rank> Ranks { get; }
        IQueryable<Dlc> Dlcs { get; }
        IQueryable<EventState> EventStates { get; }
        IQueryable<Warning> Warnings { get; }
    }
}