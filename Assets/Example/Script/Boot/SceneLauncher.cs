using Framework.Architecture.Base;
using Framework.Architecture.Pattern.MVC;

namespace Game.Boot
{
    public abstract class SceneLauncher<TLauncher, TView> : BaseLauncher<TLauncher, TView>
        where TLauncher : SceneLauncher<TLauncher, TView>
        where TView : SceneView
    {
        protected override IMain GetMain()
        {
            return GameLauncher.Instance;
        }

        protected override ILoad GetLoader()
        {
            return SceneLoader.Instance;
        }
    }
}
