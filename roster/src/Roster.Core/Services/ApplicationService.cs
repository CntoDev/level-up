using Roster.Core.Domain;
using Roster.Core.Storage;
using Roster.Core.Commands;
using Roster.Core.Mappings;

namespace Roster.Core.Services
{
    public class ApplicationService
    {
        private IApplicationStorage Storage = new MemoryApplicationStorage();

        // TODO: replace bool return with exception throwing
        public bool submitApplicationForm(ApplicationFormCommand formCommand)
        {
            ApplicationForm form = ApplicationFormMap.FromApplicationFormCommand(formCommand);
            if(form.status == ApplicationFormStatus.Invalid) {
                return false;
            }

            Storage.storeApplicationForm(form);
            return true;
        }
    }
}