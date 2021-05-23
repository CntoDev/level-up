using System;
using System.Collections.Generic;

namespace CNTO.Launcher
{
    public interface IDisplay
    {
        public void ShowRepositories(IEnumerable<Repository> repositories);
    }
}