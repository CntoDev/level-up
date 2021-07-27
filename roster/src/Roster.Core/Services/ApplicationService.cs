using Roster.Core.Domain;
using Roster.Core.Commands;
using Roster.Core.Mappings;
using Roster.Core.Storage;
using FluentResults;
using System;

namespace Roster.Core.Services
{
    public class ApplicationService
    {
        private IApplicationStorage _storage;
        private IMemberStorage _memberStorage;

        public Result SubmitApplicationForm(ApplicationFormCommand formCommand)
        {
            var existingNicknames = _memberStorage.GetAllNicknames();

            ApplicationFormFactory factory = new ApplicationFormFactory();
            try {
                ApplicationForm form = factory.Create(existingNicknames, formCommand.Nickname, formCommand.DateOfBirth, formCommand.Email);

                form = ApplicationFormMap.Merge(form, formCommand);

                _storage.StoreApplicationForm(form);
                return Result.Ok();
            } catch(ArgumentException) {
                return Result.Fail("Application form validation failed");
            }
        }
    }
}