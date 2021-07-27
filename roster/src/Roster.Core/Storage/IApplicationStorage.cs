using Roster.Core.Domain;

namespace Roster.Core.Storage
{
    public interface IApplicationStorage
    {
        public void StoreApplicationForm(ApplicationForm form);
    }
}