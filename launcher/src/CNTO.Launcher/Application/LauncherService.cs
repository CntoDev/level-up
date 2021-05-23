using System;
using System.Collections.Generic;
using CNTO.Launcher.Identity;

namespace CNTO.Launcher.Application
{
    public class LauncherService
    {
        private readonly LauncherParameters _launcherParameters;
        private readonly IRepositoryCollection _repositoryCollection;
        private readonly IDisplay _display;
        private readonly IProcessRunner _processRunner;

        public LauncherService(
            LauncherParameters launcherParameters,
            IRepositoryCollection repositoryCollection,
            IDisplay display,
            IProcessRunner processRunner
        )
        {
            _launcherParameters = launcherParameters;
            _repositoryCollection = repositoryCollection;
            _display = display;
            _processRunner = processRunner;
        }

        public void Run ()
        {
            IEnumerable<Repository> repositories = _repositoryCollection.All();
            _display.ShowRepositories(repositories);
        }

        public void StartServer (IEnumerable<RepositoryId> selectedRepositories)
        {
            IEnumerable<Repository> repositoryMetadata = _repositoryCollection.WithId(selectedRepositories);
            var server = Server.Build(_launcherParameters, repositoryMetadata);
            server.Run(_processRunner);
        }
    }
}
