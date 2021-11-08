using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<ApplicationForm> GetAll()
        {
            return _rosterDbContext.ApplicationForms.ToList();
        }

        public ApplicationForm GetByNickname(string nickname)
        {
            return _rosterDbContext.ApplicationForms.Find(nickname);
        }
    }
}