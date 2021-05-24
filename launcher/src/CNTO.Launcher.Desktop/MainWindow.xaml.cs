using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Serilog;
using UI.Source;
using CNTO.Launcher;
using Microsoft.Extensions.Configuration;
using CNTO.Launcher.Infrastructure;
using CNTO.Launcher.Application;
using CNTO.Launcher.Identity;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisplay
    {
        private readonly Repositories _repositories;
        private LauncherService _launcherService;
        private FilesystemRepositoryCollection _filesystemRepositoryCollection;

        public MainWindow()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnUnhandledException);
            InitializeComponent();

            _repositories = (Repositories)(RepositoriesGrid.Resources["Repo"]);
            Initialize();
        }

        public void ShowRepositories(IEnumerable<Repository> repositories)
        {

        }

        private void Initialize()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("launcher-log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(@"appsettings.json");
            IConfiguration configuration = builder.Build();
            LauncherParameters launcherParameters = configuration.Get<LauncherParameters>();

            Log.Information("Settings file read.");
            Log.Information("{@Parameters}", launcherParameters);

            _filesystemRepositoryCollection = new FilesystemRepositoryCollection(launcherParameters.Repositories);
            _filesystemRepositoryCollection.Load();

            _repositories.Load(_filesystemRepositoryCollection);
            IDisplay display = this;
            WindowsProcessRunner windowsProcessRunner = new WindowsProcessRunner();

            _launcherService = new LauncherService(launcherParameters, _filesystemRepositoryCollection, display, windowsProcessRunner);
            _launcherService.Run();
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            Log.Information("Launching server.");

            var selectedMods = _repositories.GetSelected().Select(r => r.Identity);
            Log.Information("Selected repositories are {selectedMods}.", selectedMods);

            int headlessClients = _repositories.HeadlessClientNumber;
            Log.Information("Number of headless clients is {headlessClients}.", headlessClients);

            Task.Run(() => _launcherService.StartServerAsync(selectedMods.Select(s => new RepositoryId(s)), headlessClients));
        }
    
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Log.Fatal((Exception)e.ExceptionObject, "Undhandled exception in application.");
            Log.Fatal("Runtime terminating {flag}.", e.IsTerminating);
        }    
    }
}
