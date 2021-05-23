using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CNTO.Launcher
{
    public class Server
    {
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

        internal void Run(IProcessRunner processRunner)
        {
            processRunner.Run(_processPath, _arguments);
        }
    }
}