using System.Collections;
using Framework.Architecture.Base;
using Framework.Architecture.Pattern.MVC;
using Game.Module.Analytic;

namespace Game.Boot
{
    public class GameLauncher : BaseMain<GameLauncher>, IMain
    {
        protected override IConnector[] GetSystemConnectors()
        {
            return new IConnector[]{
                new AnalyticConnector()
            };
        }

        protected override IController[] GetSystemDependencies()
        {
            return new IController[]{
                new AnalyticController()
            };
        }


        protected override IEnumerator InitSystem()
        {
            yield return null;
        }
    }
}
