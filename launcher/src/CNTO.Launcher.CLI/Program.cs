using System;
using Microsoft.Extensions.Configuration;
using CNTO.Launcher;
using CNTO.Launcher.Application;
using System.Linq;
using CNTO.Launcher.Identity;
using Serilog;
using CNTO.Launcher.Infrastructure;
using System.Threading.Tasks;

namespace CNTO.Launcher.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
                            
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(@"appsettings.json");
            IConfiguration configuration = builder.Build();
            LauncherParameters launcherParameters = configuration.Get<LauncherParameters>();

            Log.Information("Settings file read.");
            Log.Information("{@Parameters}", launcherParameters);
            
            FilesystemRepositoryCollection filesystemRepositoryCollection = new FilesystemRepositoryCollection(launcherParameters.Repositories);
            filesystemRepositoryCollection.Load();
            ConsoleDisplay display = new ConsoleDisplay();
            WindowsProcessRunner windowsProcessRunner = new WindowsProcessRunner();

            LauncherService launcherService = new LauncherService(launcherParameters, filesystemRepositoryCollection, display, windowsProcessRunner);
            launcherService.Run();

            string selection = Console.ReadLine();
            string[] selectedMods = selection.Split(",");

            Log.Information("Selected repositories are {selectedMods}.", selectedMods);
            Task.Run(() => launcherService.StartServerAsync(selectedMods.Select(s => new RepositoryId(s))));
        }
    }
}
