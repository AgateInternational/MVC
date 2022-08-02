namespace Agate.MVC.Core
{
    public delegate void SceneLoadEvent(string sceneName);
    public delegate void TransitionEvent();

    public interface ILoad
    {
        event SceneLoadEvent OnSceneChanged;
        event TransitionEvent OnStartTransition;
        event TransitionEvent OnFinishTransition;
        bool IsInitialized { get; }

        void InitLoader();
        void LoadScene(string sceneName);
        void RestartScene();
        void RegisterLauncher(string sceneName, ILauncher launcher);
    }
}
