using Roster.Core.Domain;
using Roster.Core.Storage;
using Roster.Core.Commands;
using Roster.Core.Mappings;
using FluentResults;
using System;

namespace Roster.Core.Services
{
    public class ApplicationService
    {
        private IApplicationStorage Storage = new MemoryApplicationStorage();
        private IMemberStorage MemberStorage = new MemoryMemberStorage();

        public Result submitApplicationForm(ApplicationFormCommand formCommand)
        {
            var existingNicknames = MemberStorage.GetAllNicknames();

            ApplicationFormFactory factory = new ApplicationFormFactory();
            try {
                ApplicationForm form = factory.create(existingNicknames, formCommand.Nickname, formCommand.DateOfBirth, formCommand.Email);

                form = ApplicationFormMap.MergeCommandWithExisting(form, formCommand);

                Storage.storeApplicationForm(form);
                return Result.Ok();
            } catch(ArgumentException e) {
                return Result.Fail("Application form validation failed");
            }
        }
    }
}