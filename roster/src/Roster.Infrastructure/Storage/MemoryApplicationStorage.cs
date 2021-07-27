using System.Collections.Generic;
using Roster.Core.Domain;
using Roster.Core.Storage;

namespace Roster.Infrastructure.Storage
{
    public class MemoryApplicationStorage : IApplicationStorage
    {
        private ICollection<ApplicationForm> _forms = new List<ApplicationForm>();
        public void StoreApplicationForm(ApplicationForm form)
        {
            this._forms.Add(form);
        }
    }
}