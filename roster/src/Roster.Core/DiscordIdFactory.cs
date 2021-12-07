using Roster.Core.Services;
using Roster.Core.Domain;

namespace Roster.Core
{
    public class DiscordIdFactory
    {
        private readonly IDiscordValidationService _validator;

        public DiscordIdFactory(IDiscordValidationService validator)
        {
            _validator = validator;
        }

        public DiscordId Create(string discordId)
        {
            return _validator.ValidateDiscordId(discordId);
        }
    }
}