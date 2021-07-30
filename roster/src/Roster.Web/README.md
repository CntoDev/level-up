# Getting started

Here is how to get started with the project.

## Install Postgres

`docker run --name dev-postgres -p 5432:5432 -e POSTGRES_PASSWORD=cnto_dev -d postgres`

Of course, you can put any password you want (POSTGRES_PASSWORD environment variable).

## Create identity database

Using tool like pgAdmin4 connect to your server and create a new database called `identity`.

## Build project

Open powershell, go to root folder of Roster.Web project and build a project with `dotnet build`.

## Deploy tables into database

In powershell (root folder of Web project) type `dotnet ef database update`. If you don't have Entity Framework Tools installed check out this [guide](https://docs.microsoft.com/en-us/ef/core/cli/dotnet#installing-the-tools).

## Start the application

Type `dotnet run` and hopefully your server will start. Monitor the console for any potential issues. Also, log file named roster-log-*.txt should give you more details on any problems. Application is using [Serilog](https://serilog.net/) for logging.