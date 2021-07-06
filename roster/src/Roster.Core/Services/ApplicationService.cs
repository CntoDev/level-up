using Roster.Core.Domain;
using Roster.Core.Storage;
using Roster.Core.Commands;
using Roster.Core.Mappings;
using FluentResults;

namespace Roster.Core.Services
{
    public class ApplicationService
    {
        private IApplicationStorage Storage = new MemoryApplicationStorage();

        // TODO: replace bool return with exception throwing
        public Result submitApplicationForm(ApplicationFormCommand formCommand)
        {
            ApplicationForm form = ApplicationFormMap.FromApplicationFormCommand(formCommand);
            if(form.status == ApplicationFormStatus.Invalid) {
                return Result.Fail("Application form validation failed");
            }

            Storage.storeApplicationForm(form);
            return Result.Ok();
        }
    }
}