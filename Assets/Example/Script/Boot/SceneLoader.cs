using Agate.MVC.Base;

namespace Example.Boot
{
    public class SceneLoader : BaseLoader<SceneLoader>
    {
        protected override string SplashScene { get { return "Splash";} }
    }
}
