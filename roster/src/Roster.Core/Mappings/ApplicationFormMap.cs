using Roster.Core.Domain;
using Roster.Core.Commands;

namespace Roster.Core.Mappings
{
    public static class ApplicationFormMap
    {
        public static ApplicationForm FromApplicationFormCommand(ApplicationFormCommand command)
        {
            var applicationForm = new ApplicationForm(command.Nickname, command.DateOfBirth, command.Email);
            applicationForm.BiNickname = command.BiNickname;
            applicationForm.SteamId = command.SteamId;
            applicationForm.Gmail = command.Gmail;
            applicationForm.GithubNickname = command.GithubNickname;
            applicationForm.DiscordId = command.DiscordId;
            applicationForm.TeamspeakId = command.DiscordId;
            applicationForm.OwnedDlcs = command.OwnedDlcs;

            return applicationForm;
        }
    }
}