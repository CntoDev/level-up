using Roster.Core.Domain;
using Roster.Core.Commands;
using Roster.Core.Storage;
using FluentResults;
using System;
using Roster.Core.Events;

namespace Roster.Core.Services
{
    public class ApplicationFormService
    {
        private IApplicationStorage _storage;
        private IMemberStorage _memberStorage;
        private IEventStore _eventStore;

        private DiscordIdFactory _discordFactory;

        public ApplicationFormService(IApplicationStorage applicationStorage, IMemberStorage memberStorage, IEventStore eventStore, IDiscordValidationService discordValidator)
        {
            _storage = applicationStorage;
            _memberStorage = memberStorage;
            _eventStore = eventStore;
            _discordFactory = new DiscordIdFactory(discordValidator);
        }

        public Result SubmitApplicationForm(ApplicationFormCommand formCommand)
        {
            var existingNicknames = _memberStorage.GetAllNicknames();

            ApplicationFormBuilder formBuilder = new ApplicationFormBuilder(existingNicknames, _discordFactory);
            try {
                ApplicationForm form = formBuilder.Create(formCommand.Nickname, formCommand.DateOfBirth, formCommand.Email)
                    .SetBiNickname(formCommand.BiNickname)
                    .SetSteamId(formCommand.SteamId)
                    .SetGmailAddress(formCommand.Gmail)
                    .SetGithubNikcname(formCommand.GithubNickname)
                    .SetDiscordId(formCommand.DiscordId)
                    .SetTeamspeakId(formCommand.TeamspeakId)
                    .SetOwnedDlcs(formCommand.OwnedDlcs)
                    .Build();

                _storage.StoreApplicationForm(form);
                _eventStore.Publish<ApplicationFormSubmitted>(new ApplicationFormSubmitted(formCommand.Nickname, formCommand.Email));
                
                return Result.Ok();
            } catch(ArgumentException) {
                return Result.Fail("Application form validation failed");
            }
        }
    }
}