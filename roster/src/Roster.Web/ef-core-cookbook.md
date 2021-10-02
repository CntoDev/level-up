# Entity Framework Core migrations

Entity Framework (EF) Core is a lightweight, extensible, open source and cross-platform version of the popular Entity Framework data access technology.

EF Core can serve as an object-relational mapper (O/RM), which:

    Enables .NET developers to work with a database using .NET objects.
    Eliminates the need for most of the data-access code that typically needs to be written.

EF Core supports many database engines, see Database Providers for details. Detailed documentation of EF Core is on [Microsoft EF Core](https://docs.microsoft.com/en-us/ef/core/) website.

## Changing domain model

In case you added new attributes or removed existing ones, added new classes in `RosterDbContext` you need to apply those changes to the database. Do the following:

1. Position yourself in root of Web project, for example `/c/cnto/dev/level-up/roster/src/Roster.Web`
1. List existing migrations with ` dotnet ef migrations list --context RosterDbContext`
1. Add new migration with `dotnet ef migrations add <migration name> --context RosterDbContext`
1. Inspect the code created in `Migrations/RosterDb/` folder. Code has been written in <migration name>.cs
1. If happy with what you see, apply the change to database with `dotnet ef database update --context RosterDbContext`

If you want to generate a SQL script to apply to production server, instead of step 5, use ` dotnet ef migrations script <previous migration name> --context RosterDbContext --output /c/temp/roster-update.sql`