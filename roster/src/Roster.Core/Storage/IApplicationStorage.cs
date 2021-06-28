using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    interface IApplicationStorage
    {
        public void storeApplicationForm(ApplicationForm form);
    }
}