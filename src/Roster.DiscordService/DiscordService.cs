using System;
using System.Collections.Generic;
using System.Net.Http;
using Roster.DiscordService.Configurations;
using Discord.Rest;
using Discord;

namespace Roster.DiscordService
{
    public class DiscordService
    {
        bool _loggedIn;

        readonly DiscordOptions _options;

        DiscordRestClient _client;

        RestGuild _guild;

        readonly ILogger<DiscordService> _logger;

        public DiscordService(DiscordOptions options, ILogger<DiscordService> logger)
        {
            _options = options;
            _loggedIn = false;
            _logger = logger;
            _client = new DiscordRestClient();
        }

        public async Task Login()
        {
            if (!_loggedIn)
            {
                await _client.LoginAsync(TokenType.Bot, _options.BotToken);
                _guild = await _client.GetGuildAsync(_options.GuildId);
                _loggedIn = true;

                _logger.LogInformation("DiscordService logged in");
            }
        }

        public async Task Logout()
        {
            if (_loggedIn)
            {
                await _client.LogoutAsync();
                _guild = null;
                _loggedIn = false;

                _logger.LogInformation("DiscordService logged out");
            }
        }

        public async Task UpdateMemberRank(UpdateMemberRankCommand command)
        {
            await Login();

            RestGuildUser member = await FindGuildMember(command.DiscordNickname);

            if (command.OldRank.HasValue)
            {
                ulong oldDiscordRoleId = MapRosterRankId(command.OldRank);
                await member.RemoveRoleAsync(oldDiscordRoleId);

                _logger.LogInformation($"Removed role {oldDiscordRoleId} from member {command.DiscordNickname}");
            }

            ulong newDiscordRoleId = MapRosterRankId(command.NewRank);
            await member.AddRoleAsync(newDiscordRoleId);

            _logger.LogInformation($"Added role {newDiscordRoleId} to member {command.DiscordNickname}");

            await Logout();
        }

        private async Task<RestGuildUser> FindGuildMember(string nickname)
        {
            IReadOnlyCollection<RestGuildUser> matches = await _guild.SearchUsersAsync(nickname, 1);

            if (matches.Count != 1)
            {
                throw new ArgumentException($"Couldn't find member {nickname} in CNTO Guild");
            }

            return matches.First();
        }

        private ulong MapRosterRankId(int? rosterRank)
        {
            // Note: the .ToString() call is required due to the automapping from appsettings.json
            return _options.RanksMap[rosterRank.ToString()];
        }
    }
}