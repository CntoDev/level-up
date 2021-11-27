using Roster.Core.Domain;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Storage
{
    public class DatabaseDlcStorage : Storage<Dlc>, IDlcStorage
    {
        public DatabaseDlcStorage(RosterDbContext rosterDbContext) : base(rosterDbContext)
        {
        }
    }
}