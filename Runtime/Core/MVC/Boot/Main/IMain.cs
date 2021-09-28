namespace Agate.MVC.Core
{
    public delegate void OnInitializeProgress(int progress, int total);
    public delegate void OnInitializeFinish();

    public interface IMain
    {
        event OnInitializeProgress OnInitializing;
        event OnInitializeFinish OnInitialized;
        bool IsInitialized { get; }
    }
}
