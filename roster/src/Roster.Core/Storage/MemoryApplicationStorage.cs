using System.Collections.Generic;
using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    public class MemoryApplicationStorage : IApplicationStorage
    {
        private ICollection<ApplicationForm> _forms = new List<ApplicationForm>();
        public void storeApplicationForm(ApplicationForm form)
        {
            this._forms.Add(form);
        }
    }
}