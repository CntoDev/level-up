using Roster.Core.Services;
using Roster.Core.Domain;

namespace Roster.Infrastructure
{
    public class DummyDiscordValidationService : IDiscordValidationService
    {
        public DummyDiscordValidationService()
        {

        }

        public DiscordId ValidateDiscordId(string id)
        {
            return new DiscordId(id);
        }
    }
}