namespace Agate.MVC.Core
{
    public interface ISplash
    {
        bool IsInitialized { get; }
        void InitSplash();
    }
}
