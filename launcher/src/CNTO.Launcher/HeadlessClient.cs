using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNTO.Launcher
{
    public class HeadlessClient
    {
        private const int HeadlessClientDelay = 3000;
        private Guid _uid;
        private readonly LauncherParameters _launcherParameters;
        private readonly IProcessRunner _processRunner;

        public Guid UID => _uid;

        public HeadlessClient(LauncherParameters parameters, IProcessRunner processRunner)
        {
            _uid = Guid.NewGuid();
            _launcherParameters = parameters;
            _processRunner = processRunner;
        }

        public async Task StartAsync(IEnumerable<Repository> repositoryMetadata)
        {
            var sortedRepositories = repositoryMetadata.Where(r => !r.ServerSide).OrderBy(r => r.Priority);
            ModSet modSet = new ModSet();

            foreach (var repo in sortedRepositories)
            {
                modSet.Append(repo);
            }

            StringBuilder sb = new StringBuilder();
            sb.Append($"-port 2302 -noSplash -noLand -enableHT -hugePages -profiles={_launcherParameters.ProfilePath} -client -connect=127.0.0.1 -password={_launcherParameters.ServerPassword}");
            sb.Append(" ");
            sb.Append(modSet.ToString());

            _processRunner.Run(_launcherParameters.GamePath, sb.ToString());
            await Task.Delay(HeadlessClientDelay);
        }
    }
}