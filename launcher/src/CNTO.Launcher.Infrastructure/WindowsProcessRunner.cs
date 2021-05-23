using System.Diagnostics;
using Serilog;

namespace CNTO.Launcher.Infrastructure
{
    public class WindowsProcessRunner : IProcessRunner
    {
        public void Run(string processPath, string arguments)
        {
            Log.Information("Starting server with {process} {arguments}.", processPath, arguments);
            Process process = Process.Start(processPath, arguments);
            Log.Information("Server started.");
        }
    }
}
