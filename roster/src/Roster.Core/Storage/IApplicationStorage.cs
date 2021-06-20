using Roster.Core.Models;

namespace Roster.Core.Storage
{
    interface IApplicationStorage
    {
        public void storeApplicationForm(ApplicationForm form);
    }
}