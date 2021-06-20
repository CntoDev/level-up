using Roster.Core.Models;
using Roster.Core.Storage;

namespace Roster.Core.Services
{
    public class ApplicationService
    {
        private IApplicationStorage _storage;

        // TODO: replace bool return with exception throwing
        public bool submitApplicationForm(ApplicationFormDto formData)
        {
            ApplicationForm form = new ApplicationForm(formData);
            if(form.status == ApplicationFormStatus.Invalid) {
                return false;
            }

            this._storage.storeApplicationForm(form);
            return true;
        }
    }
}