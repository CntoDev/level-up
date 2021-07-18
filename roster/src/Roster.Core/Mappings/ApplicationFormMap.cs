using Roster.Core.Domain;
using Roster.Core.Commands;

namespace Roster.Core.Mappings
{
    public static class ApplicationFormMap
    {
        public static ApplicationForm MergeCommandWithExisting(ApplicationForm form, ApplicationFormCommand command)
        {
            form.BiNickname = command.BiNickname;
            form.SteamId = command.SteamId;
            form.Gmail = command.Gmail;
            form.GithubNickname = command.GithubNickname;
            form.DiscordId = command.DiscordId;
            form.TeamspeakId = command.DiscordId;
            form.OwnedDlcs = command.OwnedDlcs;

            return form;
        }
    }
}