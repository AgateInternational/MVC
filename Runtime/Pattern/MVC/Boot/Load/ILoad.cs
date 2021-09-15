namespace Framework.Architecture.Pattern.MVC
{
    public interface ILoad
    {
        void LoadScene(string sceneName);
        void RestartScene();
        void RequestLoadScene(string sceneName, ILauncher launcher);
    }
}
