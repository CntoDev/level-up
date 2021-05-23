namespace CNTO.Launcher
{
    public interface IProcessRunner
    {
        void Run(string processPath, string arguments);
    }
}