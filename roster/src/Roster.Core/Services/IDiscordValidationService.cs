using Roster.Core.Domain;

namespace Roster.Core.Services
{
    public interface IDiscordValidationService
    {
        DiscordId ValidateDiscordId(string id);
    }
}