namespace Framework.Architecture.Pattern.MVC
{
    public delegate void OnLoadFinish();

    public interface ILauncher
    {
        void Load(OnLoadFinish onFinish);
        void Unload(OnLoadFinish onFinish);
    }
}
