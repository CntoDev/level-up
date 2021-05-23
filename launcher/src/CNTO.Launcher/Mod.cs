using System;
using CNTO.Launcher.Identity;

namespace CNTO.Launcher
{
    public class Mod : IMod
    {
        private string _name;
        private Repository _repository;

        internal Mod (Repository repository, string name) {
            _repository = repository;
            _name = name;
        }

        public string Name => _name;

        public string GetFullName()
        {
            return $"{_repository.Path}\\{_name}";
        }

        public bool IsSame(IMod mod)
        {
            return mod.Name.Equals(_name);
        }
    }
}