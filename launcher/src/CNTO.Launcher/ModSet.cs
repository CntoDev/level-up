using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("CNTO.Launcher.Test")]
namespace CNTO.Launcher
{    
    internal class ModSet
    {
        private List<IMod> _mods = new List<IMod>();
        private List<IMod> _serverMods = new List<IMod>();

        public ModSet Append(Repository repository)
        {
            repository.AppendToModset(this);

            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            
            if (_mods.Any())
                sb.AppendFormat(" -mod={0}", string.Join(";", _mods.Select(x => x.GetFullName())));

            if (_serverMods.Any())
                sb.AppendFormat(" -serverMod={0}", string.Join(";", _serverMods.Select(x => x.GetFullName())));
            
            return sb.ToString();
        }

        internal void AddMod(Mod mod) => AddMod(_mods, mod);

        internal void AddServerSideMod(Mod mod) => AddMod(_serverMods, mod);

        private void AddMod(List<IMod> modList, Mod mod)
        {
            var existingMod = modList.FirstOrDefault(m => m.Name == mod.Name);

            if (existingMod != null)
                modList.Remove(existingMod);
            
            modList.Add(mod);            
        }
    }
}