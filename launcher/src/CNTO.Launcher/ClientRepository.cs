using CNTO.Launcher.Identity;

namespace CNTO.Launcher
{
    internal class ClientRepository : Repository
    {
        internal ClientRepository(RepositoryId repositoryId, string path, int priority) : base(repositoryId, path, priority) { }
        
        public override bool ServerSide => false;

        internal override void AppendToModset(ModSet modSet)
        {
            foreach (var mod in Mods)
            {
                modSet.AddMod(mod);
            }
        }
    }
}