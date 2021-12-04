using Roster.Core.Domain;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Storage
{
    public class DatabaseEventStateStorage : Storage<EventState>, IEventStateStorage
    {
        public DatabaseEventStateStorage(RosterDbContext rosterDbContext) : base(rosterDbContext)
        {
        }
    }
}