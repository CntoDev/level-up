using System;
using System.Collections.Generic;

namespace CNTO.Launcher.CLI
{
    public class ConsoleDisplay : IDisplay
    {
        public void ShowRepositories(IEnumerable<Repository> repositories)
        {
            Console.WriteLine("Select repositories as comma separated list:");
            Console.WriteLine("-----");

            foreach(var r in repositories)
            {
                Console.WriteLine($"{r.RepositoryId.Name}");
            }

            Console.WriteLine();
            Console.WriteLine("Enter comma separated list of repositories to load and press enter:");
        }
    }
}
