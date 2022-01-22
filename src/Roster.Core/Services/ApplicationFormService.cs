using Roster.Core.Domain;
using Roster.Core.Commands;
using Roster.Core.Storage;
using FluentResults;
using System;
using Roster.Core.Events;
using System.Linq;

namespace Roster.Core.Services
{
    public class ApplicationFormService
    {
        private readonly IQuerySource _querySource;
        private readonly IStorage<ApplicationForm> _storage;
        private readonly IEventStore _eventStore;

        private readonly DiscordIdFactory _discordFactory;

        public ApplicationFormService(IQuerySource querySource,
                                      IStorage<ApplicationForm> storage,
                                      IEventStore eventStore,
                                      IDiscordValidationService discordValidator)
        {
            _querySource = querySource;
            _storage = storage;
            _eventStore = eventStore;
            _discordFactory = new DiscordIdFactory(discordValidator);
        }

        public Result SubmitApplicationForm(ApplicationFormCommand formCommand)
        {
            var existingNicknames = _querySource.Members.Select(m => m.Nickname).ToList();
            ApplicationFormBuilder formBuilder = new(existingNicknames, _discordFactory);

            try
            {
                ApplicationForm form = formBuilder.Create(formCommand.Nickname, formCommand.DateOfBirth, formCommand.Email)
                    .SetBiNickname(formCommand.BiNickname)
                    .SetSteamId(formCommand.SteamId)
                    .SetGmailAddress(formCommand.Gmail)
                    .SetGithubNikcname(formCommand.GithubNickname)
                    .SetDiscordId(formCommand.DiscordId)
                    .SetTeamspeakId(formCommand.TeamspeakId)
                    .SetOwnedDlcs(formCommand.OwnedDlcs)
                    .SetPreferredPronouns(formCommand.PreferredPronouns)
                    .SetTimeZone(formCommand.TimeZone)
                    .SetLanguageSkillLevel(formCommand.LanguageSkillLevel)
                    .SetPreviousArmaExperience(formCommand.PreviousArmaExperience)
                    .SetPreviousArmaModExperience(formCommand.PreviousArmaModExperience)
                    .SetDesiredCommunityRole(formCommand.DesiredCommunityRole)
                    .SetAboutYourself(formCommand.AboutYourself)
                    .Build();

                _storage.Add(form);
                _storage.Save();
                _eventStore.PublishAsync(new ApplicationFormSubmitted(formCommand.Nickname, formCommand.Email));

                return Result.Ok();
            }
            catch (ArgumentException ex)
            {
                return Result.Fail($"Application form validation failed. {ex.Message}.");
            }
        }

        public void AcceptApplicationForm(MemberNickname nickname)
        {
            ApplicationForm applicationForm = _storage.Find(nickname);
            applicationForm.Accept();
            _storage.Save();
            _eventStore.Publish(applicationForm.Events());
        }

        public void RejectApplicationForm(MemberNickname nickname, string comment)
        {
            ApplicationForm applicationForm = _storage.Find(nickname);
            applicationForm.Reject(comment);
            _storage.Save();
            _eventStore.Publish(applicationForm.Events());
        }        
    }
}