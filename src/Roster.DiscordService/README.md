# Discord Automation Service

This module handles Rooster events that require actions on the community's Discord Guild (a.k.a. Discord server). 

## Consumers

 - `MemberPromotedConsumer` updates Discord user tags whenever a new rank is issued to a community member. Consumes `Roster.Core.Events.MemberPromoted`.

## Configuration

 1. Configure the `DiscordOptions` parameters in `appsettings.json` with CNTO Discord Guild details. If you are unsure where to get the values from, check the notes section below. The sub-section `RanksMap` defines the mapping between rank identifiers in Roster and Discord respectively, using Roster ids as keys (under quotes before the comma) and Discord ids as values (after the comma). Refer to the snippet below for adjusting the configuration.
```json
"RanksMap": {
      "1": 925448093170815056,  <-- Recruit
      "2": 925448134446944287,  <-- Grunt
      "3": 925448093170815056,  <-- Reservist
      "4": 925448047121559603,  <-- Specialist
      "5": 925448012283658271,  <-- Corporal
      "6": 925447916674506832   <-- Staff Sergeant
    }
```

 2. The parameter `BotToken` should be passed as environment variable, i.e.
```bash
export DiscordOptions__BotToken="<bot_token_here>"
```

### Notes

If you don't know where to find the parameters to update in the `appsettings.json`, have a look [here](https://github.com/manix84/discord_gmod_addon_v2/wiki/Finding-your-Guild-ID-%28Server-ID%29) for how to get the Guild id. Once the Developer mode is enabled you can navigate to the Roles management page to get the ranks ids as well.