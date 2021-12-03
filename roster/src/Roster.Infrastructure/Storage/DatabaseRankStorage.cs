using Roster.Core.Domain;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Storage
{
    public class DatabaseRankStorage: Storage<Rank>, IRankStorage
    {
        public DatabaseRankStorage(RosterDbContext rosterDbContext) : base(rosterDbContext) {}
    }
}