using Roster.Core.Domain;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Storage
{
    public class DatabaseApplicationStorage : IApplicationStorage
    {
        private readonly RosterDbContext _rosterDbContext;

        public DatabaseApplicationStorage(RosterDbContext rosterDbContext)
        {
            _rosterDbContext = rosterDbContext;
        }

        public void StoreApplicationForm(ApplicationForm form)
        {
            _rosterDbContext.ApplicationForms.Add(form);
            _rosterDbContext.SaveChanges();
        }
    }
}