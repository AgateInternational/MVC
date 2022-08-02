namespace Agate.MVC.Core
{
    public delegate void InitializeProgress(int progress, int total);
    public delegate void InitializeEvent();

    public interface IMain
    {
        event InitializeProgress OnInitializing;
        event InitializeEvent OnInitializeStart;
        event InitializeEvent OnInitializeFinish; 
        bool IsInitialized { get; }
        void InitMain();
    }
}
