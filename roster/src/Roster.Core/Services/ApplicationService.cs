using Roster.Core.Domain;
using Roster.Core.Commands;
using Roster.Core.Mappings;
using Roster.Core.Storage;
using FluentResults;
using System;
using Roster.Core.Events;

namespace Roster.Core.Services
{
    public class ApplicationService
    {
        private IApplicationStorage _storage;
        private IMemberStorage _memberStorage;
        private IEventStore _eventStore;

        public ApplicationService(IApplicationStorage applicationStorage, IMemberStorage memberStorage, IEventStore eventStore)
        {
            _storage = applicationStorage;
            _memberStorage = memberStorage;
            _eventStore = eventStore;
        }

        public Result SubmitApplicationForm(ApplicationFormCommand formCommand)
        {
            var existingNicknames = _memberStorage.GetAllNicknames();

            ApplicationFormFactory factory = new ApplicationFormFactory();
            try {
                ApplicationForm form = factory.Create(existingNicknames, formCommand.Nickname, formCommand.DateOfBirth, formCommand.Email);

                form = ApplicationFormMap.Merge(form, formCommand);

                _storage.StoreApplicationForm(form);
                _eventStore.Publish<ApplicationFormSubmitted>(new ApplicationFormSubmitted(formCommand.Nickname, formCommand.Email));
                
                return Result.Ok();
            } catch(ArgumentException) {
                return Result.Fail("Application form validation failed");
            }
        }
    }
}