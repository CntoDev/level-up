using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace CNTO.Launcher
{
    public class Server
    {
        private const int ServerStartDelay = 3000;
        private readonly string _processPath;
        private readonly string _arguments;

        private Server(string processPath, string arguments)
        {
            _processPath = processPath;
            _arguments = arguments;
        }

        public static Server Build(LauncherParameters launcherParameters, IEnumerable<Repository> repositoryMetadata)
        {
            string fileName = launcherParameters.GamePath;
            StringBuilder sb = new StringBuilder();
            sb.Append($"-port 2302 -noSplash -noLand -enableHT -hugePages -profiles={launcherParameters.ProfilePath} -filePatching -name=server -config={launcherParameters.ConfigDirectory}\\server.cfg -cfg={launcherParameters.ConfigDirectory}\\basic.cfg");
            sb.Append(" ");
            var sortedRepositories = repositoryMetadata.OrderBy(r => r.Priority);
            ModSet modSet = new ModSet();

            foreach (var repo in sortedRepositories)
            {
                modSet.Append(repo);
            }

            sb.Append(modSet.ToString());
            return new Server(fileName, sb.ToString());
        }

        internal async Task RunAsync(IProcessRunner processRunner)
        {
            processRunner.Run(_processPath, _arguments);
            await Task.Delay(ServerStartDelay);
        }
    }
}