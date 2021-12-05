using System.Linq;
using Roster.Core.Sagas;

namespace Roster.Core.Storage
{
    public interface IProcessSource
    {
        IQueryable<RecruitmentSaga> RecruitmentSagas { get; }
    }
}